using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class PlayerInputController : IPlayerInputController
    {
        private readonly PlayerInputData playerInputData;

        public PlayerInputController(PlayerInputData playerInputData)
        {
            this.playerInputData = playerInputData;

            playerInputData.move.action.started += OnStartMove;
            playerInputData.move.action.canceled += OnEndMove;
            playerInputData.shootBullet.action.performed += OnShootBullet;
        }

        public void Update()
        {
            IsShootBulletPressed = playerInputData.shootBullet.action.WasPressedThisFrame();
            IsShootLaserPressed = playerInputData.shootLaser.action.WasPressedThisFrame();

            IsMovingForwardPressed = false;
            TurnDirection = Vector2.zero;

            var value = playerInputData.move.action.ReadValue<Vector2>();

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
        public event Action StartedMoving;
        public event Action ShootedBullet;
        public event Action EndedMoving;

        private void OnShootBullet(InputAction.CallbackContext obj)
        {
            ShootedBullet?.Invoke();
        }

        private void OnEndMove(InputAction.CallbackContext obj)
        {
            EndedMoving?.Invoke();
        }

        private void OnStartMove(InputAction.CallbackContext obj)
        {
            StartedMoving?.Invoke();
        }
    }
}