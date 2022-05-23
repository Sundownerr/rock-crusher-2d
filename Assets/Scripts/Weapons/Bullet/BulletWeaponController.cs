using System;
using System.Collections.Generic;
using Game.Base;
using Game.Combat.Interface;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Weapons.Bullet
{
    public class BulletWeaponController : Controller<BulletWeaponData>, IWeaponController
    {
        private readonly BulletFactory bulletFactory;
        private readonly List<Transform> bullets = new List<Transform>();

        public BulletWeaponController(BulletWeaponData model, BulletFactory bulletFactory) : base(model)
        {
            this.bulletFactory = bulletFactory;
        }

        public event Action<Transform> Hit;

        public void Update()
        {
            for (var i = 0; i < bullets.Count; i++)
            {
                var bullet = bullets[i];

                if (bullet == null)
                {
                    bullets.Remove(bullet);
                    continue;
                }

                bullet.position += bullet.up * (Time.deltaTime * model.BulletSpeed);
                var hit = Physics2D.Raycast(bullet.position, bullet.forward, 1, model.BulletTargetLayer);

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
            var bullet = bulletFactory.Create();
            bullets.Add(bullet);

            Object.Destroy(bullet.gameObject, model.DestroyDelay);
        }
    }
}