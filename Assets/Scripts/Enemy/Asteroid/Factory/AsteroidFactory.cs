﻿using System;
using Game.Base;
using Game.Enemy.Asteroid.Interface;
using Game.Enemy.Asteroid.Movement;
using Game.Enemy.Factory.Interface;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game.Enemy.Asteroid.Factory
{
    public class AsteroidFactory : Controller<AsteroidFactoryData>, IAsteroidFactory
    {
        private readonly Transform parent;

        public AsteroidFactory(AsteroidFactoryData model, Transform parent) : base(model)
        {
            this.parent = parent;
        }

        public event Action<(IAsteroid, AsteroidData)> Created;

        public (IAsteroid, AsteroidData) Create()
        {
            var randomOffset = Random.insideUnitCircle * model.SpawnRadius;
            var spawnPos = Vector2.zero + randomOffset;

            return CreateAsteroid(model.PrefabBig, spawnPos);
        }

        public void CreateSmall(Vector3 position)
        {
            CreateAsteroid(model.PrefabSmall, position);
        }

        public void CreateMedium(Vector3 position)
        {
            CreateAsteroid(model.PrefabMedium, position);
        }

        private (IAsteroid, AsteroidData) CreateAsteroid(GameObject prefab, Vector3 position)
        {
            var asteroid = Object.Instantiate(prefab, position, Quaternion.identity, parent);
            var data = asteroid.GetComponent<AsteroidData>();

            var movementController = new AsteroidMovementController(model.SpeedData, asteroid.transform);
            var controller = new AsteroidController(data, movementController);

            var result = (controller, data);

            Created?.Invoke(result);

            return result;
        }
    }
}