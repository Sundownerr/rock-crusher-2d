﻿using Game.Scenes.Interface;
using Game.UI.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class UIController
    {
        private readonly ISceneLoader sceneLoader;

        public UIController(ISceneLoader sceneLoader, MonoBehaviour runner)
        {
            this.sceneLoader = sceneLoader;

            MenuUIController = new MenuUIController();
            GameplayUIController = new GameplayUIController();

            sceneLoader.GameplayUISceneLoaded += OnGameplayUISceneLoaded;
            sceneLoader.MenuUISceneLoaded += OnMenuUISceneLoaded;
        }

        public IMenuUIController MenuUIController { get; }
        public IGameplayUIController GameplayUIController { get; }

        private void OnMenuUISceneLoaded(Scene scene)
        {
            MenuUIController.HandleSceneLoad(scene);
        }

        private void OnGameplayUISceneLoaded(Scene scene)
        {
            GameplayUIController.HandleSceneLoad(scene);
        }

        public void Destroy()
        {
            sceneLoader.GameplayUISceneLoaded -= OnGameplayUISceneLoaded;
            sceneLoader.MenuUISceneLoaded -= OnMenuUISceneLoaded;
        }
    }
}