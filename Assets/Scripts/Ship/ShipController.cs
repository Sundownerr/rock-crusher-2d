using Game.Combat.Interface;
using Game.Input.Interface;
using Game.Movement.Interface;

namespace Game.PlayerShip
{
    public class ShipController : IUpdate, IDestroyable
    {
        private readonly IWeaponController bulletWeaponController;
        private readonly IWeaponController laserWeaponController;
        private readonly IShipMovementController movementController;
        private readonly IPlayerInputController playerInputController;
        private readonly ISpeedController speedController;

        public ShipController(Ship ship,
                              IShipMovementController movementController,
                              ISpeedController speedController,
                              IWeaponController bulletWeaponController,
                              IWeaponController laserWeaponController,
                              IPlayerInputController playerInputController)
        {
            Ship = ship;
            this.movementController = movementController;
            this.speedController = speedController;
            this.bulletWeaponController = bulletWeaponController;
            this.laserWeaponController = laserWeaponController;
            this.playerInputController = playerInputController;

            playerInputController.MovingForwardPressed += OnStartMovingPressed;
            playerInputController.MovingReleased += OnMovingReleased;
            playerInputController.ShootBulletPressed += OnShootBulletPressed;
            playerInputController.ShootLaserPressed += OnShootLaserPressed;
        }

        public Ship Ship { get; }

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
            laserWeaponController.Update();

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

            Ship.Speed = speedController.Speed;
        }

        private void OnShootLaserPressed()
        {
            laserWeaponController.Shoot();
        }

        private void OnShootBulletPressed()
        {
            bulletWeaponController.Shoot();
            Ship.BulletShootVFX.Play();
        }

        private void OnMovingReleased()
        {
            Ship.EngineVFX.Stop();
        }

        private void OnStartMovingPressed()
        {
            Ship.EngineVFX.Play();
        }
    }
}