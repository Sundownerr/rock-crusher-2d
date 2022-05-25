using System;
using Game.Base;
using Game.Combat;
using Game.Enemy.Factory.Interface;
using Game.Enemy.Interface;
using UnityEngine;

namespace Game.Enemy.Factory
{
    public abstract class EnemyFactory<T, V> : Controller<T>, IEnemyFactory<V>
        where T : GameObjectFactoryData
        where V : EnemyDamagable
    {
        protected readonly Transform parent;

        protected EnemyFactory(T model, Transform parent) : base(model)
        {
            this.parent = parent;
        }

        public abstract (IEnemy controller, V model) Create(Vector3 position);

        public abstract event Action<IEnemy, V> Created;
    }
}