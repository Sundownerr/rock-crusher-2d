using System;
using Game.Base;

namespace Game.UI
{
    public class MenuUIController : Controller<MenuUI>, IDestroyable
    {
        public MenuUIController(MenuUI model) : base(model)
        {
            model.PlayButton.onClick.AddListener(() =>
            {
                PlayPressed?.Invoke();

                model.PlayButton.onClick.RemoveAllListeners();
                model.QuitButton.onClick.RemoveAllListeners();
            });

            model.QuitButton.onClick.AddListener(() =>
            {
                QuitPressed?.Invoke();

                model.PlayButton.onClick.RemoveAllListeners();
                model.QuitButton.onClick.RemoveAllListeners();
            });
        }

        public void Destroy()
        {
            model.PlayButton.onClick.RemoveAllListeners();
            model.QuitButton.onClick.RemoveAllListeners();
        }

        public event Action PlayPressed;
        public event Action QuitPressed;
    }
}