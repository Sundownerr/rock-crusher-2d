using System.Collections.Generic;
using Game.Base;
using Game.Enemies.Asteroid.Spawner;
using Game.Enemies.Asteroid.Spawner.Interface;
using Game.Enemies.UFO.Spawner;
using Game.Gameplay.Utility;
using Game.Ship.Interface;
using Game.Ship.Spawner;
using Game.Ship.Spawner.Interface;
using UnityEngine;

namespace Game
{
    public class GameplayController : Controller<GameplayData>, IUpdate, IDestroyable
    {
        private readonly IEnemySpawner asteroidSpawner;
        private readonly CoroutineRunner runner;
        private readonly ScreenBoundsController screenBoundsController;
        private readonly IShipSpawner shipSpawner;
        private readonly IEnemySpawner ufoSpawner;
        private readonly List<IUpdate> updatees = new List<IUpdate>();
        private IShipController shipController;

        public GameplayController(GameplayData model, CoroutineRunner runner, ParentData parentData) : base(model)
        {
            this.runner = runner;

            screenBoundsController = new ScreenBoundsController(Camera.main);
            updatees.Add(screenBoundsController);

            shipSpawner = new ShipSpawner(
                model.ShipSpawnerData,
                model.ShipWeaponsData,
                parentData.BulletParent,
                model.ShipMovementData,
                model.ShipShipSpeedData,
                model.PlayerInputData);

            asteroidSpawner = new AsteroidSpawner(
                model.AsteroidSpawnerData,
                runner,
                parentData.AsteroidParent);

            ufoSpawner = new UfoSpawner(
                model.UfoSpawnerData,
                runner,
                parentData.UfoParent);

            shipSpawner.Created += ShipSpawnerOnCreated;
            asteroidSpawner.Created += AsteroidSpawnerOnCreated;
            ufoSpawner.Created += UfoSpawnerOnCreated;
        }

        public void Destroy()
        {
            updatees.Clear();

            screenBoundsController.Destroy();
            shipController.Destroy();
            asteroidSpawner.Destroy();

            shipSpawner.Created -= ShipSpawnerOnCreated;
            asteroidSpawner.Created -= AsteroidSpawnerOnCreated;
            ufoSpawner.Created -= UfoSpawnerOnCreated;
        }

        public void Update()
        {
            for (var i = 0; i < updatees.Count; i++)
                updatees[i].Update();
        }

        private void ShipSpawnerOnCreated((IShipController, IFactory<Transform>, Transform) spawnResult)
        {
            shipController = spawnResult.Item1;

            updatees.Add(shipController);
            screenBoundsController.Add(spawnResult.Item2);
            screenBoundsController.Add(spawnResult.Item3);
        }

        private void UfoSpawnerOnCreated((IUpdate, Transform) spawnResult)
        {
            updatees.Add(spawnResult.Item1);
            screenBoundsController.Add(spawnResult.Item2);
        }

        private void AsteroidSpawnerOnCreated((IUpdate, Transform) spawnResult)
        {
            updatees.Add(spawnResult.Item1);
            screenBoundsController.Add(spawnResult.Item2);
        }

        public void CreateGameplayObjects()
        {
            shipSpawner.Spawn(runner);
            asteroidSpawner.StartSpawn();
            ufoSpawner.StartSpawn();
        }
    }
}