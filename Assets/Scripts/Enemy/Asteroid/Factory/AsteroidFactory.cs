using System;
using Game.Base;
using Game.Enemy.Asteroid.Interface;
using Game.Enemy.Asteroid.Movement;
using Game.Enemy.Factory.Interface;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Enemy.Asteroid.Factory
{
    public class AsteroidFactory : Controller<AsteroidFactoryData>, IAsteroidFactory
    {
        private readonly Transform parent;
        private readonly Transform ship;

        public AsteroidFactory(AsteroidFactoryData model, Transform ship, Transform parent) : base(model)
        {
            this.ship = ship;
            this.parent = parent;
        }

        public event Action<(IAsteroid, AsteroidData)> Created;

        public (IAsteroid, AsteroidData) Create()
        {
            var x = Mathf.Sin(Time.time * model.SpawnRadius) * model.SpawnRadius;
            var y = Mathf.Cos(Time.time * model.SpawnRadius) * model.SpawnRadius;

            var randomOffset = new Vector3(x, y);

            var spawnPos = ship.position + randomOffset;

            return CreateAsteroid(model.PrefabBig, model.SpeedDataBig, spawnPos);
        }

        public void CreateSmall(Vector3 position)
        {
            CreateAsteroid(model.PrefabSmall, model.SpeedDataSmall, position);
        }

        public void CreateMedium(Vector3 position)
        {
            CreateAsteroid(model.PrefabMedium, model.SpeedDataMedium, position);
        }

        private (IAsteroid, AsteroidData) CreateAsteroid(GameObject prefab,
                                                         AsteroidSpeedData speedData,
                                                         Vector3 position)
        {
            var asteroid = Object.Instantiate(prefab, position, Quaternion.identity, parent);
            var data = asteroid.GetComponent<AsteroidData>();

            var movementController = new AsteroidMovementController(speedData, asteroid.transform);
            var controller = new AsteroidController(data, movementController);

            var result = (controller, data);

            Created?.Invoke(result);

            return result;
        }
    }
}