using System.Collections.Generic;
using Game.Enemy.Base;
using Game.Ship.Weapons.Interface;
using Game.Ship.Weapons.Laser.Interface;
using UnityEngine;

namespace Game.Combat
{
    public class WeaponHitController
    {
        private readonly List<IWeaponController> weaponControllers = new();

        public void Add(IWeaponController weaponController)
        {
            if (weaponController is ILaserWeaponController laserWeaponController)
                laserWeaponController.Hit += OnMegaHit;

            weaponController.Hit += OnHit;

            weaponControllers.Add(weaponController);
        }

        public void Destroy()
        {
            foreach (var weaponController in weaponControllers)
            {
                if (weaponController is ILaserWeaponController laserWeaponController)
                    laserWeaponController.Hit -= OnMegaHit;

                weaponController.Hit -= OnHit;
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