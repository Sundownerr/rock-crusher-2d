using System.Collections.Generic;
using Game.Combat;
using Game.Ship.Weapons.Interface;
using Game.Ship.Weapons.Laser.Interface;
using UnityEngine;

namespace Game.Ship.Weapons
{
    public class WeaponHitController
    {
        private readonly List<IWeaponController> weaponControllers = new();

        public void Add(IWeaponController weaponController)
        {
            weaponController.Hit += OnHit;

            if (weaponController is ILaserWeaponController laserWeaponController)
                laserWeaponController.Hit += OnMegaHit;

            weaponControllers.Add(weaponController);
        }

        public void Destroy()
        {
            foreach (var weaponController in weaponControllers)
            {
                weaponController.Hit -= OnHit;

                if (weaponController is ILaserWeaponController laserWeaponController)
                    laserWeaponController.Hit -= OnMegaHit;
            }
        }

        private void OnMegaHit(Transform target)
        {
            var enemy = target.GetComponent<EnemyDamagable>();
            enemy.IsCompletlyDestroyed = true;
        }

        private void OnHit(Transform target)
        {
            target.GetComponent<EnemyDamagable>().IsDamaged = true;
        }
    }
}