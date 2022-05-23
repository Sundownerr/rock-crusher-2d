using UnityEngine;

namespace Game.Weapons.Bullet
{
    [CreateAssetMenu(fileName = "BulletWeaponData", menuName = "Data/Bullet Weapon")]
    public class BulletWeaponData : ScriptableObject
    {
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private float bulletSpeed;
        [SerializeField] private float destroyDelay;
        [SerializeField] private LayerMask bulletTargetLayer;

        public GameObject BulletPrefab => bulletPrefab;
        public float BulletSpeed => bulletSpeed;
        public float DestroyDelay => destroyDelay;
        public LayerMask BulletTargetLayer => bulletTargetLayer;
    }
}