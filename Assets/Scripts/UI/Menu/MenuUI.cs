using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Menu
{
    public class MenuUI : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button quitButton;

        public Button PlayButton => playButton;
        public Button QuitButton => quitButton;
    }
}