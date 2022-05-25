using UnityEngine;

namespace Game.Enemy.UFO.Factory
{
    [CreateAssetMenu(fileName = "UfoFactory", menuName = "Data/UfoFactory")]
    public class UfoFactoryData : ScriptableObject
    {
        [SerializeField] private UfoMovementData movementData;
        [SerializeField] private GameObject prefab;
        [SerializeField] private float spawnInterval;
        [SerializeField] private float spawnRadius;

        public GameObject Prefab => prefab;
        public float SpawnInterval => spawnInterval;
        public UfoMovementData MovementData => movementData;
        public float SpawnRadius => spawnRadius;
    }
}