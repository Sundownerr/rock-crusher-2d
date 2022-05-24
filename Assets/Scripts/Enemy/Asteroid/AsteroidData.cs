using Game.Combat;
using UnityEngine;

namespace Game.Enemy.Asteroid
{
    public class AsteroidData : Damagable
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