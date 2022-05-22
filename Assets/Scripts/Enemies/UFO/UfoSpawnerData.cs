using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "UfoSpawner", menuName = "Data/UfoSpawner")]
    public class UfoSpawnerData : SpawnerData
    {
        [SerializeField] public float spawnDelay;
    }
}