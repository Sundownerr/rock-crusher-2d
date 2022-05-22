using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "ShipMovement", menuName = "Data/Ship Movement")]
    public class ShipMovement : ScriptableObject
    {
        [SerializeField] public float turnSpeed;
    }
}