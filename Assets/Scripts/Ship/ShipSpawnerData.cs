using Game.Input;
using Game.Movement;
using Game.Weapons.Bullet;
using Game.Weapons.Laser;
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
        [SerializeField] private LaserWeaponData laserWeaponData;

        public GameObject Prefab => prefab;
        public ShipMovementData ShipMovementData => shipMovementData;
        public SpeedData SpeedData => speedData;
        public PlayerInputData PlayerInputData => playerInputData;
        public BulletWeaponData BulletWeaponData => bulletWeaponData;
        public LaserWeaponData LaserWeaponData => laserWeaponData;
    }
}