using Game.Base;
using Game.Input.Interface;
using Game.Movement.Interface;
using Game.Ship.Weapons.Interface;

namespace Game.Ship
{
    public class ShipController : Controller<ShipData>, IUpdate, IDestroyable
    {
        private readonly IShipMovementController movementController;
        private readonly IPlayerInputController playerInputController;
        private readonly IShipWeaponController shipWeaponController;

        public ShipController(ShipData model,
                              IShipMovementController movementController,
                              IPlayerInputController playerInputController,
                              IShipWeaponController shipWeaponController) : base(model)
        {
            this.movementController = movementController;

            this.playerInputController = playerInputController;
            this.shipWeaponController = shipWeaponController;

            playerInputController.MovingForwardPressed += OnStartMovingPressed;
            playerInputController.MovingReleased += OnMovingReleased;
            playerInputController.ShootBulletPressed += OnShootBulletPressed;
            playerInputController.ShootLaserPressed += OnShootLaserPressed;
        }

        public void Destroy()
        {
            playerInputController.MovingForwardPressed -= OnStartMovingPressed;
            playerInputController.MovingReleased -= OnMovingReleased;
            playerInputController.ShootLaserPressed -= OnShootLaserPressed;
            playerInputController.ShootBulletPressed -= OnShootBulletPressed;
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

            movementController.Turn(playerInputController.TurnDirection);
        }

        private void OnShootLaserPressed()
        {
            shipWeaponController.ShootLaser();
        }

        private void OnShootBulletPressed()
        {
            shipWeaponController.ShootBullet();
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