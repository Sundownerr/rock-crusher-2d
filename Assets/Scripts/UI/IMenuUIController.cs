using System;

namespace Game
{
    public interface IMenuUIController : IUiController
    {
        event Action PlayButtonClicked;
        event Action QuitButtonClicked;
    }
}