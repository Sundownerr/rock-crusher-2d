using System;
using System.Globalization;
using Game.PlayerShip;
using Game.UI.Interface;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class GameplayUIController : IGameplayUIController
    {
        private GameplayUI gameplayUI;
        private MonoBehaviour runner;
        private Ship ship;

        public GameplayUIController(MonoBehaviour runner)
        {
            this.runner = runner;
        }

        public void HandleSceneLoad(Scene scene)
        {
            var rootGOs = scene.GetRootGameObjects();

            foreach (var rootGO in rootGOs)
            {
                if (!rootGO.TryGetComponent<GameplayUI>(out var ui))
                    continue;

                gameplayUI = ui;
                break;
            }
        }

        public void Update()
        {
            UpdateShipValues();
        }

        public void SetShip(Ship ship)
        {
            this.ship = ship;
            UpdateShipValues();
        }

        private void UpdateShipValues()
        {
            gameplayUI.ShipCoordinatesX.text = Math.Round(ship.X, 1).ToString(CultureInfo.InvariantCulture);
            gameplayUI.ShipCoordinatesY.text = Math.Round(ship.Y, 1).ToString(CultureInfo.InvariantCulture);
            gameplayUI.ShipTurnAngle.text = Math.Round(ship.Angle, 0).ToString(CultureInfo.InvariantCulture);
            gameplayUI.ShipSpeed.text = Math.Round(ship.Speed, 1).ToString(CultureInfo.InvariantCulture);
        }
    }
}