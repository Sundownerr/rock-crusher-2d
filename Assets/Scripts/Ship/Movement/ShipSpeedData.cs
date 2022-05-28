using System;
using UnityEngine;

namespace Game.Ship.Movement
{
    [Serializable]
    public class ShipSpeedData
    {
        [SerializeField] public float acceleration;
        [SerializeField] public float deceleration;
        [SerializeField] public float maxSpeed;

        public float CurrentSpeed { get; set; }
    }
}