using UnityEngine.SceneManagement;

namespace Game
{
    public interface IUiController
    {
        void HandleSceneLoad(Scene scene);
    }
}