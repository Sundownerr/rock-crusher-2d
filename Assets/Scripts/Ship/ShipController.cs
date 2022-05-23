using Game.Base;
using Game.Input.Interface;
using Game.Movement.Interface;
using Game.Weapons.Bullet.Interface;
using Game.Weapons.Laser;
using UnityEngine;

namespace Game.PlayerShip
{
    public class ShipController : Controller<ShipData>, IUpdate, IDestroyable
    {
        private readonly IBulletWeaponController bulletWeaponController;
        private readonly ILaserWeaponController laserWeaponController;
        private readonly IShipMovementController movementController;
        private readonly IPlayerInputController playerInputController;
        private readonly Transform shipTransform;
        private readonly ISpeedController speedController;

        public ShipController(ShipData model,
                              IShipMovementController movementController,
                              ISpeedController speedController,
                              IBulletWeaponController bulletWeaponController,
                              ILaserWeaponController laserWeaponController,
                              IPlayerInputController playerInputController) : base(model)
        {
            this.movementController = movementController;
            this.speedController = speedController;
            this.bulletWeaponController = bulletWeaponController;
            this.laserWeaponController = laserWeaponController;
            this.playerInputController = playerInputController;

            playerInputController.MovingForwardPressed += OnStartMovingPressed;
            playerInputController.MovingReleased += OnMovingReleased;
            playerInputController.ShootBulletPressed += OnShootBulletPressed;
            playerInputController.ShootLaserPressed += OnShootLaserPressed;

            shipTransform = model.transform;
        }

        public void Destroy()
        {
            playerInputController.ShootLaserPressed -= OnShootLaserPressed;
            playerInputController.ShootBulletPressed -= OnShootBulletPressed;
            playerInputController.MovingForwardPressed -= OnStartMovingPressed;
            playerInputController.MovingReleased -= OnMovingReleased;
        }

        public void Update()
        {
            playerInputController.Update();
            movementController.Update();
            speedController.Update();
            bulletWeaponController.Update();

            if (playerInputController.IsMovingForwardPressed)
            {
                speedController.Accelerate();
                movementController.Move(speedController.Speed);
            }
            else
            {
                movementController.Stop();
            }

            movementController.Turn(playerInputController.TurnDirection);

            model.X = shipTransform.position.x;
            model.Y = shipTransform.position.y;
            model.Angle = shipTransform.rotation.eulerAngles.z;
            model.Speed = speedController.Speed;
        }

        private void OnShootLaserPressed()
        {
            laserWeaponController.Shoot();

            if (laserWeaponController.CanShoot)
                model.Animator.Play(model.LaserAnimationKey);
        }

        private void OnShootBulletPressed()
        {
            bulletWeaponController.Shoot();
            model.BulletShootVFX.Play();
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