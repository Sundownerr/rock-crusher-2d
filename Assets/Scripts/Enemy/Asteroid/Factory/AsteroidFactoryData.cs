using Game.Enemy.Asteroid.Movement;
using UnityEngine;

namespace Game.Enemy.Asteroid.Factory
{
    [CreateAssetMenu(fileName = "AsteroidFactory", menuName = "Data/AsteroidFactory")]
    public class AsteroidFactoryData : ScriptableObject
    {
        [SerializeField] private AsteroidSpeedData speedData;
        [SerializeField] private GameObject prefab;
        [SerializeField] private float spawnInterval;
        [SerializeField] private float spawnRadius;

        public AsteroidSpeedData SpeedData => speedData;
        public GameObject Prefab => prefab;

        public float SpawnRadius => spawnRadius;

        public float SpawnInterval => spawnInterval;
    }
}