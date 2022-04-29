using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BulletWeaponController : ShipWeapon, IWeaponController
    {
        private readonly BulletFactory bulletFactory;
        private readonly Transform bulletParent;
        private readonly List<Transform> bullets = new List<Transform>();
        private readonly BulletWeaponModel model;
        private readonly ScreenBoundsController screenBoundsController;

        public BulletWeaponController(BulletWeaponModel model, ShipModel shipModel,
            ScreenBoundsController screenBoundsController, Transform bulletParent) :
            base(shipModel)
        {
            this.model = model;
            this.screenBoundsController = screenBoundsController;
            this.bulletParent = bulletParent;

            bulletFactory = new BulletFactory(model.bulletPrefab);
        }

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
                }
            }
        }

        public void Shoot()
        {
            CreateBullet(shipModel.BulletShootPoint);
        }

        public void CreateBullet(Transform shootPoint)
        {
            var bullet = bulletFactory.CreateBullet(shootPoint.position, shootPoint.rotation, bulletParent)
                .transform;

            bullets.Add(bullet);
            screenBoundsController.Add(bullet);

            Object.Destroy(bullet.gameObject, 3);
        }
    }
}