using Game.Ship.Weapons.Bullet;
using Game.Ship.Weapons.Laser;
using UnityEngine;

namespace Game.Ship.Weapons
{
    [CreateAssetMenu(fileName = "ShipWeaponsData", menuName = "Data/Ship/Weapons")]
    public class ShipWeaponsData : ScriptableObject
    {
        [SerializeField] private BulletWeaponData bulletWeaponData;
        [SerializeField] private LaserWeaponData laserWeaponData;

        public BulletWeaponData BulletWeaponData => bulletWeaponData;
        public LaserWeaponData LaserWeaponData => laserWeaponData;
    }
}