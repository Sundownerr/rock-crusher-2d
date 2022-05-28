using System;
using UnityEngine;

namespace Game.Enemy.Asteroid.Movement
{
    [Serializable]
    public struct AsteroidSpeedData
    {
        [SerializeField] public Vector2 minMaxSpeed;
        [SerializeField] public Vector2 minMaxRotationSpeed;

        public float CurrentSpeed { get; set; }
        public float CurrentRotationSpeed { get; set; }
    }
}