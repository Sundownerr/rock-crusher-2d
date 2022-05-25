using System;
using Game.Base;
using Game.Ship.Movement;
using Game.Ship.Weapons.Laser;

namespace Game.UI
{
    public class GameplayUIController : Controller<GameplayUI>, IUpdate
    {
        private readonly LaserWeaponData laserWeaponData;
        private readonly ShipMovementData movementData;

        public GameplayUIController(GameplayUI model, ShipMovementData movementData, LaserWeaponData laserWeaponData) :
            base(model)
        {
            this.movementData = movementData;
            this.laserWeaponData = laserWeaponData;

            UpdateShipValues();
        }

        public void Update()
        {
            UpdateShipValues();
            UpdateLaserValues();
        }

        private void UpdateLaserValues()
        {
            model.LaserCharges.text = laserWeaponData.CurrentCharges.ToString();

            model.LaserCooldown.gameObject.SetActive(laserWeaponData.CurrentCooldown > 0);

            if (model.LaserCooldown.gameObject.activeSelf)
                model.LaserCooldown.text = Math.Round(laserWeaponData.CurrentCooldown, 1).ToString();
        }

        private void UpdateShipValues()
        {
            model.ShipCoordinatesX.text = Math.Round(movementData.X, 1).ToString();
            model.ShipCoordinatesY.text = Math.Round(movementData.Y, 1).ToString();
            model.ShipTurnAngle.text = Math.Round(movementData.Angle, 0).ToString();
            model.ShipSpeed.text = Math.Round(movementData.Speed, 1).ToString();
        }
    }
}