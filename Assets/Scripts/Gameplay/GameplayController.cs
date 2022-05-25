using System;
using System.Collections.Generic;
using Game.Base;
using Game.Enemy;
using Game.Gameplay.Utility;
using Game.Input;
using Game.Input.Interface;
using Game.Ship;
using Game.Ship.Factory;
using Game.Ship.Factory.Interface;
using Game.Ship.Interface;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game
{
    public class GameplayController : Controller<GameplayData>, IUpdate, IDestroyable
    {
        private readonly ParentData parentData;
        private readonly CoroutineRunner runner;
        private readonly List<IUpdate> updatees = new();
        private EnemyController enemyController;
        private bool isShipDestroyed;
        private IPlayerInputController playerInputController;
        private ScoreController scoreController;
        private ScreenBoundsController screenBoundsController;
        private IShipController shipController;
        private ShipData shipData;
        private IShipFactory shipFactory;
        private VfxController vfxController;

        public GameplayController(GameplayData model, CoroutineRunner runner, ParentData parentData) : base(model)
        {
            this.runner = runner;
            this.parentData = parentData;
        }

        public void Destroy()
        {
            updatees.Clear();

            screenBoundsController.Destroy();
            shipController.Destroy();
            enemyController.Destroy();
            playerInputController.Destroy();
            scoreController.Destroy();
            vfxController.Destroy();
        }

        public void Update()
        {
            if (shipData.IsDamaged && !isShipDestroyed)
            {
                isShipDestroyed = true;

                enemyController.HandleShipDestroyed();
                enemyController.StopSpawn();

                updatees.Remove(playerInputController);
                updatees.Remove(shipController);

                playerInputController.Destroy();
                shipController.Destroy();

                ShipDestroyed?.Invoke(shipData.transform);
                Object.Destroy(shipData.gameObject);
            }

            for (var i = 0; i < updatees.Count; i++)
                updatees[i].Update();
        }

        public event Action<Transform> ShipDestroyed;

        public void CreateGameplayObjects()
        {
            screenBoundsController = new ScreenBoundsController(Camera.main);
            updatees.Add(screenBoundsController);

            playerInputController = new PlayerInputController(model.PlayerInputData);
            updatees.Add(playerInputController);

            shipFactory = new ShipFactory(
                model.ShipFactoryData,
                model.ShipWeaponsData,
                parentData.BulletParent,
                model.ShipMovementData,
                model.ShipShipSpeedData,
                runner,
                playerInputController);

            var shipSpawnResult = shipFactory.Create();

            shipController = shipSpawnResult.Item1;
            shipData = shipSpawnResult.Item3;

            updatees.Add(shipController);
            screenBoundsController.Add(shipSpawnResult.Item2);
            screenBoundsController.Add(shipSpawnResult.Item3.transform);

            enemyController = new EnemyController(
                model.EnemyData,
                runner,
                screenBoundsController,
                parentData,
                shipSpawnResult.Item3.transform);

            if (model.SpawnEnemies)
                enemyController.StartSpawn();

            updatees.Add(enemyController);

            scoreController = new ScoreController(model.ScoreData, enemyController);
            vfxController = new VfxController(model.VFXData, enemyController, this, parentData.VFXParent);
        }
    }
}