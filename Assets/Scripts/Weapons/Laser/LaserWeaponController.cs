using System;
using UnityEngine;

namespace Game
{
    public class LaserWeaponController : ShipWeapon, IWeaponController
    {
        public LaserWeaponController(Transform shootPoint) : base(shootPoint)
        {
        }


        public void Update()
        {
            // throw new NotImplementedException();
        }

        public event Action<Transform> Hit;

        public void Shoot()
        {
            // throw new NotImplementedException();
        }
    }
}