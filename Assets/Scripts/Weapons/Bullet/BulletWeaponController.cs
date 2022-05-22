using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game
{
    public class BulletWeaponController : ShipWeapon, IWeaponController, IFactory<Transform>
    {
        private readonly BulletFactory bulletFactory;
        private readonly Transform bulletParent;
        private readonly List<Transform> bullets = new List<Transform>();
        private readonly BulletWeapon model;

        public BulletWeaponController(BulletWeapon model, Transform shootPoint, Transform bulletParent) :
            base(shootPoint)
        {
            this.model = model;

            this.bulletParent = bulletParent;

            bulletFactory = new BulletFactory(model.bulletPrefab);
        }

        public event Action<Transform> Created;

        public event Action<Transform> Hit;

        public void Update()
        {
            for (var index = 0; index < bullets.Count; index++)
            {
                var bullet = bullets[index];

                if (bullet == null)
                {
                    bullets.Remove(bullet);
                    continue;
                }

                bullet.position += bullet.up * (Time.deltaTime * model.bulletSpeed);
                var hit = Physics2D.Raycast(bullet.position, bullet.forward, 1, model.bulletTargetLayer);

                if (hit.collider != null)
                {
                    bullets.Remove(bullet);
                    Object.Destroy(bullet.gameObject);

                    Hit?.Invoke(hit.transform);
                }
            }
        }

        public void Shoot()
        {
            var bullet = bulletFactory.CreateBullet(shootPoint.position, shootPoint.rotation, bulletParent)
                .transform;

            bullets.Add(bullet);

            Created?.Invoke(bullet);

            Object.Destroy(bullet.gameObject, 3);
        }
    }
}