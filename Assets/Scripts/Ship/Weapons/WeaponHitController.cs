using System.Collections.Generic;
using Game.Combat;
using Game.Ship.Weapons.Interface;
using Game.Ship.Weapons.Laser.Interface;
using UnityEngine;

namespace Game.Ship.Weapons
{
    public class WeaponHitController
    {
        private readonly List<IWeaponController> weaponControllers = new List<IWeaponController>();

        public void Add(IWeaponController weaponController)
        {
            weaponController.Hit += OnHit;

            if (weaponController is ILaserWeaponController laserWeaponController)
                laserWeaponController.Hit += LaserWeaponControllerOnHit;

            weaponControllers.Add(weaponController);
        }

        private void LaserWeaponControllerOnHit(Transform target)
        {
            target.GetComponent<EnemyDamagable>().IsCompletlyDestroyed = true;
        }

        public void Destroy()
        {
            foreach (var weaponController in weaponControllers)
            {
                weaponController.Hit -= OnHit;

                if (weaponController is ILaserWeaponController laserWeaponController)
                    laserWeaponController.Hit -= LaserWeaponControllerOnHit;
            }
        }

        private void OnHit(Transform target)
        {
            target.GetComponent<EnemyDamagable>().IsDamaged = true;
        }
    }
}