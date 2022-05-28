using System;
using Game.Base;
using Game.Base.Interface;
using Game.Damagables.Interface;
using Game.Input.Interface;
using Game.Ship.Movement.Interface;
using Game.Ship.Weapons.Interface;
using UnityEngine;

namespace Game.Ship
{
    public class ShipController : Controller<ShipData>, IDamagable, IDestroyable
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

            playerInputController.MovingPressed += OnStartMovingPressed;
            playerInputController.MovingReleased += OnMovingReleased;
            playerInputController.ShootLaserPressed += OnShootLaserPressed;

            colliderData.Enter += ColliderDataOnEnter;

            void ColliderDataOnEnter(Collision2D obj)
            {
                model.IsDamaged = true;
                Damaged?.Invoke();
                colliderData.Enter -= ColliderDataOnEnter;
            }
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

        public event Action Damaged;

        public void Destroy()
        {
            playerInputController.MovingPressed -= OnStartMovingPressed;
            playerInputController.MovingReleased -= OnMovingReleased;
            playerInputController.ShootLaserPressed -= OnShootLaserPressed;

            shipWeaponController.Destroy();
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