using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Game.Base;
using Game.Combat;
using Game.Enemy.Asteroid;
using Game.Enemy.Asteroid.Factory;
using Game.Enemy.Interface;
using Game.Enemy.UFO;
using Game.Enemy.UFO.Factory;
using Game.Gameplay.Utility;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Enemy
{
    public class EnemyController : Controller<EnemyData>, IUpdate, IDestroyable
    {
        private readonly WaitForSeconds asteroidSpawnInterval;

        private readonly AsteroidStageController asteroidStageController;
        private readonly EnemyContainer<AsteroidData> bigAsteroidContainer;
        private readonly Dictionary<IEnemy, EnemyDamagable> enemies = new();
        private readonly EnemyContainer<AsteroidData> mediumAsteroidContainer;
        private readonly CoroutineRunner runner;
        private readonly ScreenBoundsController screenBoundsController;
        private readonly Transform ship;
        private readonly EnemyContainer<AsteroidData> smallAsteroidContainer;
        private readonly EnemyContainer<UfoData> ufoContainer;
        private readonly WaitForSeconds ufoSpawnInterval;
        private bool canSpawn;

        public EnemyController(EnemyData model,
                               CoroutineRunner runner,
                               ScreenBoundsController screenBoundsController,
                               ParentData parentData,
                               Transform ship) : base(model)
        {
            this.runner = runner;
            this.screenBoundsController = screenBoundsController;
            this.ship = ship;

            bigAsteroidContainer = new EnemyContainer<AsteroidData>(
                new AsteroidFactory(model.BigAsteroidFactoryData, parentData.AsteroidParent),
                () => GetEnemyCreationPoint(model.AsteroidSpawnRadius));

            mediumAsteroidContainer = new EnemyContainer<AsteroidData>(
                new AsteroidFactory(model.MediumAsteroidFactoryData, parentData.AsteroidParent),
                () => GetEnemyCreationPoint(model.AsteroidSpawnRadius));

            smallAsteroidContainer = new EnemyContainer<AsteroidData>(
                new AsteroidFactory(model.SmallAsteroidFactoryData, parentData.AsteroidParent),
                () => GetEnemyCreationPoint(model.AsteroidSpawnRadius));

            asteroidStageController = new AsteroidStageController(
                bigAsteroidContainer,
                mediumAsteroidContainer,
                smallAsteroidContainer);

            ufoContainer = new EnemyContainer<UfoData>(
                new UfoFactory(model.UfoFactoryData, parentData.UfoParent, ship),
                () => GetEnemyCreationPoint(model.UfoSpawnRadius));

            asteroidSpawnInterval = new WaitForSeconds(model.AsteroidSpawnInterval);
            ufoSpawnInterval = new WaitForSeconds(model.UfoSpawnInterval);

            bigAsteroidContainer.ItemGiven += EnemyContainerOnItemGiven;
            mediumAsteroidContainer.ItemGiven += EnemyContainerOnItemGiven;
            smallAsteroidContainer.ItemGiven += EnemyContainerOnItemGiven;
            ufoContainer.ItemGiven += EnemyContainerOnItemGiven;
        }

        public void Destroy()
        {
            enemies.Clear();
            asteroidStageController.Destroy();

            bigAsteroidContainer.ItemGiven -= EnemyContainerOnItemGiven;
            mediumAsteroidContainer.ItemGiven -= EnemyContainerOnItemGiven;
            smallAsteroidContainer.ItemGiven -= EnemyContainerOnItemGiven;
            ufoContainer.ItemGiven -= EnemyContainerOnItemGiven;
        }

        public void Update()
        {
            asteroidStageController.Update();

            foreach (var enemy in enemies)
            {
                if (enemy.Value.IsDamaged)
                {
                    enemies.Remove(enemy.Key);
                    HandleDamagedEnemy(enemy);

                    break;
                }

                enemy.Key.Update();
            }

            void HandleDamagedEnemy(KeyValuePair<IEnemy, EnemyDamagable> enemy)
            {
                switch (enemy.Value)
                {
                    case AsteroidData asteroid:
                        switch (asteroid.Stage)
                        {
                            case AsteroidData.AsteroidStage.Big:
                                BigAsteroidDestroyed?.Invoke(asteroid.transform);
                                bigAsteroidContainer.Return((enemy.Key, asteroid));
                                break;
                            case AsteroidData.AsteroidStage.Medium:
                                MediumAsteroidDestroyed?.Invoke(asteroid.transform);
                                mediumAsteroidContainer.Return((enemy.Key, asteroid));
                                break;
                            case AsteroidData.AsteroidStage.Small:
                                SmallAsteroidDestroyed?.Invoke(asteroid.transform);
                                smallAsteroidContainer.Return((enemy.Key, asteroid));
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }

                        break;
                    case UfoData ufo:
                        UfoDestroyed?.Invoke(ufo.transform);
                        ufoContainer.Return((enemy.Key, ufo));
                        break;
                }
            }
        }

        private void EnemyContainerOnItemGiven(IEnemy controller, EnemyDamagable data)
        {
            enemies.Add(controller, data);
            screenBoundsController.Add(data.transform);
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

            var enemy = ufoContainer.Get();
            enemy.Item2.transform.position = GetEnemyCreationPoint(model.UfoSpawnRadius);

            runner.StartCoroutine(SpawnUfos());
        }

        private IEnumerator SpawnAsteroids()
        {
            yield return asteroidSpawnInterval;

            if (!canSpawn)
                yield break;

            var enemy = bigAsteroidContainer.Get();
            enemy.Item2.transform.position = GetEnemyCreationPoint(model.AsteroidSpawnRadius);

            runner.StartCoroutine(SpawnAsteroids());
        }

        private Vector3 GetEnemyCreationPoint(float radius)
        {
            var random = 10 + Random.Range(1f, 10f);
            var x = Mathf.Sin(random * radius) * radius;
            var y = Mathf.Cos(random * radius) * radius;

            var randomOffset = new Vector3(x, y);
            return ship.position + randomOffset;
        }
    }
}