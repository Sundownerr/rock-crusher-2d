﻿using System.Collections.Generic;
using Game.Base;
using Game.Enemy;
using Game.Gameplay.Utility;
using Game.Input;
using Game.Input.Interface;
using Game.Ship.Factory;
using Game.Ship.Factory.Interface;
using Game.Ship.Interface;
using UnityEngine;

namespace Game
{
    public class GameplayController : Controller<GameplayData>, IUpdate, IDestroyable
    {
        private readonly ParentData parentData;
        private readonly CoroutineRunner runner;
        private readonly List<IUpdate> updatees = new List<IUpdate>();
        private EnemyController enemyController;
        private IPlayerInputController playerInputController;
        private ScoreController scoreController;
        private ScreenBoundsController screenBoundsController;
        private IShipController shipController;
        private IShipFactory shipFactory;

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

            runner.StopAllCoroutines();
        }

        public void Update()
        {
            for (var i = 0; i < updatees.Count; i++)
                updatees[i].Update();
        }

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

            updatees.Add(shipController);
            screenBoundsController.Add(shipSpawnResult.Item2);
            screenBoundsController.Add(shipSpawnResult.Item3);

            enemyController = new EnemyController(
                runner,
                screenBoundsController,
                model.AsteroidFactoryData,
                model.UfoFactoryData,
                parentData,
                shipSpawnResult.Item3);

            updatees.Add(enemyController);

            scoreController = new ScoreController(model.ScoreData, enemyController);

            enemyController.StartSpawn();
        }
    }
}