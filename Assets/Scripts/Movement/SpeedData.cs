using UnityEngine;

namespace Game.Movement
{
    [CreateAssetMenu(fileName = "SpeedData", menuName = "Data/Speed Data")]
    public class SpeedData : ScriptableObject
    {
        [SerializeField] public float acceleration;
        [SerializeField] public float deceleration;
        [SerializeField] public float maxSpeed;

        public float CurrentSpeed { get; set; }
    }
}