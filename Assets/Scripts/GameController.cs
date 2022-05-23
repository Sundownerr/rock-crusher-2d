﻿using System.Collections.Generic;
using Game.Gameplay.Utility;
using Game.Scenes;
using Game.Scenes.Interface;
using Game.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameController : IUpdate
    {
        private readonly GameplayData gameplayData;
        private readonly ISceneController sceneController;
        private readonly ISceneLoader sceneLoader;
        private readonly UIController uiController;
        private readonly List<IUpdate> updatees = new List<IUpdate>();
        private GameplayController gameplayController;

        public GameController(GameplayData gameplayData, SceneData sceneData, MonoBehaviour coroutineRunner)
        {
            this.gameplayData = gameplayData;

            sceneController = new SceneController(sceneData);

            sceneLoader = new SceneLoader(sceneData);
            sceneLoader.GameplaySceneLoaded += OnGameplaySceneLoaded;
            sceneLoader.GameplaySceneUnloaded += OnGameplaySceneUnloaded;

            uiController = new UIController(sceneLoader, coroutineRunner);
            uiController.MenuUIController.PlayButtonClicked += OnPlayButtonClicked;
            uiController.MenuUIController.QuitButtonClicked += OnQuitButtonClicked;

            sceneController.LoadMenuScene();
        }

        public void Update()
        {
            for (var i = 0; i < updatees.Count; i++)
                updatees[i].Update();
        }

        private void OnGameplaySceneUnloaded()
        {
            updatees.Remove(gameplayController);
            updatees.Remove(uiController.GameplayUIController);
            gameplayController.Destroy();
        }

        private void OnGameplaySceneLoaded(Scene obj)
        {
            var parentData = Object.FindObjectOfType<ParentData>();
            var runner = Object.FindObjectOfType<CoroutineRunner>();

            gameplayController = new GameplayController(gameplayData, runner, parentData);
            gameplayController.CreateGameplayObjects();
            updatees.Add(gameplayController);

            uiController.GameplayUIController.SetShipMovemenData(gameplayData.ShipMovementData);
            uiController.GameplayUIController.SetLaserData(gameplayData.ShipWeaponsData.LaserWeaponData);

            updatees.Add(uiController.GameplayUIController);
        }

        private static void OnQuitButtonClicked()
        {
            Application.Quit();
        }

        private void OnPlayButtonClicked()
        {
            sceneController.LoadGameplayScene();
        }

        public void Destroy()
        {
            uiController.MenuUIController.PlayButtonClicked -= OnPlayButtonClicked;
            uiController.MenuUIController.QuitButtonClicked -= OnQuitButtonClicked;
            sceneLoader.GameplaySceneLoaded -= OnGameplaySceneLoaded;
            sceneLoader.GameplaySceneUnloaded -= OnGameplaySceneUnloaded;

            uiController.Destroy();
            sceneLoader.Destroy();
            gameplayController.Destroy();
        }
    }
}