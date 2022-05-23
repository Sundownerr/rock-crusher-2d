using Game.Base;
using Game.Enemies.Asteroid.Spawner;
using Game.Enemies.UFO.Spawner;
using Game.Gameplay.Utility;
using Game.Ship;
using Game.Ship.Spawner;
using UnityEngine;

namespace Game
{
    public class GameplayController : Controller<GameplayData>, IUpdate, IDestroyable
    {
        private readonly AsteroidSpawner asteroidSpawner;
        private readonly ParentData parentData;
        private readonly CoroutineRunner runner;
        private readonly ScreenBoundsController screenBoundsController;
        private readonly ShipSpawner shipSpawner;
        private readonly UfoSpawner ufoSpawner;
        private ShipController shipController;

        public GameplayController(GameplayData model, CoroutineRunner runner, ParentData parentData) : base(model)
        {
            this.runner = runner;
            this.parentData = parentData;

            screenBoundsController = new ScreenBoundsController(Camera.main);

            shipSpawner = new ShipSpawner(
                model.ShipSpawnerData,
                model.ShipWeaponsData,
                model.ShipMovementData,
                model.ShipSpeedData,
                model.PlayerInputData);

            asteroidSpawner = new AsteroidSpawner(
                model.AsteroidSpawnerData,
                runner,
                parentData.AsteroidParent);

            ufoSpawner = new UfoSpawner(
                model.UfoSpawnerData,
                runner,
                parentData.UfoParent);

            screenBoundsController.Add(ufoSpawner);
            screenBoundsController.Add(asteroidSpawner);
        }

        public void Destroy()
        {
            screenBoundsController.Destroy();
            shipController.Destroy();
        }

        public void Update()
        {
            screenBoundsController.Update();
            shipController.Update();
        }

        public void CreateGameplayObjects()
        {
            shipController = shipSpawner.Spawn(parentData.BulletParent, screenBoundsController, runner);

            asteroidSpawner.StartSpawn();
            ufoSpawner.StartSpawn();
        }
    }
}