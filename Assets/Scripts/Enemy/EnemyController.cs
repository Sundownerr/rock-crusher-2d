using System.Collections;
using System.Collections.Generic;
using Game.Enemy.Asteroid.Factory;
using Game.Enemy.Factory.Interface;
using Game.Enemy.UFO.Factory;
using Game.Gameplay.Utility;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyController : IUpdate, IDestroyable
    {
        private readonly IEnemyFactory asteroidFactory;
        private readonly WaitForSeconds asteroidSpawnInterval;
        private readonly CoroutineRunner runner;
        private readonly ScreenBoundsController screenBoundsController;
        private readonly IEnemyFactory ufoFactory;
        private readonly WaitForSeconds ufoSpawnInterval;
        private readonly List<IUpdate> updatees = new List<IUpdate>();

        public EnemyController(CoroutineRunner runner,
                               ScreenBoundsController screenBoundsController,
                               AsteroidFactoryData asteroidFactoryData,
                               UfoFactoryData ufoFactoryData,
                               ParentData parentData)
        {
            this.runner = runner;
            this.screenBoundsController = screenBoundsController;

            asteroidFactory = new AsteroidFactory(asteroidFactoryData, parentData.AsteroidParent);
            ufoFactory = new UfoFactory(ufoFactoryData, parentData.AsteroidParent);

            asteroidSpawnInterval = new WaitForSeconds(asteroidFactoryData.SpawnInterval);
            ufoSpawnInterval = new WaitForSeconds(ufoFactoryData.SpawnInterval);
        }

        public void Destroy()
        {
            updatees.Clear();
            runner.StopCoroutine(SpawnUfos());
            runner.StopCoroutine(SpawnAsteroids());
        }

        public void Update()
        {
            for (var i = 0; i < updatees.Count; i++)
                updatees[i].Update();
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

            var result = asteroidFactory.Create();
            HandleEnemySpawn(result);

            runner.StartCoroutine(SpawnAsteroids());
        }

        private void HandleEnemySpawn((IUpdate, Transform) spawnResult)
        {
            updatees.Add(spawnResult.Item1);
            screenBoundsController.Add(spawnResult.Item2);
        }
    }
}