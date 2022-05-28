using System;
using Game.Base;
using Game.Enemy.Asteroid.Movement;
using UnityEngine;

namespace Game.Enemy.Asteroid.Factory
{
    [Serializable]
    public class AsteroidFactoryData : GameObjectFactoryData
    {
        [SerializeField] private AsteroidSpeedData speedData;
        public AsteroidSpeedData SpeedData => speedData;
    }
}