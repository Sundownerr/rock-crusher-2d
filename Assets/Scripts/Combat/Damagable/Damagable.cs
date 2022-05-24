using Game.Combat.Interface;
using UnityEngine;

namespace Game.Combat
{
    public class Damagable : MonoBehaviour, IDamagable
    {
        public bool IsCompletlyDestroyed { get; set; }
        public bool IsDamaged { get; set; }
    }
}