using System;
using Game.Base.Interface;
using Game.Enemy.Base;
using Game.Enemy.Interface;
using Game.Enemy.UFO;
using Game.Enemy.UFO.Factory;
using UnityEngine;

namespace Game.Enemy.Ufo.Factory
{
    public class UfoProvider : EnemyProvider<UfoData>, IDestroyable
    {
        public UfoProvider(UfoFactoryData factoryData,
                           Func<Vector3> getSpawnPosition,
                           Transform ufoTarget,
                           Transform parent)
        {
            var factory = new UfoFactory(factoryData, parent, ufoTarget);
            pool = new EnemyPool<IEnemy, UfoData>(() => factory.Create(getSpawnPosition()));

            pool.ItemGiven += OnItemGiven;
        }

        public void Destroy()
        {
            pool.ItemGiven -= OnItemGiven;
        }

        private void OnItemGiven(IEnemy enemy, UfoData data)
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