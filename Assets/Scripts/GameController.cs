using System;
using System.Collections.Generic;
using Game.Base;
using Game.Base.Interface;
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
        private GameplayData currentModel;
        private GameplayController gameplayController;
        private CoroutineRunner runner;

        public GameController(GameplayData model, SceneData sceneData) : base(model)
        {
            sceneController = new SceneController(sceneData);

            sceneLoader = new SceneLoader(sceneData);
            sceneLoader.GameplaySceneLoaded += OnGameplaySceneLoaded;
            sceneLoader.GameplaySceneUnloaded += OnGameplaySceneUnloaded;
            sceneLoader.GameplayUISceneLoaded += OnGameplayUISceneLoaded;
            sceneLoader.GameplayUISceneUnloaded += OnGameplayUISceneUnloaded;

            uiController = new UIController(sceneLoader, () => currentModel);
            uiController.ButtonPressed += OnButtonPressed;

            sceneController.LoadMenuScene();
        }

        public void Update()
        {
            for (var i = 0; i < updatees.Count; i++)
                updatees[i].Update();
        }

        private void OnGameplayUISceneUnloaded() => updatees.Remove(uiController);

        private void OnGameplayUISceneLoaded() => updatees.Add(uiController);

        private void OnButtonPressed(UIController.Button button)
        {
            switch (button)
            {
                case UIController.Button.Play:
                    Object.Destroy(currentModel);
                    currentModel = Object.Instantiate(model);
                    sceneController.LoadGameplayScene();
                    break;
                case UIController.Button.Retry:
                    Object.Destroy(currentModel);
                    currentModel = Object.Instantiate(model);
                    runner.StopAllCoroutines();
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
            updatees.Remove(gameplayController);
            updatees.Remove(uiController);

            gameplayController.ShipDestroyed -= OnShipDestroyed;
            gameplayController.Destroy();
        }

        private void OnGameplaySceneLoaded()
        {
            var parentData = Object.FindObjectOfType<ParentData>();
            runner = Object.FindObjectOfType<CoroutineRunner>();

            gameplayController = new GameplayController(currentModel, runner, parentData);
            gameplayController.CreateGameplayObjects();
            gameplayController.ShipDestroyed += OnShipDestroyed;

            updatees.Add(gameplayController);
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
            sceneLoader.GameplayUISceneLoaded -= OnGameplayUISceneLoaded;
            sceneLoader.GameplayUISceneUnloaded -= OnGameplayUISceneUnloaded;

            uiController.Destroy();
            sceneLoader.Destroy();
            gameplayController.Destroy();
        }
    }
}