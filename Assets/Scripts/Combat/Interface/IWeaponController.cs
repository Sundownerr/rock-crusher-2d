using System;
using UnityEngine;

namespace Game.Combat.Interface
{
    public interface IWeaponController : IUpdate
    {
        public event Action<Transform> Hit;
        void Shoot();
    }
}