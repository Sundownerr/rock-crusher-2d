using System;
using Game.Combat;
using Game.Enemy.Factory.Interface;
using Game.Enemy.Interface;
using UnityEngine;

namespace Game.Enemy.Asteroid.Factory
{
    public class EnemyPool<T> : Pool<(IEnemy controller, T data)> where T : EnemyDamagable
    {
        private readonly IEnemyFactory<T> factory;
        private readonly Func<Vector3> getSpawnPosition;

        public EnemyPool(IEnemyFactory<T> factory, Func<Vector3> getSpawnPosition)
        {
            this.factory = factory;
            this.getSpawnPosition = getSpawnPosition;
        }

        public override (IEnemy controller, T data) Get()
        {
            var asteroid = base.Get();

            asteroid.data.gameObject.SetActive(true);
            return asteroid;
        }

        public override void Return((IEnemy controller, T data) item)
        {
            item.data.gameObject.SetActive(false);
            item.data.IsDamaged = false;
            item.data.IsCompletlyDestroyed = false;
            base.Return(item);
        }

        protected override (IEnemy controller, T data) GetNew() =>
            factory.Create(getSpawnPosition());
    }
}