using System;
using Game.Ship.Weapons.Bullet;
using Game.Ship.Weapons.Laser;
using UnityEngine;

namespace Game.Ship.Weapons
{
    [Serializable]
    public class ShipWeaponsData
    {
        [SerializeField] private BulletWeaponData bulletWeaponData;
        [SerializeField] private LaserWeaponData laserWeaponData;

        public BulletWeaponData BulletWeaponData => bulletWeaponData;
        public LaserWeaponData LaserWeaponData => laserWeaponData;
    }
}