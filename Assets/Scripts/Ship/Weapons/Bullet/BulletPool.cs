using Game.Ship.Weapons.Bullet;
using UnityEngine;

namespace Game
{
    public class BulletPool : Pool<Transform>
    {
        private readonly BulletFactory bulletFactory;

        public BulletPool(BulletFactory bulletFactory)
        {
            this.bulletFactory = bulletFactory;
        }

        public override Transform Get()
        {
            var bullet = base.Get();
            bullet.gameObject.SetActive(true);

            return bullet;
        }

        public override void Return(Transform item)
        {
            item.gameObject.SetActive(false);
            base.Return(item);
        }

        protected override Transform GetNew() => bulletFactory.Create();
    }
}