using System;
using System.Collections;
using Game.Base;
using Game.Enemies.Asteroid.Spawner.Interface;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game.Enemies.Asteroid.Spawner
{
    public class AsteroidSpawner : Controller<AsteroidSpawnerData>, IEnemySpawner, IDestroyable
    {
        private readonly MonoBehaviour coroutineRunner;
        private readonly Transform parent;
        private readonly WaitForSeconds spawnDelay;

        private bool canSpawn;

        public AsteroidSpawner(AsteroidSpawnerData model, MonoBehaviour coroutineRunner, Transform parent) : base(model)
        {
            this.coroutineRunner = coroutineRunner;
            this.parent = parent;

            spawnDelay = new WaitForSeconds(model.spawnDelay);
        }

        public void Destroy()
        {
            canSpawn = false;
            coroutineRunner.StopCoroutine(Spawn());
        }

        public event Action<(IUpdate, Transform)> Created;

        public void StartSpawn()
        {
            canSpawn = true;
            coroutineRunner.StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            yield return spawnDelay;

            var randomOffset = Random.insideUnitCircle * 3f;
            var spawnPos = Vector2.zero + randomOffset;

            var asteroid = Object.Instantiate(model.Prefab, spawnPos, Quaternion.identity, parent);

            var movementController = new AsteroidMovementController(model.SpeedData, asteroid.transform);
            var controller = new AsteroidController(movementController);

            Created?.Invoke((controller, asteroid.transform));

            if (!canSpawn)
                yield break;

            coroutineRunner.StartCoroutine(Spawn());
        }
    }
}