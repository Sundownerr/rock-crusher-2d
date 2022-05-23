using System;
using Game.Base;
using Game.Enemies.Asteroid.Spawner.Interface;
using UnityEngine;

namespace Game.Enemies.UFO.Spawner
{
    public class UfoSpawner : Controller<UfoSpawnerData>, IEnemySpawner
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

        public event Action<(IUpdate, Transform)> Created;

        public void StartSpawn()
        {
            // throw new NotImplementedException(); 
        }
    }
}