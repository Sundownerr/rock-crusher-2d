using System;
using Game.Base;
using Game.Ship.Movement;
using Game.Ship.Weapons;
using UnityEngine;

namespace Game.Ship.Factory
{
    [Serializable]
    public class ShipFactoryData : GameObjectFactoryData
    {
        [SerializeField] private ShipWeaponsData shipWeaponsData;
        [SerializeField] private ShipMovementData shipMovementData;
        [SerializeField] private ShipSpeedData shipSpeedData;

        public ShipWeaponsData ShipWeaponsData => shipWeaponsData;
        public ShipMovementData ShipMovementData => shipMovementData;
        public ShipSpeedData ShipSpeedData => shipSpeedData;
    }
}