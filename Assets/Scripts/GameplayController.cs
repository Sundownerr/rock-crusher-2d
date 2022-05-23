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

        public GameplayController(GameplayData model, CoroutineRunner runner, ParentData parentData)
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

        public ShipController CreateShip()
        {
            shipController = shipSpawner.Spawn(parentData.BulletParent);
            screenBoundsController.Add(shipController.Ship.transform);
            screenBoundsController.Add(shipController.BulletWeaponController);

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