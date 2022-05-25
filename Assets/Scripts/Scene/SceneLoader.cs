using System;
using Game.Scenes.Interface;
using UnityEngine.SceneManagement;

namespace Game.Scenes
{
    public class SceneLoader : ISceneLoader
    {
        private readonly SceneData sceneData;

        public SceneLoader(SceneData sceneData)
        {
            this.sceneData = sceneData;

            SceneManager.sceneLoaded += OnSceneLoaded;
            SceneManager.sceneUnloaded += OnSceneUnloaded;
        }

        public event Action GameplaySceneLoaded;
        public event Action GameplayUISceneLoaded;
        public event Action MenuUISceneLoaded;
        public event Action GameoverUISceneLoaded;
        public event Action GameplaySceneUnloaded;
        public event Action GameplayUISceneUnloaded;
        public event Action MenuUISceneUnoaded;
        public event Action GameoverUISceneUnoaded;

        public void Destroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
            SceneManager.sceneUnloaded -= OnSceneUnloaded;
        }

        private void OnSceneUnloaded(Scene scene)
        {
            if (scene.name == sceneData.Gameplay)
            {
                GameplaySceneUnloaded?.Invoke();
                return;
            }

            if (scene.name == sceneData.GameplayUI)
            {
                GameplayUISceneUnloaded?.Invoke();
                return;
            }

            if (scene.name == sceneData.GameoverUI)
            {
                GameoverUISceneUnoaded?.Invoke();
                return;
            }

            if (scene.name == sceneData.MenuUI)
                MenuUISceneUnoaded?.Invoke();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == sceneData.Gameplay)
            {
                GameplaySceneLoaded?.Invoke();
                return;
            }

            if (scene.name == sceneData.GameplayUI)
            {
                GameplayUISceneLoaded?.Invoke();
                return;
            }

            if (scene.name == sceneData.GameoverUI)
            {
                GameoverUISceneLoaded?.Invoke();
                return;
            }

            if (scene.name == sceneData.MenuUI)
                MenuUISceneLoaded?.Invoke();
        }
    }
}