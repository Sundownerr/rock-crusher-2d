using System;
using Game.Base;
using Game.Enemy.Interface;

namespace Game.Enemy.Base
{
    public class EnemyPool<T1, T2> : Pool<T1, T2>
        where T1 : IEnemy
        where T2 : EnemyDamagable
    {
        private readonly Func<(T1, T2)> factory;

        public EnemyPool(Func<(T1, T2)> factory)
        {
            this.factory = factory;
        }

        protected override (T1, T2) GetNew() => factory();

        protected override void ActivateItem(T1 item1, T2 item2)
        {
            item2.IsCompletlyDestroyed = false;
            item2.IsDamaged = false;
            item2.gameObject.SetActive(true);
        }

        protected override void DeactivateItem(T1 item1, T2 item2)
        {
            item2.gameObject.SetActive(false);
        }
    }
}