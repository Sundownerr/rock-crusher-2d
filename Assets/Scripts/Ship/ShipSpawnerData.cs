using Game.Input;
using Game.Movement;
using Game.Weapons.Bullet;
using UnityEngine;

namespace Game.PlayerShip
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