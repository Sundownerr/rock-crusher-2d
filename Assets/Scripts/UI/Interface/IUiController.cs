using UnityEngine.SceneManagement;

namespace Game.UI.Interface
{
    public interface IUiController
    {
        void HandleSceneLoad(Scene scene);
    }
}