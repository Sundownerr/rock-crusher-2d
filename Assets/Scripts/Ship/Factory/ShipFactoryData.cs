using UnityEngine;

namespace Game.Ship.Factory
{
    [CreateAssetMenu(fileName = "ShipFactoryData", menuName = "Data/Ship/Factory")]
    public class ShipFactoryData : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        public GameObject Prefab => prefab;
    }
}