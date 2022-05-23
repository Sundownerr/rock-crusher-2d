using System;
using System.Collections;
using Game.Base;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game.Enemies.Asteroid
{
    public class AsteroidSpawner : Controller<AsteroidSpawnerData>, IFactory<GameObject>, IDestroyable
    {
        private readonly MonoBehaviour coroutineRunner;
        private readonly Transform parent;
        private readonly WaitForSeconds spawnDelay;

        public AsteroidSpawner(AsteroidSpawnerData model, MonoBehaviour coroutineRunner, Transform parent) : base(model)
        {
            this.coroutineRunner = coroutineRunner;
            this.parent = parent;

            spawnDelay = new WaitForSeconds(model.spawnDelay);
        }

        public void Destroy()
        {
            coroutineRunner.StopCoroutine(Spawn());
        }

        public event Action<GameObject> Created;

        public void StartSpawn()
        {
            coroutineRunner.StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            yield return spawnDelay;

            var randomOffset = Random.insideUnitCircle * 3f;
            var spawnPos = Vector2.zero + randomOffset;

            var randomIndex = Random.Range(0, model.prefabs.Length);
            var prefab = model.prefabs[randomIndex];

            var asteroid = Object.Instantiate(prefab, spawnPos, Quaternion.identity, parent);

            Created?.Invoke(asteroid);
        }
    }
}