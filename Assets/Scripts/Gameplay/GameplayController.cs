using System.Collections.Generic;
using Game.Base;
using Game.Enemy;
using Game.Gameplay.Utility;
using Game.Ship.Factory;
using Game.Ship.Factory.Interface;
using Game.Ship.Interface;
using UnityEngine;

namespace Game
{
    public class GameplayController : Controller<GameplayData>, IUpdate, IDestroyable
    {
        private readonly EnemyController enemyController;

        private readonly CoroutineRunner runner;
        private readonly ScreenBoundsController screenBoundsController;
        private readonly IShipFactory shipFactory;
        private readonly List<IUpdate> updatees = new List<IUpdate>();

        private IShipController shipController;

        public GameplayController(GameplayData model, CoroutineRunner runner, ParentData parentData) : base(model)
        {
            this.runner = runner;

            screenBoundsController = new ScreenBoundsController(Camera.main);
            updatees.Add(screenBoundsController);

            shipFactory = new ShipFactory(
                model.ShipFactoryData,
                model.ShipWeaponsData,
                parentData.BulletParent,
                model.ShipMovementData,
                model.ShipShipSpeedData,
                model.PlayerInputData,
                runner);

            enemyController = new EnemyController(
                runner, screenBoundsController,
                model.AsteroidFactoryData,
                model.UfoFactoryData,
                parentData);

            updatees.Add(enemyController);
        }

        public void Destroy()
        {
            updatees.Clear();

            screenBoundsController.Destroy();
            shipController.Destroy();
            enemyController.Destroy();

            runner.StopAllCoroutines();
        }

        public void Update()
        {
            for (var i = 0; i < updatees.Count; i++)
                updatees[i].Update();
        }

        public void CreateGameplayObjects()
        {
            var shipSpawnResult = shipFactory.Create();

            shipController = shipSpawnResult.Item1;

            updatees.Add(shipController);
            screenBoundsController.Add(shipSpawnResult.Item2);
            screenBoundsController.Add(shipSpawnResult.Item3);

            enemyController.StartSpawn();
        }
    }
}