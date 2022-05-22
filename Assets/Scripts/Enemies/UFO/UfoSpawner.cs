using System;
using UnityEngine;

namespace Game
{
    public class UfoSpawner : Spawner<UfoSpawnerData, GameObject>, IDestroyable
    {
        private readonly MonoBehaviour coroutineRunner;
        private readonly Transform parent;

        public UfoSpawner(UfoSpawnerData model, MonoBehaviour coroutineRunner, Transform parent) : base(model)
        {
            this.coroutineRunner = coroutineRunner;
            this.parent = parent;
        }

        public void Destroy()
        {
            // coroutineRunner.StopCoroutine(Spawn());
        }

        public void StartSpawn()
        {
            // throw new NotImplementedException();
        }

        public override event Action<GameObject> Created;
    }
}