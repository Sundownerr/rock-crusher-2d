using Game.Scenes;
using UnityEngine;

namespace Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameplayData gameplayData;
        [SerializeField] private SceneData sceneData;

        private GameController gameController;

        private void Awake()
        {
            Application.targetFrameRate = Screen.currentResolution.refreshRate;
            Screen.SetResolution(1280, 720, FullScreenMode.Windowed);
        }

        private void Start()
        {
            gameController = new GameController(gameplayData, sceneData);
        }

        private void Update()
        {
            gameController.Update();
        }

        private void OnDestroy()
        {
            gameController.Destroy();
        }
    }
}