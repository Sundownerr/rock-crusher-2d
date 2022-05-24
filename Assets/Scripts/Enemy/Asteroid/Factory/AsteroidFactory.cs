using System;
using Game.Base;
using Game.Enemy.Asteroid.Movement;
using Game.Enemy.Factory.Interface;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game.Enemy.Asteroid.Factory
{
    public class AsteroidFactory : Controller<AsteroidFactoryData>, IEnemyFactory
    {
        private readonly Transform parent;

        public AsteroidFactory(AsteroidFactoryData model, Transform parent) : base(model)
        {
            this.parent = parent;
        }

        public event Action<(IUpdate, Transform)> Created;

        public (IUpdate, Transform) Create()
        {
            var randomOffset = Random.insideUnitCircle * model.SpawnRadius;
            var spawnPos = Vector2.zero + randomOffset;

            var asteroid = Object.Instantiate(model.Prefab, spawnPos, Quaternion.identity, parent);

            var movementController = new AsteroidMovementController(model.SpeedData, asteroid.transform);
            var controller = new AsteroidController(movementController);

            var result = (controller, asteroid.transform);

            Created?.Invoke(result);

            return result;
        }
    }
}