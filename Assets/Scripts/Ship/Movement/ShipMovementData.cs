using System;
using UnityEngine;

namespace Game.Ship.Movement
{
    [Serializable]
    public class ShipMovementData
    {
        [SerializeField] private float turnSpeed;
        [SerializeField] private float inertiaFadeMultiplier;

        public float TurnSpeed => turnSpeed;
        public float InertiaFadeMultiplier => inertiaFadeMultiplier;
        public float X { get; set; }
        public float Y { get; set; }
        public float Angle { get; set; }
        public float Inertia { get; set; }
    }
}