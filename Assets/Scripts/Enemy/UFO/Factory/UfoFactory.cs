using System;
using Game.Base;
using Game.Enemy.Interface;
using UnityEngine;
using Object = UnityEngine.Object;

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
            var x = Mathf.Sin(Time.time * model.SpawnRadius) * model.SpawnRadius;
            var y = Mathf.Cos(Time.time * model.SpawnRadius) * model.SpawnRadius;

            var randomOffset = new Vector3(x, y);

            var spawnPos = target.position + randomOffset;

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