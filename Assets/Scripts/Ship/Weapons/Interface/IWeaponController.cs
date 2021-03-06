using System;
using UnityEngine;

namespace Game.Ship.Weapons.Interface
{
    public interface IWeaponController
    {
        public event Action<Transform> Hit;
        void Shoot();
    }
}