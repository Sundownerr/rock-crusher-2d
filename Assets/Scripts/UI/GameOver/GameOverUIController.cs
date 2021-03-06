using System;
using Game.Base;
using Game.Base.Interface;
using Game.Score;

namespace Game.UI.GameOver
{
    public class GameOverUIController : Controller<GameOverUI>, IDestroyable
    {
        public GameOverUIController(GameOverUI model, ScoreData scoreData) : base(model)
        {
            model.RetryButton.onClick.AddListener(() => { RetryPressed?.Invoke(); });

            model.QuitButton.onClick.AddListener(() => { QuitPressed?.Invoke(); });

            model.ScoreText.text = scoreData.CurrentScore.ToString();
        }

        public void Destroy()
        {
            model.RetryButton.onClick.RemoveAllListeners();
            model.QuitButton.onClick.RemoveAllListeners();
        }

        public event Action RetryPressed;
        public event Action QuitPressed;
    }
}