using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "ShipSpawnerData", menuName = "Data/ShipSpawnerData")]
    public class ShipSpawnerData : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private ShipMovement shipMovement;
        [SerializeField] private SpeedData speedData;
        [SerializeField] private PlayerInputData playerInputData;
        [SerializeField] private BulletWeapon bulletWeapon;

        public GameObject Prefab => prefab;
        public ShipMovement ShipMovement => shipMovement;
        public SpeedData SpeedData => speedData;
        public PlayerInputData PlayerInputData => playerInputData;
        public BulletWeapon BulletWeapon => bulletWeapon;
    }
}