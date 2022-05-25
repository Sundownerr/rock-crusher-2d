using Game.Enemy.Asteroid;
using Game.Enemy.Asteroid.Factory;
using Game.Enemy.Factory;
using Game.Enemy.UFO;
using Game.Enemy.UFO.Factory;
using UnityEngine;

namespace Game.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "Data/Enemy Data")]
    public class EnemyData : ScriptableObject
    {
        [SerializeField] private AsteroidFactoryData bigAsteroidFactoryData;
        [SerializeField] private AsteroidFactoryData mediumAsteroidFactoryData;
        [SerializeField] private AsteroidFactoryData smallAsteroidFactoryData;
        [SerializeField] private UfoFactoryData ufoFactoryData;
        [SerializeField] private float asteroidSpawnInterval;
        [SerializeField] private float asteroidSpawnRadius;
        [SerializeField] private float ufoSpawnInterval;
        [SerializeField] private float ufoSpawnRadius;

        public EnemyFactory<AsteroidFactoryData, AsteroidData> BigAsteroidFactory { get; set; }
        public EnemyFactory<AsteroidFactoryData, AsteroidData> MediumAsteroidFactory { get; set; }
        public EnemyFactory<AsteroidFactoryData, AsteroidData> SmallAsteroidFactory { get; set; }
        public EnemyFactory<UfoFactoryData, UfoData> UfoFactory { get; set; }

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