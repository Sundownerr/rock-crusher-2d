using System;
using Game.Base;
using Game.Input.Interface;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Input
{
    public class PlayerInputController : Controller<PlayerInputData>, IPlayerInputController, IDestroyable
    {
        public PlayerInputController(PlayerInputData model) : base(model)
        {
            model.move.action.started += OnStartMove;
            model.move.action.canceled += OnEndMove;
            model.shootBullet.action.performed += OnShootBullet;
            model.shootLaser.action.performed += OnShootLaser;
        }

        public void Destroy()
        {
            model.move.action.started -= OnStartMove;
            model.move.action.canceled -= OnEndMove;
            model.shootBullet.action.performed -= OnShootBullet;
            model.shootLaser.action.performed -= OnShootLaser;
        }

        public Vector2 TurnDirection { get; private set; }
        public bool IsMovingForwardPressed { get; private set; }

        public event Action MovingForwardPressed;
        public event Action ShootBulletPressed;
        public event Action ShootLaserPressed;
        public event Action MovingReleased;

        public void Update()
        {
            var moveDirection = model.move.action.ReadValue<Vector2>();

            TurnDirection = Vector2.zero;
            IsMovingForwardPressed = moveDirection.y > 0;

            if (moveDirection.x != 0)
                TurnDirection = moveDirection;
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