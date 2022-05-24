using System.Collections;
using System.Collections.Generic;
using Game.Combat;
using Game.Enemy.Asteroid;
using Game.Enemy.Asteroid.Factory;
using Game.Enemy.Asteroid.Interface;
using Game.Enemy.Factory.Interface;
using Game.Enemy.Interface;
using Game.Enemy.UFO.Factory;
using Game.Gameplay.Utility;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyController : IUpdate, IDestroyable
    {
        private readonly IAsteroidFactory asteroidFactory;
        private readonly WaitForSeconds asteroidSpawnInterval;
        private readonly AsteroidStageController asteroidStageController;
        private readonly Dictionary<IEnemy, Damagable> enemies = new Dictionary<IEnemy, Damagable>();
        private readonly CoroutineRunner runner;
        private readonly ScreenBoundsController screenBoundsController;
        private readonly IEnemyFactory ufoFactory;
        private readonly WaitForSeconds ufoSpawnInterval;

        public EnemyController(CoroutineRunner runner,
                               ScreenBoundsController screenBoundsController,
                               AsteroidFactoryData asteroidFactoryData,
                               UfoFactoryData ufoFactoryData,
                               ParentData parentData)
        {
            this.runner = runner;
            this.screenBoundsController = screenBoundsController;

            asteroidFactory = new AsteroidFactory(asteroidFactoryData, parentData.AsteroidParent);
            asteroidStageController = new AsteroidStageController(asteroidFactory);

            asteroidFactory.Created += AsteroidFactoryOnCreated;

            ufoFactory = new UfoFactory(ufoFactoryData, parentData.AsteroidParent);

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
        }

        public void Update()
        {
            asteroidStageController.Update();

            foreach (var enemy in enemies)
            {
                if (enemy.Value.IsDamaged)
                {
                    enemies.Remove(enemy.Key);
                    break;
                }

                enemy.Key.Update();
            }
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
            //
            // var result = ufoFactory.Create();
            // HandleEnemySpawn(result);

            // runner.StartCoroutine(SpawnUfos());
        }

        private IEnumerator SpawnAsteroids()
        {
            yield return asteroidSpawnInterval;

            asteroidFactory.Create();
            runner.StartCoroutine(SpawnAsteroids());
        }
    }
}