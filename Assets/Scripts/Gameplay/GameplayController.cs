using System;
using System.Collections.Generic;
using Game.Base;
using Game.Base.Interface;
using Game.Enemy;
using Game.Enemy.Base;
using Game.Enemy.Interface;
using Game.Gameplay.Utility;
using Game.Input;
using Game.Input.Interface;
using Game.Score;
using Game.Ship;
using Game.Ship.Factory;
using Game.Ship.Factory.Interface;
using Game.Vfx;
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
        private EnemyDeathFactory enemyDeathFactory;
        private EnemyFactory enemyFactory;
        private IPlayerInputController playerInputController;
        private ScoreController scoreController;
        private ScreenBoundsController screenBoundsController;
        private ShipController shipController;
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
            enemyFactory.Destroy();
            enemyDeathFactory.Destroy();
            enemyController.Destroy();
            shipController.Destroy();
            playerInputController.Destroy();
            scoreController.Destroy();
            vfxController.Destroy();
        }

        public void Update()
        {
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
                parentData.BulletParent,
                parentData.BulletParent,
                runner,
                playerInputController);

            var shipSpawnResult = shipFactory.Create();

            shipController = shipSpawnResult.Item1;
            shipController.Damaged += OnShipDamaged;
            updatees.Add(shipController);

            shipData = shipSpawnResult.Item2;

            screenBoundsController.Add(shipData.transform);
            screenBoundsController.Add(shipSpawnResult.Item3);

            enemyFactory = new EnemyFactory(model.EnemyData, runner, parentData, shipData.transform);
            enemyFactory.Created += OnEnemyCreated;

            enemyDeathFactory = new EnemyDeathFactory(enemyFactory);

            enemyController = new EnemyController(
                enemyFactory,
                enemyDeathFactory);

            scoreController = new ScoreController(model.ScoreData, enemyDeathFactory);

            vfxController = new VfxController(model.VFXData, parentData.VFXParent, shipData, shipController,
                enemyDeathFactory);

            if (!model.SpawnEnemies)
                return;

            enemyFactory.CreateEnemies();
            updatees.Add(enemyController);
        }

        private void OnEnemyCreated(IEnemy arg1, EnemyDamagable arg2)
        {
            screenBoundsController.Add(arg2.transform);
        }

        private void OnShipDamaged()
        {
            enemyController.HandleShipDestroyed();
            enemyFactory.StopCreatingEnemies();

            updatees.Remove(playerInputController);
            updatees.Remove(shipController);

            playerInputController.Destroy();
            shipController.Destroy();

            ShipDestroyed?.Invoke(shipData.transform);
            Object.Destroy(shipData.gameObject);

            shipController.Damaged -= OnShipDamaged;
            enemyFactory.Created -= OnEnemyCreated;
        }
    }
}