using UnityEngine;

namespace Game
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Level level;
        [SerializeField] private SceneData sceneData;

        private GameController gameController;

        private void Start()
        {
            gameController = new GameController(level, sceneData, this);
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