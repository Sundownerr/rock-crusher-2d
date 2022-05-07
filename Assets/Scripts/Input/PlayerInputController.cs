using UnityEngine;

namespace Game
{
    public class PlayerInputController : IPlayerInputController
    {
        private readonly PlayerInputModel playerInputModel;

        public PlayerInputController(PlayerInputModel playerInputModel)
        {
            this.playerInputModel = playerInputModel;
        }

        public void Update()
        {
            IsShootBulletPressed = playerInputModel.shootBullet.action.WasPressedThisFrame();
            IsShootLaserPressed = playerInputModel.shootLaser.action.WasPressedThisFrame();

            IsMovingForwardPressed = false;
            TurnDirection = Vector2.zero;

            var value = playerInputModel.move.action.ReadValue<Vector2>();

            if (value == Vector2.zero)
                return;

            IsMovingForwardPressed = value.y > 0;

            if (value.x != 0)
                TurnDirection = value;
        }

        public Vector2 TurnDirection { get; private set; }
        public bool IsMovingForwardPressed { get; private set; }
        public bool IsShootBulletPressed { get; private set; }
        public bool IsShootLaserPressed { get; private set; }
    }
}