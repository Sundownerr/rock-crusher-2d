using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class BulletWeaponModel : MonoBehaviour
    {
        [SerializeField] public GameObject bulletPrefab;
        [SerializeField] public float bulletSpeed;
        [SerializeField] public LayerMask bulletTargetLayer;
    }

    public class BulletWeapon : ShipWeapon, IUpdate, IShipWeapon
    {
        private readonly BulletFactory bulletFactory;
        private readonly List<Transform> bullets = new List<Transform>();
        private readonly BulletWeaponModel model;

        public BulletWeapon(Transform shootPoint, BulletWeaponModel model) : base(shootPoint)
        {
            this.model = model;

            bulletFactory = new BulletFactory(model.bulletPrefab);
        }

        public void Shoot()
        {
            CreateBullet(shootPoint);
        }

        public void Update()
        {
            for (var index = 0; index < bullets.Count; index++)
            {
                var bullet = bullets[index];

                bullet.position += bullet.forward * Time.deltaTime * model.bulletSpeed;
                var hit = Physics2D.Raycast(bullet.position, bullet.forward, 1, model.bulletTargetLayer);

                if (hit.collider != null)
                {
                    bullets.Remove(bullet);
                    Object.Destroy(bullet.gameObject);
                }
            }
        }

        public void CreateBullet(Transform shootPoint)
        {
            var bullet = bulletFactory.CreateBullet(shootPoint.position, shootPoint.rotation, shootPoint)
                .transform;

            bullets.Add(bullet);
        }
    }
}