using System.Collections.Generic;
using Game.Combat.Interface;
using UnityEngine;

namespace Game.Combat
{
    public class WeaponHitController
    {
        private readonly List<IWeaponController> weaponControllers = new List<IWeaponController>();

        public void Add(IWeaponController weaponController)
        {
            weaponController.Hit += OnHit;

            weaponControllers.Add(weaponController);
        }

        public void Destroy()
        {
            foreach (var weaponController in weaponControllers)
                weaponController.Hit -= OnHit;
        }

        private void OnHit(Transform obj)
        {
            obj.GetComponent<Damagable>().HandleDamaged();
        }
    }
}