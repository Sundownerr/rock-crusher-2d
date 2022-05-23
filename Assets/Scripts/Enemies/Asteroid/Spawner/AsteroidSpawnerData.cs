using Game.Movement;
using UnityEngine;

namespace Game.Enemies.Asteroid.Spawner
{
    [CreateAssetMenu(fileName = "AsteroidSpawner", menuName = "Data/AsteroidSpawner")]
    public class AsteroidSpawnerData : ScriptableObject
    {
        [SerializeField] private AsteroidSpeedData speedData;
        [SerializeField] protected GameObject prefab;
        [SerializeField] public float spawnDelay;

        public AsteroidSpeedData SpeedData => speedData;
        public GameObject Prefab => prefab;
    }
}