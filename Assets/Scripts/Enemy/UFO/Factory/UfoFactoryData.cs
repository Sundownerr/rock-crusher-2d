using System;
using Game.Base;
using Game.Enemy.UFO.Movement;
using UnityEngine;

namespace Game.Enemy.UFO.Factory
{
    [Serializable]
    public class UfoFactoryData : GameObjectFactoryData
    {
        [SerializeField] private UfoMovementData movementData;
        public UfoMovementData MovementData => movementData;
    }
}