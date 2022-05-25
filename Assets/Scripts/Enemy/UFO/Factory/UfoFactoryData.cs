using Game.Base;
using UnityEngine;

namespace Game.Enemy.UFO.Factory
{
    [CreateAssetMenu(fileName = "UfoFactory", menuName = "Data/UfoFactory")]
    public class UfoFactoryData : GameObjectFactoryData
    {
        [SerializeField] private UfoMovementData movementData;
        public UfoMovementData MovementData => movementData;
    }
}