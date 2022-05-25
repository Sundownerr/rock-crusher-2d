using System;
using Game.Base;
using Game.Enemy.Interface;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game.Enemy.UFO.Factory
{
    public class UfoFactory : Controller<UfoFactoryData>, IUfoFactory
    {
        private readonly Transform parent;
        private readonly Transform target;

        public UfoFactory(UfoFactoryData model, Transform parent, Transform target) : base(model)
        {
            this.parent = parent;
            this.target = target;
        }

        public event Action<(IEnemy, UfoData)> Created;

        public (IEnemy, UfoData) Create()
        {
            var randomOffset = Random.insideUnitCircle * model.SpawnRadius;
            var spawnPos = Vector2.zero + randomOffset;

            return CreateUfo(spawnPos);
        }

        private (IEnemy, UfoData) CreateUfo(Vector3 position)
        {
            var ufoGameObject = Object.Instantiate(model.Prefab, position, Quaternion.identity, parent);

            var movementController = new UfoMovementController(
                model.MovementData,
                ufoGameObject.transform,
                target);

            var ufo = ufoGameObject.GetComponent<UfoData>();

            var controller = new UfoController(movementController);

            var result = (controller, ufo);

            Created?.Invoke(result);

            return result;
        }
    }
}