using UnityEngine;

namespace Game.Ship.Movement
{
    [CreateAssetMenu(fileName = "ShipMovementData", menuName = "Data/Ship Movement")]
    public class ShipMovementData : ScriptableObject
    {
        [SerializeField] private float turnSpeed;
        [SerializeField] private float inertiaFadeMultiplier;

        public float TurnSpeed => turnSpeed;
        public float InertiaFadeMultiplier => inertiaFadeMultiplier;
        public float X { get; set; }
        public float Y { get; set; }
        public float Angle { get; set; }
        public float Speed { get; set; }
    }
}