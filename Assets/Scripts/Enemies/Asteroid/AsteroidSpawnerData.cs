using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "AsteroidSpawner", menuName = "Data/AsteroidSpawner")]
    public class AsteroidSpawnerData : SpawnerData
    {
        [SerializeField] public float spawnDelay;
    }
}