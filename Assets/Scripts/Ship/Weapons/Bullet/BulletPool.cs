using Game.Base.Interface;
using UnityEngine;

namespace Game
{
    public class BulletPool : Pool<Transform>
    {
        private readonly IFactory<Transform> factory;

        public BulletPool(IFactory<Transform> factory)
        {
            this.factory = factory;
        }

        public override Transform Give()
        {
            var bullet = base.Give();
            bullet.gameObject.SetActive(true);

            return bullet;
        }

        public override void Take(Transform item)
        {
            base.Take(item);
            item.gameObject.SetActive(false);
        }

        protected override Transform GetNewItem() => factory.Create().transform;
    }
}