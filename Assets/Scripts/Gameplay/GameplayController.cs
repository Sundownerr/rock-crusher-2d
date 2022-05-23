using Game.Base;
using Game.Enemies.Asteroid;
using Game.Enemies.UFO;
using Game.Gameplay;
using Game.Gameplay.Utility;
using Game.PlayerShip;

namespace Game
{
    public class GameplayController : Controller<GameplayData>, IUpdate
    {
        private readonly AsteroidSpawner asteroidSpawner;
        private readonly ParentData parentData;
        private readonly ScreenBoundsController screenBoundsController;
        private readonly ShipSpawner shipSpawner;
        private readonly UfoSpawner ufoSpawner;
        private ShipController shipController;

        public GameplayController(GameplayData model, CoroutineRunner runner, ParentData parentData) : base(model)
        {
            this.parentData = parentData;

            screenBoundsController = new ScreenBoundsController();
            shipSpawner = new ShipSpawner(model.shipSpawnerData);
            asteroidSpawner = new AsteroidSpawner(model.asteroidSpawnerData, runner, parentData.AsteroidParent);
            ufoSpawner = new UfoSpawner(model.ufoSpawnerData, runner, parentData.UfoParent);

            screenBoundsController.Add(ufoSpawner);
            screenBoundsController.Add(asteroidSpawner);
        }

        public void Update()
        {
            screenBoundsController.Update();
            shipController.Update();
        }

        public Ship CreateShip()
        {
            shipController = shipSpawner.Spawn(parentData.BulletParent, screenBoundsController);

            return shipController.Ship;
        }

        public void CreateGameplayObjects()
        {
            asteroidSpawner.StartSpawn();
            ufoSpawner.StartSpawn();
        }

        public void Destroy()
        {
            screenBoundsController.Destroy();
            shipSpawner.Destroy();
        }
    }
}