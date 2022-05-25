using System;
using System.Collections.Generic;
using Game.Gameplay.Utility;
using Game.Scenes;
using Game.Scenes.Interface;
using Game.UI;
using UnityEngine;
using Object = UnityEngine.Object;

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

            uiController = new UIController(sceneLoader, gameplayData);
            uiController.ButtonPressed += OnButtonPressed;

            sceneController.LoadMenuScene();
        }

        public void Update()
        {
            for (var i = 0; i < updatees.Count; i++)
                updatees[i].Update();
        }

        private void OnButtonPressed(UIController.Button button)
        {
            switch (button)
            {
                case UIController.Button.Play:
                    sceneController.LoadGameplayScene();
                    break;
                case UIController.Button.Retry:
                    break;
                case UIController.Button.Quit:
                    Application.Quit();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(button), button, null);
            }
        }

        private void OnGameplaySceneUnloaded()
        {
            updatees.Remove(gameplayController);
            updatees.Remove(uiController);
            gameplayController.Destroy();
        }

        private void OnGameplaySceneLoaded()
        {
            var parentData = Object.FindObjectOfType<ParentData>();
            var runner = Object.FindObjectOfType<CoroutineRunner>();

            gameplayController = new GameplayController(gameplayData, runner, parentData);
            gameplayController.CreateGameplayObjects();

            updatees.Add(gameplayController);
            updatees.Add(uiController);
        }

        public void Destroy()
        {
            uiController.ButtonPressed -= OnButtonPressed;
            sceneLoader.GameplaySceneLoaded -= OnGameplaySceneLoaded;
            sceneLoader.GameplaySceneUnloaded -= OnGameplaySceneUnloaded;

            uiController.Destroy();
            sceneLoader.Destroy();
            gameplayController.Destroy();
        }
    }
}