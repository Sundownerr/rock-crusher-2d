using Game.Enemy.Asteroid.Movement;
using UnityEngine;

namespace Game.Enemy.Asteroid.Factory
{
    [CreateAssetMenu(fileName = "AsteroidFactory", menuName = "Data/AsteroidFactory")]
    public class AsteroidFactoryData : ScriptableObject
    {
        [SerializeField] private AsteroidSpeedData speedData;
        [SerializeField] private GameObject prefabBig;
        [SerializeField] private GameObject prefabMedium;
        [SerializeField] private GameObject prefabSmall;
        [SerializeField] private float spawnInterval;
        [SerializeField] private float spawnRadius;

        public AsteroidSpeedData SpeedData => speedData;
        public GameObject PrefabBig => prefabBig;
        public GameObject PrefabMedium => prefabMedium;
        public GameObject PrefabSmall => prefabSmall;

        public float SpawnRadius => spawnRadius;

        public float SpawnInterval => spawnInterval;
    }
}