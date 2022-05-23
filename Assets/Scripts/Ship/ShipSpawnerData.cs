using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "ShipSpawnerData", menuName = "Data/ShipSpawnerData")]
    public class ShipSpawnerData : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private ShipMovementData shipMovementData;
        [SerializeField] private SpeedData speedData;
        [SerializeField] private PlayerInputData playerInputData;
        [SerializeField] private BulletWeaponData bulletWeaponData;

        public GameObject Prefab => prefab;
        public ShipMovementData ShipMovementData => shipMovementData;
        public SpeedData SpeedData => speedData;
        public PlayerInputData PlayerInputData => playerInputData;
        public BulletWeaponData BulletWeaponData => bulletWeaponData;
    }
}