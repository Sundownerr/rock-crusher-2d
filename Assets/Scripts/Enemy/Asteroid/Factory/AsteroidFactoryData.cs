using Game.Enemy.Asteroid.Movement;
using UnityEngine;

namespace Game.Enemy.Asteroid.Factory
{
    [CreateAssetMenu(fileName = "AsteroidFactory", menuName = "Data/AsteroidFactory")]
    public class AsteroidFactoryData : ScriptableObject
    {
        [SerializeField] private AsteroidSpeedData speedDataBig;
        [SerializeField] private AsteroidSpeedData speedDataMedium;
        [SerializeField] private AsteroidSpeedData speedDataSmall;
        [SerializeField] private GameObject prefabBig;
        [SerializeField] private GameObject prefabMedium;
        [SerializeField] private GameObject prefabSmall;
        [SerializeField] private float spawnInterval;
        [SerializeField] private float spawnRadius;

        public AsteroidSpeedData SpeedDataBig => speedDataBig;

        public AsteroidSpeedData SpeedDataMedium => speedDataMedium;

        public AsteroidSpeedData SpeedDataSmall => speedDataSmall;
        public GameObject PrefabBig => prefabBig;
        public GameObject PrefabMedium => prefabMedium;
        public GameObject PrefabSmall => prefabSmall;
        public float SpawnRadius => spawnRadius;
        public float SpawnInterval => spawnInterval;
    }
}