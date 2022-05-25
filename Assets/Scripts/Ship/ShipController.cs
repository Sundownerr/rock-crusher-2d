using Game.Base;
using Game.Input.Interface;
using Game.Movement.Interface;
using Game.Ship.Interface;
using Game.Ship.Weapons.Interface;
using UnityEngine;

namespace Game.Ship
{
    public class ShipController : Controller<ShipData>, IShipController
    {
        private readonly IShipMovementController movementController;
        private readonly IPlayerInputController playerInputController;
        private readonly IShipWeaponController shipWeaponController;

        public ShipController(ShipData model,
                              ColliderData colliderData,
                              IShipMovementController movementController,
                              IPlayerInputController playerInputController,
                              IShipWeaponController shipWeaponController) : base(model)
        {
            this.movementController = movementController;
            this.playerInputController = playerInputController;
            this.shipWeaponController = shipWeaponController;

            playerInputController.MovingForwardPressed += OnStartMovingPressed;
            playerInputController.MovingReleased += OnMovingReleased;
            playerInputController.ShootLaserPressed += OnShootLaserPressed;

            colliderData.Enter += ColliderDataOnEnter;

            void ColliderDataOnEnter(Collision2D obj)
            {
                model.IsDamaged = true;
                colliderData.Enter -= ColliderDataOnEnter;
            }
        }

        public void Destroy()
        {
            playerInputController.MovingForwardPressed -= OnStartMovingPressed;
            playerInputController.MovingReleased -= OnMovingReleased;
            playerInputController.ShootLaserPressed -= OnShootLaserPressed;

            shipWeaponController.Destroy();
        }

        public void Update()
        {
            playerInputController.Update();
            movementController.Update();
            shipWeaponController.Update();

            if (playerInputController.IsMovingForwardPressed)
                movementController.Move();
            else
                movementController.Stop();

            if (playerInputController.IsShootingBulletsPressed)
                shipWeaponController.ShootBullets();

            movementController.Turn(playerInputController.TurnDirection);
        }

        private void OnShootLaserPressed()
        {
            shipWeaponController.ShootLaser();
        }

        private void OnMovingReleased()
        {
            model.EngineVFX.Stop();
        }

        private void OnStartMovingPressed()
        {
            model.EngineVFX.Play();
        }
    }
}