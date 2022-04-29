namespace Game
{
    public class ShipController : IUpdate
    {
        private readonly IWeaponController bulletWeaponController;
        private readonly IWeaponController laserWeaponController;
        private readonly IMovementController movementController;
        private readonly IPlayerInputController playerInputController;
        private readonly ISpeedController speedController;

        public ShipController(
            IMovementController movementController,
            ISpeedController speedController,
            IWeaponController bulletWeaponController,
            IWeaponController laserWeaponController,
            IPlayerInputController playerInputController)
        {
            this.movementController = movementController;
            this.speedController = speedController;
            this.bulletWeaponController = bulletWeaponController;
            this.laserWeaponController = laserWeaponController;
            this.playerInputController = playerInputController;
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
        }
    }
}