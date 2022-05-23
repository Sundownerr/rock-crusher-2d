using System;

namespace Game.UI.Interface
{
    public interface IMenuUIController : IUiController
    {
        event Action PlayButtonClicked;
        event Action QuitButtonClicked;
    }
}