using System;
using UnityEngine.SceneManagement;

namespace Game
{
    public interface ISceneLoader
    {
        event Action<Scene> GameplaySceneLoaded;
        event Action<Scene> GameplayUISceneLoaded;
        event Action<Scene> MenuUISceneLoaded;

        event Action GameplaySceneUnloaded;
        event Action GameplayUISceneUnloaded;
        event Action MenuUISceneUnoaded;
        public Scene GetActiveScene();
        public void Destroy();
    }
}