using Game.Gameplay;
using UnityEngine;

namespace Game.Enemies.Asteroid.Spawner
{
    [CreateAssetMenu(fileName = "AsteroidSpawner", menuName = "Data/AsteroidSpawner")]
    public class AsteroidSpawnerData : SpawnerData
    {
        [SerializeField] public float spawnDelay;
    }
}