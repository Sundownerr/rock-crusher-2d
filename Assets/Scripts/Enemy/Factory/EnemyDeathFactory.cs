using System;
using Game.Base.Interface;
using Game.Enemy.Base;
using Game.Enemy.Interface;

namespace Game.Enemy
{
    public class EnemyDeathFactory : IFactory<IEnemy, EnemyDamagable>, IDestroyable
    {
        private readonly IFactory<IEnemy, EnemyDamagable> enemyFactory;

        public EnemyDeathFactory(IFactory<IEnemy, EnemyDamagable> enemyFactory)
        {
            this.enemyFactory = enemyFactory;

            enemyFactory.Created += OnEnemyCreated;
        }

        public void Destroy()
        {
            enemyFactory.Created -= OnEnemyCreated;
        }

        public event Action<IEnemy, EnemyDamagable> Created;

        private void OnEnemyCreated(IEnemy enemy, EnemyDamagable damagable)
        {
            enemy.Damaged += OnDamaged;

            void OnDamaged()
            {
                enemy.Damaged -= OnDamaged;
                Created?.Invoke(enemy, damagable);
            }
        }
    }
}