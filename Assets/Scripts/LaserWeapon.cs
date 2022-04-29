using System;
using UnityEngine;

namespace Game
{
    public class LaserWeapon : ShipWeapon, IUpdate, IShipWeapon
    {
        public LaserWeapon(Transform shootPoint) : base(shootPoint)
        {
        }

        public void Shoot()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            throw new NotImplementedException();
        }
    }
}