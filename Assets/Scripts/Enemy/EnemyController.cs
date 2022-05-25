using System;
using System.Collections;
using System.Collections.Generic;
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
        private readonly Dictionary<IEnemy, EnemyDamagable> enemies = new Dictionary<IEnemy, EnemyDamagable>();
        private readonly CoroutineRunner runner;
        private readonly ScreenBoundsController screenBoundsController;
        private readonly IUfoFactory ufoFactory;
        private readonly WaitForSeconds ufoSpawnInterval;

        public EnemyController(CoroutineRunner runner,
                               ScreenBoundsController screenBoundsController,
                               AsteroidFactoryData asteroidFactoryData,
                               UfoFactoryData ufoFactoryData,
                               ParentData parentData,
                               Transform ship)
        {
            this.runner = runner;
            this.screenBoundsController = screenBoundsController;

            asteroidFactory = new AsteroidFactory(asteroidFactoryData, parentData.AsteroidParent);
            asteroidStageController = new AsteroidStageController(asteroidFactory);
            asteroidFactory.Created += AsteroidFactoryOnCreated;

            ufoFactory = new UfoFactory(ufoFactoryData, parentData.AsteroidParent, ship);
            ufoFactory.Created += UfoFactoryOnCreated;

            asteroidSpawnInterval = new WaitForSeconds(asteroidFactoryData.SpawnInterval);
            ufoSpawnInterval = new WaitForSeconds(ufoFactoryData.SpawnInterval);
        }

        public void Destroy()
        {
            enemies.Clear();
            runner.StopCoroutine(SpawnUfos());
            runner.StopCoroutine(SpawnAsteroids());
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
                    HandleDamagedEnemy(enemy.Value);
                    enemies.Remove(enemy.Key);
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
                                BigAsteroidDestroyed?.Invoke();
                                break;
                            case AsteroidData.AsteroidStage.Medium:
                                MediumAsteroidDestroyed?.Invoke();
                                break;
                            case AsteroidData.AsteroidStage.Small:
                                SmallAsteroidDestroyed?.Invoke();
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        break;
                    case UfoData ufo:
                        Object.Destroy(ufo.gameObject);
                        UfoDestroyed?.Invoke();
                        break;
                }
            }
        }

        public event Action SmallAsteroidDestroyed;
        public event Action MediumAsteroidDestroyed;
        public event Action BigAsteroidDestroyed;
        public event Action UfoDestroyed;

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
            runner.StartCoroutine(SpawnAsteroids());
            runner.StartCoroutine(SpawnUfos());
        }

        private IEnumerator SpawnUfos()
        {
            yield return ufoSpawnInterval;

            ufoFactory.Create();
            runner.StartCoroutine(SpawnUfos());
        }

        private IEnumerator SpawnAsteroids()
        {
            yield return asteroidSpawnInterval;

            asteroidFactory.Create();
            runner.StartCoroutine(SpawnAsteroids());
        }
    }
}