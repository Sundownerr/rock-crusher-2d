using System;
using System.Globalization;
using Game.Ship.Movement;
using Game.Ship.Weapons.Laser;
using Game.UI.Interface;
using UnityEngine.SceneManagement;

namespace Game.UI
{
    public class GameplayUIController : IGameplayUIController
    {
        private GameplayUI gameplayUI;
        private LaserWeaponData laserData;
        private ShipMovementData shipMovementData;

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
            UpdateLaserValues();
        }

        public void SetShipMovemenData(ShipMovementData data)
        {
            shipMovementData = data;
            UpdateShipValues();
        }

        public void SetLaserData(LaserWeaponData data)
        {
            laserData = data;
            UpdateLaserValues();
        }

        private void UpdateLaserValues()
        {
            gameplayUI.LaserCharges.text = laserData.CurrentCharges.ToString(CultureInfo.InvariantCulture);
            gameplayUI.LaserCooldown.text =
                Math.Round(laserData.CurrentCooldown, 1).ToString(CultureInfo.InvariantCulture);
        }

        private void UpdateShipValues()
        {
            gameplayUI.ShipCoordinatesX.text = Math.Round(shipMovementData.X, 1).ToString(CultureInfo.InvariantCulture);
            gameplayUI.ShipCoordinatesY.text = Math.Round(shipMovementData.Y, 1).ToString(CultureInfo.InvariantCulture);
            gameplayUI.ShipTurnAngle.text =
                Math.Round(shipMovementData.Angle, 0).ToString(CultureInfo.InvariantCulture);
            gameplayUI.ShipSpeed.text = Math.Round(shipMovementData.Speed, 1).ToString(CultureInfo.InvariantCulture);
        }
    }
}