using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "BulletWeaponModel", menuName = "Models/BulletWeaponModel")]
    public class BulletWeaponModel : ScriptableObject
    {
        [SerializeField] public GameObject bulletPrefab;
        [SerializeField] public float bulletSpeed;
        [SerializeField] public LayerMask bulletTargetLayer;
    }
}