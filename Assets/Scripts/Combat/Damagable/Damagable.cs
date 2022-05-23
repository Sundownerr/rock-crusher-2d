using System;
using Game.Combat.Interface;
using UnityEngine;

namespace Game.Combat
{
    public class Damagable : MonoBehaviour, IDamagable
    {
        public event Action Damaged;

        public void HandleDamaged()
        {
            Damaged?.Invoke();
        }
    }
}