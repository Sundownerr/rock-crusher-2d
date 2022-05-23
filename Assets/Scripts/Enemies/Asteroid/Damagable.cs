using System;
using UnityEngine;

namespace Game
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