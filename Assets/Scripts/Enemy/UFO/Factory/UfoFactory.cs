using System;
using Game.Base;
using Game.Enemy.Factory.Interface;
using UnityEngine;

namespace Game.Enemy.UFO.Factory
{
    public class UfoFactory : Controller<UfoFactoryData>, IEnemyFactory
    {
        private readonly Transform parent;

        public UfoFactory(UfoFactoryData model, Transform parent) : base(model)
        {
            this.parent = parent;
        }

        public event Action<(IUpdate, Transform)> Created;

        public (IUpdate, Transform) Create()
        {
            Debug.Log("ufo spawn");
            return default;
        }

        public void Destroy()
        {
            // coroutineRunner.StopCoroutine(Spawn());
        }

        public void StartSpawn()
        {
            // throw new NotImplementedException(); 
        }
    }
}