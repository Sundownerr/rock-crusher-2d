using Game.Weapons.Bullet;
using Game.Weapons.Laser;
using UnityEngine;

namespace Game.PlayerShip
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