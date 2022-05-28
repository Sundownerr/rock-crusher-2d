using Game.Base;
using Game.Base.Interface;
using UnityEngine;

namespace Game.Ship.Weapons.Bullet.Factory
{
    public class BulletProvider : Container<Transform>
    {
        private readonly IContainer<Transform> pool;

        public BulletProvider(GameObject bulletPrefab,
                              Transform shootPoint,
                              Transform parent)
        {
            var factory = new BulletFactory(bulletPrefab, shootPoint, parent);
            pool = new BulletPool(() => factory.Create());
        }

        protected override Transform GetItem() => pool.Get();

        protected override void ReturnItem(Transform item)
        {
            pool.Return(item);
        }
    }
}