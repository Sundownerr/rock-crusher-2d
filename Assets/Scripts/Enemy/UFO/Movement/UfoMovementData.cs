using System;
using UnityEngine;

namespace Game.Enemy.UFO.Movement
{
    [Serializable]
    public struct UfoMovementData
    {
        [SerializeField] private float speed;

        public float Speed => speed;
    }
}