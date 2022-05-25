using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Combat;
using Game.Enemy.Asteroid;
using Game.Enemy.Asteroid.Factory;
using Game.Enemy.Asteroid.Interface;
using Game.Enemy.Factory.Interface;
using Game.Enemy.Interface;
using Game.Enemy.UFO;
using Game.Enemy.UFO.Factory;
using Game.Gameplay.Utility;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Enemy
{
    public class EnemyController : IUpdate, IDestroyable
    {
        private readonly IAsteroidFactory asteroidFactory;
        private readonly WaitForSeconds asteroidSpawnInterval;
        private readonly AsteroidStageController asteroidStageController;
        private readonly Dictionary<IEnemy, EnemyDamagable> enemies = new();
        private readonly CoroutineRunner runner;
        private readonly ScreenBoundsController screenBoundsController;

        private readonly IUfoFactory ufoFactory;
        private readonly WaitForSeconds ufoSpawnInterval;
        private bool canSpawn;

        public EnemyController(CoroutineRunner runner,
                               ScreenBoundsController screenBoundsController,
                               AsteroidFactoryData asteroidFactoryData,
                               UfoFactoryData ufoFactoryData,
                               ParentData parentData,
                               Transform ship)
        {
            this.runner = runner;
            this.screenBoundsController = screenBoundsController;

            asteroidFactory = new AsteroidFactory(asteroidFactoryData, ship, parentData.AsteroidParent);
            asteroidStageController = new AsteroidStageController(asteroidFactory);
            asteroidFactory.Created += AsteroidFactoryOnCreated;

            ufoFactory = new UfoFactory(ufoFactoryData, parentData.UfoParent, ship);
            ufoFactory.Created += UfoFactoryOnCreated;

            asteroidSpawnInterval = new WaitForSeconds(asteroidFactoryData.SpawnInterval);
            ufoSpawnInterval = new WaitForSeconds(ufoFactoryData.SpawnInterval);
        }

        public void Destroy()
        {
            enemies.Clear();
            asteroidStageController.Destroy();

            asteroidFactory.Created -= AsteroidFactoryOnCreated;
            ufoFactory.Created -= UfoFactoryOnCreated;
        }

        public void Update()
        {
            asteroidStageController.Update();

            foreach (var enemy in enemies)
            {
                if (enemy.Value.IsDamaged)
                {
                    enemies.Remove(enemy.Key);
                    HandleDamagedEnemy(enemy.Value);
                    Object.Destroy(enemy.Value.gameObject);
                    break;
                }

                enemy.Key.Update();
            }

            void HandleDamagedEnemy(EnemyDamagable enemy)
            {
                switch (enemy)
                {
                    case AsteroidData asteroid:
                        switch (asteroid.Stage)
                        {
                            case AsteroidData.AsteroidStage.Big:
                                BigAsteroidDestroyed?.Invoke(asteroid.transform);
                                break;
                            case AsteroidData.AsteroidStage.Medium:
                                MediumAsteroidDestroyed?.Invoke(asteroid.transform);
                                break;
                            case AsteroidData.AsteroidStage.Small:
                                SmallAsteroidDestroyed?.Invoke(asteroid.transform);
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        break;
                    case UfoData ufo:
                        UfoDestroyed?.Invoke(ufo.transform);
                        break;
                }
            }
        }

        public void HandleShipDestroyed()
        {
            var ufos = enemies.Select(x => x.Key).Where(x => x is UfoController).ToArray();

            foreach (var ufo in ufos)
                enemies.Remove(ufo);
        }

        public event Action<Transform> SmallAsteroidDestroyed;
        public event Action<Transform> MediumAsteroidDestroyed;
        public event Action<Transform> BigAsteroidDestroyed;
        public event Action<Transform> UfoDestroyed;

        private void UfoFactoryOnCreated((IEnemy ufo, UfoData data) result)
        {
            enemies.Add(result.ufo, result.data);
            screenBoundsController.Add(result.data.transform);
        }

        private void AsteroidFactoryOnCreated((IAsteroid asteroid, AsteroidData data) result)
        {
            enemies.Add(result.asteroid, result.data);
            screenBoundsController.Add(result.data.transform);
        }

        public void StartSpawn()
        {
            canSpawn = true;
            runner.StartCoroutine(SpawnAsteroids());
            runner.StartCoroutine(SpawnUfos());
        }

        public void StopSpawn()
        {
            canSpawn = false;
        }

        private IEnumerator SpawnUfos()
        {
            yield return ufoSpawnInterval;

            if (!canSpawn)
                yield break;

            ufoFactory.Create();
            runner.StartCoroutine(SpawnUfos());
        }

        private IEnumerator SpawnAsteroids()
        {
            yield return asteroidSpawnInterval;

            if (!canSpawn)
                yield break;

            asteroidFactory.Create();
            runner.StartCoroutine(SpawnAsteroids());
        }
    }
}