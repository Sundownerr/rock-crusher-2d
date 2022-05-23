using UnityEngine;

namespace Game.Movement
{
    [CreateAssetMenu(fileName = "ShipMovementData", menuName = "Data/Ship Movement")]
    public class ShipMovementData : ScriptableObject
    {
        [SerializeField] public float turnSpeed;
    }
}