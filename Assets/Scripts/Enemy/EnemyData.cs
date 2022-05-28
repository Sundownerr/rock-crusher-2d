using System;
using Game.Enemy.Asteroid.Factory;
using Game.Enemy.UFO.Factory;
using UnityEngine;

namespace Game.Enemy
{
    [Serializable]
    public class EnemyData
    {
        [SerializeField] private AsteroidFactoryData bigAsteroidFactoryData;
        [SerializeField] private AsteroidFactoryData mediumAsteroidFactoryData;
        [SerializeField] private AsteroidFactoryData smallAsteroidFactoryData;
        [SerializeField] private UfoFactoryData ufoFactoryData;
        [SerializeField] private float asteroidSpawnInterval;
        [SerializeField] private float asteroidSpawnRadius;
        [SerializeField] private float ufoSpawnInterval;
        [SerializeField] private float ufoSpawnRadius;

        public UfoFactoryData UfoFactoryData => ufoFactoryData;
        public AsteroidFactoryData BigAsteroidFactoryData => bigAsteroidFactoryData;
        public AsteroidFactoryData MediumAsteroidFactoryData => mediumAsteroidFactoryData;
        public AsteroidFactoryData SmallAsteroidFactoryData => smallAsteroidFactoryData;
        public float AsteroidSpawnInterval => asteroidSpawnInterval;
        public float AsteroidSpawnRadius => asteroidSpawnRadius;
        public float UfoSpawnInterval => ufoSpawnInterval;
        public float UfoSpawnRadius => ufoSpawnRadius;
    }
}