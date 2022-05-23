using Game.Gameplay;
using UnityEngine;

namespace Game.Enemies.UFO
{
    [CreateAssetMenu(fileName = "UfoSpawner", menuName = "Data/UfoSpawner")]
    public class UfoSpawnerData : SpawnerData
    {
        [SerializeField] public float spawnDelay;
    }
}