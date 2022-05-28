using System;
using System.Collections;
using Game.Base;
using Game.Base.Interface;
using Game.Enemy.Asteroid.Factory;
using Game.Enemy.Base;
using Game.Enemy.Interface;
using Game.Enemy.Ufo.Factory;
using Game.Gameplay.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Enemy
{
    public class EnemyFactory : Controller<EnemyData>, IDestroyable, IFactory<IEnemy, EnemyDamagable>
    {
        private readonly WaitForSeconds asteroidSpawnInterval;
        private readonly AsteroidStageController asteroidStageController;
        private readonly AsteroidProvider bigAsteroidProvider;
        private readonly AsteroidProvider mediumAsteroidProvider;
        private readonly CoroutineRunner runner;
        private readonly Transform ship;
        private readonly AsteroidProvider smallAsteroidProvider;
        private readonly UfoProvider ufoProvider;
        private readonly WaitForSeconds ufoSpawnInterval;
        private bool canSpawn = true;

        public EnemyFactory(EnemyData model, CoroutineRunner runner, ParentData parentData, Transform ship) :
            base(model)
        {
            this.runner = runner;
            this.ship = ship;

            bigAsteroidProvider = new AsteroidProvider(
                model.BigAsteroidFactoryData,
                () => GetEnemyCreationPoint(model.AsteroidSpawnRadius),
                parentData.AsteroidParent);

            mediumAsteroidProvider = new AsteroidProvider(
                model.MediumAsteroidFactoryData,
                () => GetEnemyCreationPoint(model.AsteroidSpawnRadius),
                parentData.AsteroidParent);

            smallAsteroidProvider = new AsteroidProvider(
                model.SmallAsteroidFactoryData,
                () => GetEnemyCreationPoint(model.AsteroidSpawnRadius),
                parentData.AsteroidParent);

            ufoProvider = new UfoProvider(
                model.UfoFactoryData,
                () => GetEnemyCreationPoint(model.AsteroidSpawnRadius),
                ship,
                parentData.AsteroidParent);

            asteroidStageController = new AsteroidStageController(
                bigAsteroidProvider,
                mediumAsteroidProvider,
                smallAsteroidProvider);

            asteroidSpawnInterval = new WaitForSeconds(model.AsteroidSpawnInterval);
            ufoSpawnInterval = new WaitForSeconds(model.UfoSpawnInterval);

            bigAsteroidProvider.ItemGiven += OnEnemyCreated;
            mediumAsteroidProvider.ItemGiven += OnEnemyCreated;
            smallAsteroidProvider.ItemGiven += OnEnemyCreated;
            ufoProvider.ItemGiven += OnEnemyCreated;
        }

        public void Destroy()
        {
            asteroidStageController.Destroy();

            bigAsteroidProvider.ItemGiven -= OnEnemyCreated;
            mediumAsteroidProvider.ItemGiven -= OnEnemyCreated;
            smallAsteroidProvider.ItemGiven -= OnEnemyCreated;
            ufoProvider.ItemGiven -= OnEnemyCreated;

            bigAsteroidProvider.Destroy();
            mediumAsteroidProvider.Destroy();
            smallAsteroidProvider.Destroy();
            ufoProvider.Destroy();
        }

        public event Action<IEnemy, EnemyDamagable> Created;

        public void CreateEnemies()
        {
            canSpawn = true;
            runner.StartCoroutine(CreateAsteroids());
            runner.StartCoroutine(CreateUfos());
        }

        public void StopCreatingEnemies()
        {
            canSpawn = false;
        }

        private void OnEnemyCreated(IEnemy controller, EnemyDamagable data)
        {
            Created?.Invoke(controller, data);
        }

        private Vector3 GetEnemyCreationPoint(float radius)
        {
            var random = 10 + Random.Range(1f, 10f);
            var x = Mathf.Sin(random * radius) * radius;
            var y = Mathf.Cos(random * radius) * radius;

            var randomOffset = new Vector3(x, y);
            return ship.position + randomOffset;
        }

        private IEnumerator CreateUfos()
        {
            yield return ufoSpawnInterval;

            if (!canSpawn)
                yield break;

            var enemy = ufoProvider.Get();
            enemy.Item2.transform.position = GetEnemyCreationPoint(model.UfoSpawnRadius);

            runner.StartCoroutine(CreateUfos());
        }

        private IEnumerator CreateAsteroids()
        {
            yield return asteroidSpawnInterval;

            if (!canSpawn)
                yield break;

            var enemy = bigAsteroidProvider.Get();
            enemy.Item2.transform.position = GetEnemyCreationPoint(model.AsteroidSpawnRadius);

            runner.StartCoroutine(CreateAsteroids());
        }
    }
}