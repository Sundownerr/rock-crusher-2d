using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "ShipMovementModel", menuName = "Models/ShipMovementModel")]
    public class ShipMovementModel : ScriptableObject
    {
        [SerializeField] public float turnSpeed;
    }
}