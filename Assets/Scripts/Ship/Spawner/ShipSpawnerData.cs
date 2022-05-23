using UnityEngine;

namespace Game.Ship.Spawner
{
    [CreateAssetMenu(fileName = "ShipSpawnerData", menuName = "Data/Ship/Spawner")]
    public class ShipSpawnerData : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        public GameObject Prefab => prefab;
    }
}