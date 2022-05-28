using Game.Enemy.Base;
using UnityEngine;

namespace Game.Enemy.Asteroid
{
    public class AsteroidData : EnemyDamagable
    {
        public enum AsteroidStage
        {
            Big,
            Medium,
            Small,
        }

        [SerializeField] private AsteroidStage stage;

        public AsteroidStage Stage => stage;
    }
}