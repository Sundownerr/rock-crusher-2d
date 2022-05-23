using UnityEngine;

namespace Game.Ship.Movement
{
    [CreateAssetMenu(fileName = "ShipSpeedData", menuName = "Data/Ship/Speed Data")]
    public class ShipSpeedData : ScriptableObject
    {
        [SerializeField] public float acceleration;
        [SerializeField] public float deceleration;
        [SerializeField] public float maxSpeed;

        public float CurrentSpeed { get; set; }
    }
}