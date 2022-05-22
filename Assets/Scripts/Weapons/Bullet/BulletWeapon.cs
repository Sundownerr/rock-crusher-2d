using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "BulletWeapon", menuName = "Data/Bullet Weapon")]
    public class BulletWeapon : ScriptableObject
    {
        [SerializeField] public GameObject bulletPrefab;
        [SerializeField] public float bulletSpeed;
        [SerializeField] public LayerMask bulletTargetLayer;
    }
}