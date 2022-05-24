using UnityEngine;

namespace Game.Combat
{
    public class EnemyDamagable : Damagable
    {
        [SerializeField] private int score;

        public int Score => score;
        public bool IsCompletlyDestroyed { get; set; }
    }
}