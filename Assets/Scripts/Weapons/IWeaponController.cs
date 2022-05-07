using System;
using UnityEngine;

namespace Game
{
    public interface IWeaponController : IUpdate
    {
        public event Action<Transform> Hit;
        void Shoot();
    }
}