using System;
using Game.Enemy.Factory;
using Game.Enemy.Interface;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Enemy.UFO.Factory
{
    public class UfoFactory : EnemyFactory<UfoFactoryData, UfoData>
    {
        private readonly Transform target;

        public UfoFactory(UfoFactoryData model, Transform parent, Transform target) : base(model, parent)
        {
            this.target = target;
        }

        public override (IEnemy controller, UfoData model) Create(Vector3 position)
        {
            var ufoGameObject = Object.Instantiate(model.Prefab, position, Quaternion.identity, parent);

            var movementController = new UfoMovementController(
                model.MovementData,
                ufoGameObject.transform,
                target);

            var ufo = ufoGameObject.GetComponent<UfoData>();

            var controller = new UfoController(movementController);

            var result = (controller, ufo);

            Created?.Invoke(controller, ufo);

            return result;
        }

        public override event Action<IEnemy, UfoData> Created;
    }
}