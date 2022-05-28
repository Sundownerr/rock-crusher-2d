using System;
using Game.Base.Interface;
using Game.Scenes.Interface;
using Game.UI.GameOver;
using Game.UI.Gameplay;
using Game.UI.Menu;
using Object = UnityEngine.Object;

namespace Game.UI
{
    public class UIController : IDestroyable, IUpdate
    {
        public enum Button
        {
            Play,
            Retry,
            Quit,
        }

        private readonly Func<GameplayData> getGameplayData;

        private readonly ISceneLoader sceneLoader;
        private GameOverUIController gameOverUIController;
        private GameplayUIController gameplayUIController;
        private MenuUIController menuUIController;

        public UIController(ISceneLoader sceneLoader, Func<GameplayData> getGameplayData)
        {
            this.sceneLoader = sceneLoader;
            this.getGameplayData = getGameplayData;

            sceneLoader.GameplayUISceneLoaded += OnGameplayUISceneLoaded;
            sceneLoader.GameoverUISceneLoaded += OnGameoverUISceneLoaded;
            sceneLoader.GameoverUISceneUnoaded += OnGameoverUISceneUnoaded;
            sceneLoader.MenuUISceneLoaded += OnMenuUISceneLoaded;
            sceneLoader.MenuUISceneUnoaded += OnMenuUISceneUnoaded;
        }

        private GameplayData gameplayData => getGameplayData();

        public void Destroy()
        {
            sceneLoader.GameplayUISceneLoaded -= OnGameplayUISceneLoaded;
            sceneLoader.GameoverUISceneLoaded -= OnGameoverUISceneLoaded;
            sceneLoader.GameoverUISceneUnoaded -= OnGameoverUISceneUnoaded;
            sceneLoader.MenuUISceneLoaded -= OnMenuUISceneLoaded;
            sceneLoader.MenuUISceneUnoaded -= OnMenuUISceneUnoaded;

            menuUIController.Destroy();
            gameOverUIController.Destroy();
        }

        public void Update()
        {
            gameplayUIController.Update();
        }

        private void OnGameoverUISceneUnoaded()
        {
            gameOverUIController.RetryPressed -= OnRetryPressed;
            gameOverUIController.QuitPressed -= OnQuitPressed;
            gameOverUIController.Destroy();
        }

        private void OnGameoverUISceneLoaded()
        {
            var model = Object.FindObjectOfType<GameOverUI>();
            gameOverUIController = new GameOverUIController(model, gameplayData.ScoreData);

            gameOverUIController.RetryPressed += OnRetryPressed;
            gameOverUIController.QuitPressed += OnQuitPressed;
        }

        private void OnRetryPressed()
        {
            ButtonPressed?.Invoke(Button.Retry);
        }

        public event Action<Button> ButtonPressed;

        private void OnMenuUISceneUnoaded()
        {
            menuUIController.PlayPressed -= OnPlayPressed;
            menuUIController.QuitPressed -= OnQuitPressed;
            menuUIController.Destroy();
        }

        private void OnMenuUISceneLoaded()
        {
            var model = Object.FindObjectOfType<MenuUI>();
            menuUIController = new MenuUIController(model);

            menuUIController.PlayPressed += OnPlayPressed;
            menuUIController.QuitPressed += OnQuitPressed;
        }

        private void OnQuitPressed()
        {
            ButtonPressed?.Invoke(Button.Quit);
        }

        private void OnPlayPressed()
        {
            ButtonPressed?.Invoke(Button.Play);
        }

        private void OnGameplayUISceneLoaded()
        {
            var model = Object.FindObjectOfType<GameplayUI>();

            gameplayUIController = new GameplayUIController(
                model,
                gameplayData.ShipFactoryData.ShipMovementData,
                gameplayData.ShipFactoryData.ShipWeaponsData.LaserWeaponData);
        }
    }
}