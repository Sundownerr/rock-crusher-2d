using System;
using Game.Combat;
using Game.Enemy.Asteroid.Factory;
using Game.Enemy.Factory.Interface;
using Game.Enemy.Interface;
using UnityEngine;

namespace Game.Enemy
{
    public class EnemyContainer<T> : IContainer<IEnemy, T> where T : EnemyDamagable
    {
        private readonly EnemyPool<T> pool;

        public EnemyContainer(IEnemyFactory<T> factory, Func<Vector3> getSpawnPosition)
        {
            pool = new EnemyPool<T>(factory, getSpawnPosition);
        }

        public event Action<IEnemy, T> ItemGiven;
        public event Action<IEnemy, T> ItemReturned;

        public (IEnemy, T) Get()
        {
            var item = pool.Get();
            ItemGiven?.Invoke(item.controller, item.data);

            return item;
        }

        public void Return((IEnemy, T) item)
        {
            pool.Return(item);
            ItemReturned?.Invoke(item.Item1, item.Item2);
        }
    }
}