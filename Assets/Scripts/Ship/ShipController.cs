namespace Game
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

            playerInputController.StartedMoving += OnStartMoving;
            playerInputController.EndedMoving += OnEndedMoving;
            playerInputController.ShootedBullet += OnShootedBullet;
        }

        public Ship Ship { get; }

        public void Destroy()
        {
            playerInputController.ShootedBullet -= OnShootedBullet;
            playerInputController.StartedMoving -= OnStartMoving;
            playerInputController.EndedMoving -= OnEndedMoving;
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

            if (playerInputController.IsShootBulletPressed)
                bulletWeaponController.Shoot();

            if (playerInputController.IsShootLaserPressed)
                laserWeaponController.Shoot();

            Ship.Speed = speedController.Speed;
        }

        private void OnShootedBullet()
        {
            Ship.BulletShootVFX.Play();
        }

        private void OnEndedMoving()
        {
            Ship.EngineVFX.Stop();
        }

        private void OnStartMoving()
        {
            Ship.EngineVFX.Play();
        }
    }
}