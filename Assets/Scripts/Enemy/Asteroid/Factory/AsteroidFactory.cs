using System;
using Game.Enemy.Asteroid.Movement;
using Game.Enemy.Factory;
using Game.Enemy.Interface;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Enemy.Asteroid.Factory
{
    public class AsteroidFactory : EnemyFactory<AsteroidFactoryData, AsteroidData>
    {
        public AsteroidFactory(AsteroidFactoryData model, Transform parent) : base(model, parent)
        { }

        public override (IEnemy controller, AsteroidData model) Create(Vector3 position)
        {
            var instancedPrefab = Object.Instantiate(model.Prefab, position, Quaternion.identity, parent);
            var asteroid = instancedPrefab.GetComponent<AsteroidData>();
            var movementController = new AsteroidMovementController(model.SpeedData, instancedPrefab.transform);
            var controller = new AsteroidController(asteroid, movementController);

            Created?.Invoke(controller, asteroid);
            return (controller, asteroid);
        }

        public override event Action<IEnemy, AsteroidData> Created;
    }
}