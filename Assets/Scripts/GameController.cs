using System;
using System.Collections.Generic;
using Game.Base;
using Game.Gameplay.Utility;
using Game.Scenes;
using Game.Scenes.Interface;
using Game.UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game
{
    public class GameController : Controller<GameplayData>, IUpdate
    {
        private readonly SceneController sceneController;
        private readonly ISceneLoader sceneLoader;
        private readonly UIController uiController;
        private readonly List<IUpdate> updatees = new();
        private GameplayController gameplayController;
        private CoroutineRunner runner;

        public GameController(GameplayData model, SceneData sceneData) : base(model)
        {
            sceneController = new SceneController(sceneData);

            sceneLoader = new SceneLoader(sceneData);
            sceneLoader.GameplaySceneLoaded += OnGameplaySceneLoaded;
            sceneLoader.GameplaySceneUnloaded += OnGameplaySceneUnloaded;

            uiController = new UIController(sceneLoader, model);
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
                    sceneController.RestartGameplayScene();
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
            // runner.StopAllCoroutines();

            updatees.Remove(gameplayController);
            updatees.Remove(uiController);

            gameplayController.ShipDestroyed -= OnShipDestroyed;
            gameplayController.Destroy();
        }

        private void OnGameplaySceneLoaded()
        {
            var parentData = Object.FindObjectOfType<ParentData>();
            runner = Object.FindObjectOfType<CoroutineRunner>();

            gameplayController = new GameplayController(model, runner, parentData);
            gameplayController.CreateGameplayObjects();
            gameplayController.ShipDestroyed += OnShipDestroyed;

            updatees.Add(gameplayController);
            updatees.Add(uiController);
        }

        private void OnShipDestroyed(Transform ship)
        {
            updatees.Remove(uiController);
            sceneController.LoadGameOverScene();
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