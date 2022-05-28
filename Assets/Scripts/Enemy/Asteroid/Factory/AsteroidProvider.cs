using System;
using Game.Base.Interface;
using Game.Enemy.Base;
using Game.Enemy.Interface;
using UnityEngine;

namespace Game.Enemy.Asteroid.Factory
{
    public class AsteroidProvider : EnemyProvider<AsteroidData>, IDestroyable
    {
        public AsteroidProvider(AsteroidFactoryData factoryData,
                                Func<Vector3> getSpawnPosition,
                                Transform parent)
        {
            var factory = new AsteroidFactory(factoryData, parent);
            pool = new EnemyPool<IEnemy, AsteroidData>(() => factory.Create(getSpawnPosition()));

            pool.ItemGiven += OnItemGiven;
        }

        public void Destroy()
        {
            pool.ItemGiven -= OnItemGiven;
        }

        private void OnItemGiven(IEnemy enemy, AsteroidData data)
        {
            enemy.Damaged += OnDamaged;

            void OnDamaged()
            {
                enemy.Damaged -= OnDamaged;
                pool.Return(enemy, data);
            }
        }
    }
}