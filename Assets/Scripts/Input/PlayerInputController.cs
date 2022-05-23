using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class PlayerInputController : IPlayerInputController, IDestroyable
    {
        private readonly PlayerInputData playerInputData;

        public PlayerInputController(PlayerInputData playerInputData)
        {
            this.playerInputData = playerInputData;

            playerInputData.move.action.started += OnStartMove;
            playerInputData.move.action.canceled += OnEndMove;
            playerInputData.shootBullet.action.performed += OnShootBullet;
            playerInputData.shootLaser.action.performed += OnShootLaser;
        }

        public void Destroy()
        {
            playerInputData.move.action.started -= OnStartMove;
            playerInputData.move.action.canceled -= OnEndMove;
            playerInputData.shootBullet.action.performed -= OnShootBullet;
            playerInputData.shootLaser.action.performed -= OnShootLaser;
        }

        public Vector2 TurnDirection { get; private set; }
        public bool IsMovingForwardPressed { get; private set; }

        public event Action MovingForwardPressed;
        public event Action ShootBulletPressed;
        public event Action ShootLaserPressed;
        public event Action MovingReleased;

        public void Update()
        {
            TurnDirection = Vector2.zero;

            var value = playerInputData.move.action.ReadValue<Vector2>();

            if (value == Vector2.zero)
                return;

            IsMovingForwardPressed = value.y > 0;

            if (value.x != 0)
                TurnDirection = value;
        }

        private void OnShootLaser(InputAction.CallbackContext ctx)
        {
            ShootLaserPressed?.Invoke();
        }

        private void OnShootBullet(InputAction.CallbackContext ctx)
        {
            ShootBulletPressed?.Invoke();
        }

        private void OnEndMove(InputAction.CallbackContext ctx)
        {
            MovingReleased?.Invoke();
        }

        private void OnStartMove(InputAction.CallbackContext ctx)
        {
            MovingForwardPressed?.Invoke();
        }
    }
}