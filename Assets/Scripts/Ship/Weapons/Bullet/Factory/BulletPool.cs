using System;
using Game.Base;
using UnityEngine;

namespace Game.Ship.Weapons.Bullet
{
    public class BulletPool : Pool<Transform>
    {
        private readonly Func<Transform> factory;

        public BulletPool(Func<Transform> factory)
        {
            this.factory = factory;
        }

        protected override Transform GetNew() => factory();

        protected override void ActivateItem(Transform item)
        {
            item.gameObject.SetActive(true);
        }

        protected override void DeactivateItem(Transform item)
        {
            item.gameObject.SetActive(false);
        }
    }
}