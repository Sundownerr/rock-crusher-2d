using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "BulletWeaponData", menuName = "Data/Bullet Weapon")]
    public class BulletWeaponData : ScriptableObject
    {
        [SerializeField] public GameObject bulletPrefab;
        [SerializeField] public float bulletSpeed;
        [SerializeField] public LayerMask bulletTargetLayer;
    }
}