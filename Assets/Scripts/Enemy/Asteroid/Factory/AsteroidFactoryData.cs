using Game.Base;
using Game.Enemy.Asteroid.Movement;
using UnityEngine;

namespace Game.Enemy.Asteroid.Factory
{
    [CreateAssetMenu(fileName = "AsteroidFactory", menuName = "Data/AsteroidFactory")]
    public class AsteroidFactoryData : GameObjectFactoryData
    {
        [SerializeField] private AsteroidSpeedData speedData;
        public AsteroidSpeedData SpeedData => speedData;
    }
}