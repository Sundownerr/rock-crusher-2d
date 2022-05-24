using Game.Gameplay;
using UnityEngine;

namespace Game.Enemy.UFO.Factory
{
    [CreateAssetMenu(fileName = "UfoFactory", menuName = "Data/UfoFactory")]
    public class UfoFactoryData : FactoryData
    {
        [SerializeField] private float spawnInterval;

        public float SpawnInterval => spawnInterval;
    }
}