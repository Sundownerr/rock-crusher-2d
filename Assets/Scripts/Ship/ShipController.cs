namespace Game
{
    public class ShipController : IUpdate, IDestroyable
    {
        private readonly IWeaponController laserWeaponController;
        private readonly IShipMovementController movementController;
        private readonly IPlayerInputController playerInputController;
        private readonly ISpeedController speedController;

        public ShipController(Ship ship,
                              IShipMovementController movementController,
                              ISpeedController speedController,
                              IBulletWeaponController bulletWeaponController,
                              IWeaponController laserWeaponController,
                              IPlayerInputController playerInputController)
        {
            Ship = ship;
            this.movementController = movementController;
            this.speedController = speedController;
            BulletWeaponController = bulletWeaponController;
            this.laserWeaponController = laserWeaponController;
            this.playerInputController = playerInputController;

            playerInputController.MovingForwardPressed += OnStartMovingPressed;
            playerInputController.MovingReleased += OnMovingReleased;
            playerInputController.ShootBulletPressed += OnShootBulletPressed;
            playerInputController.ShootLaserPressed += OnShootLaserPressed;
        }

        public Ship Ship { get; }

        public IBulletWeaponController BulletWeaponController { get; }

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
            BulletWeaponController.Update();
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
            BulletWeaponController.Shoot();
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