using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public enum TurnDirection
    {
        None,
        Left,
        Right
    }

    public interface IPlayerInputController : IUpdate
    {
        Vector2 TurnDirection { get; }
        bool IsMovingForwardPressed { get; }
        bool IsShootPressed { get; }
    }

    public class PlayerInputController : IPlayerInputController
    {
        private readonly InputAction fire;
        private readonly InputAction move;

        private PlayerInputModel playerInputModel;

        public PlayerInputController(PlayerInputModel playerInputModel)
        {
            this.playerInputModel = playerInputModel;

            move = playerInputModel.move.action;
            fire = playerInputModel.fire.action;
        }

        public void Update()
        {
            IsMovingForwardPressed = false;
            TurnDirection = Vector2.zero;

            var value = move.ReadValue<Vector2>();

            if (value == Vector2.zero)
                return;

            IsMovingForwardPressed = value.y > 0;

            if (value.x != 0)
                TurnDirection = value;

            IsShootPressed = fire.IsPressed();
        }

        public Vector2 TurnDirection { get; private set; }
        public bool IsMovingForwardPressed { get; private set; }
        public bool IsShootPressed { get; private set; }
    }
}