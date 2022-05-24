using UnityEngine;

namespace Game.Ship.Weapons.Bullet
{
    [CreateAssetMenu(fileName = "BulletWeaponData", menuName = "Data/Weapon/Bullet")]
    public class BulletWeaponData : ScriptableObject
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float shootInterval;
        [SerializeField] private float destroyDelay;
        [SerializeField] private LayerMask bulletTargetLayer;

        public GameObject BulletPrefab => bulletPrefab;
        public float BulletSpeed => bulletSpeed;

        public float ShootInterval => shootInterval;
        public float DestroyDelay => destroyDelay;
        public LayerMask BulletTargetLayer => bulletTargetLayer;
    }
}