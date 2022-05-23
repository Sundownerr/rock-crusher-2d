using System;
using Game.UI.Interface;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Game.UI
{
    public class MenuUIController : IMenuUIController
    {
        public void HandleSceneLoad(Scene scene)
        {
            var menuUI = Object.FindObjectOfType<MenuUI>();
            var playButton = menuUI.PlayButton;
            var quitButton = menuUI.QuitButton;

            playButton.onClick.AddListener(() =>
            {
                PlayButtonClicked?.Invoke();

                playButton.onClick.RemoveAllListeners();
                quitButton.onClick.RemoveAllListeners();
            });

            quitButton.onClick.AddListener(() =>
            {
                QuitButtonClicked?.Invoke();

                playButton.onClick.RemoveAllListeners();
                quitButton.onClick.RemoveAllListeners();
            });
        }

        public event Action PlayButtonClicked;
        public event Action QuitButtonClicked;
    }
}