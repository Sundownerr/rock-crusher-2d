using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Weapons.Bullet
{
    public class BulletFactory : IFactory<Transform>
    {
        private readonly GameObject bulletPrefab;
        private readonly Transform parent;
        private readonly Transform shootPoint;

        public BulletFactory(GameObject bulletPrefab, Transform shootPoint, Transform parent)
        {
            this.bulletPrefab = bulletPrefab;
            this.shootPoint = shootPoint;
            this.parent = parent;
        }

        public event Action<Transform> Created;

        public Transform Create()
        {
            var bullet = Object.Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation, parent);
            return bullet.transform;
        }
    }
}