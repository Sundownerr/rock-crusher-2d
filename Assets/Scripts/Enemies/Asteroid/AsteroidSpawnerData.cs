using Game.Gameplay;
using UnityEngine;

namespace Game.Enemies.Asteroid
{
    [CreateAssetMenu(fileName = "AsteroidSpawner", menuName = "Data/AsteroidSpawner")]
    public class AsteroidSpawnerData : SpawnerData
    {
        [SerializeField] public float spawnDelay;
    }
}