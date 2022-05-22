using System;
using UnityEngine.SceneManagement;

namespace Game
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

        public event Action<Scene> GameplaySceneLoaded;
        public event Action<Scene> GameplayUISceneLoaded;
        public event Action<Scene> MenuUISceneLoaded;
        public event Action GameplaySceneUnloaded;
        public event Action GameplayUISceneUnloaded;
        public event Action MenuUISceneUnoaded;

        public Scene GetActiveScene() => SceneManager.GetActiveScene();

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

            if (scene.name == sceneData.MenuUI)
                MenuUISceneUnoaded?.Invoke();
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == sceneData.Gameplay)
            {
                GameplaySceneLoaded?.Invoke(scene);
                return;
            }

            if (scene.name == sceneData.GameplayUI)
            {
                GameplayUISceneLoaded?.Invoke(scene);
                return;
            }

            if (scene.name == sceneData.MenuUI)
                MenuUISceneLoaded?.Invoke(scene);
        }
    }
}