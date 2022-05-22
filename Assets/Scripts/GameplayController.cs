namespace Game
{
    public class GameplayController : IUpdate
    {
        private readonly AsteroidSpawner asteroidSpawner;
        private readonly ParentData parentData;
        private readonly ScreenBoundsController screenBoundsController;
        private readonly ShipSpawner shipSpawner;
        private readonly UfoSpawner ufoSpawner;
        private ShipController shipController;

        public GameplayController(Level model, CoroutineRunner runner, ParentData parentData)
        {
            this.parentData = parentData;

            shipSpawner = new ShipSpawner(model.shipSpawnerData);
            screenBoundsController = new ScreenBoundsController();
            asteroidSpawner = new AsteroidSpawner(model.asteroidSpawnerData, runner, parentData.AsteroidParent);
            ufoSpawner = new UfoSpawner(model.ufoSpawnerData, runner, parentData.AsteroidParent);

            screenBoundsController.Add(ufoSpawner);
            screenBoundsController.Add(asteroidSpawner);
        }

        public void Update()
        {
            screenBoundsController.Update();
            shipController.Update();
        }

        public ShipController CreateShip()
        {
            shipController = shipSpawner.Spawn(parentData.BulletParent);
            screenBoundsController.Add(shipController.Ship.transform);
            return shipController;
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