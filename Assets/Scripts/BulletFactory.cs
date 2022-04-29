using UnityEngine;

namespace Game
{
    public class BulletFactory
    {
        private readonly GameObject bulletPrefab;

        public BulletFactory(GameObject bulletPrefab)
        {
            this.bulletPrefab = bulletPrefab;
        }

        public GameObject CreateBullet(Vector3 position, Quaternion rotation, Transform parent)
        {
            var bullet = Object.Instantiate(bulletPrefab, position, rotation, parent);
            return bullet;
        }
    }
}