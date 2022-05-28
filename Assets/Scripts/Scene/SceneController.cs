using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scenes
{
    public class SceneController
    {
        private readonly List<string> loadedSceneNames = new();
        private readonly SceneData sceneData;

        public SceneController(SceneData sceneData)
        {
            this.sceneData = sceneData;
        }

        public void RestartGameplayScene()
        {
            UnloadScene(sceneData.GameoverUI);

            UnloadScene(sceneData.Gameplay);
            LoadSceneAdditive(sceneData.Gameplay, true);

            UnloadScene(sceneData.GameplayUI);
            LoadSceneAdditive(sceneData.GameplayUI);
        }

        public void LoadMenuScene()
        {
            UnloadScene(sceneData.Gameplay);
            UnloadScene(sceneData.GameplayUI);

            LoadSceneAdditive(sceneData.MenuUI, true);
        }

        public void LoadGameplayScene()
        {
            UnloadScene(sceneData.MenuUI);

            LoadSceneAdditive(sceneData.Gameplay, true);
            LoadSceneAdditive(sceneData.GameplayUI);
        }

        public void LoadGameOverScene()
        {
            LoadSceneAdditive(sceneData.GameoverUI);
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