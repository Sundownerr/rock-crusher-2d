using System;
using Game.Damagables;
using Game.Enemy.Interface;
using Game.Enemy.UFO.Movement;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Enemy.UFO.Factory
{
    public class UfoFactory : DamagableFactory<UfoFactoryData, IEnemy, UfoData>
    {
        private readonly Transform target;

        public UfoFactory(UfoFactoryData model, Transform parent, Transform target) : base(model, parent)
        {
            this.target = target;
        }

        public (IEnemy controller, UfoData model) Create(Vector3 position)
        {
            var ufoGameObject = Object.Instantiate(factoryData.Prefab, position, Quaternion.identity, parent);

            var movementController = new UfoMovementController(
                factoryData.MovementData,
                ufoGameObject.transform,
                target);

            var ufo = ufoGameObject.GetComponent<UfoData>();
            var controller = new UfoController(ufo, movementController);

            var result = (controller, ufo);

            Created?.Invoke(controller, ufo);

            return result;
        }

        public override event Action<IEnemy, UfoData> Created;
    }
}