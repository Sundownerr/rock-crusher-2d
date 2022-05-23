using System.Collections.Generic;
using Game.Scenes.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scenes
{
    public class SceneController : ISceneController
    {
        private readonly List<string> loadedSceneNames = new List<string>();
        private readonly SceneData sceneData;

        public SceneController(SceneData sceneData)
        {
            this.sceneData = sceneData;
        }

        public void RestartGameplayScene()
        {
            UnloadScene(sceneData.GameplayUI);
            LoadSceneAdditive(sceneData.GameplayUI);

            UnloadScene(sceneData.Gameplay);
            LoadSceneAdditive(sceneData.Gameplay, true);
        }

        public void LoadMenuScene()
        {
            UnloadScene(sceneData.GameplayUI);
            UnloadScene(sceneData.Gameplay);

            LoadSceneAdditive(sceneData.MenuUI, true);
        }

        public void LoadGameplayScene()
        {
            UnloadScene(sceneData.MenuUI);

            LoadSceneAdditive(sceneData.GameplayUI);
            LoadSceneAdditive(sceneData.Gameplay, true);
        }

        private void UnloadScene(string sceneName)
        {
            if (!loadedSceneNames.Contains(sceneName))
                return;

            loadedSceneNames.Remove(sceneName);
            SceneManager.UnloadSceneAsync(sceneName);
        }

        private void LoadSceneAdditive(string sceneName, bool setActive = false)
        {
            loadedSceneNames.Add(sceneName);
            var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

            if (!setActive)
                return;

            op.completed += OpOncompleted;

            void OpOncompleted(AsyncOperation obj)
            {
                SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneName));
                op.completed -= OpOncompleted;
            }
        }
    }
}