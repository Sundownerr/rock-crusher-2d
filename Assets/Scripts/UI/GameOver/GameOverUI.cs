using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.GameOver
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private Button retryButton;
        [SerializeField] private Button quitButton;

        public TMP_Text ScoreText => scoreText;
        public Button RetryButton => retryButton;
        public Button QuitButton => quitButton;
    }
}