using System;
using UnityEngine;

namespace Game.Input.Interface
{
    public interface IPlayerInputController : IUpdate
    {
        Vector2 TurnDirection { get; }
        bool IsMovingForwardPressed { get; }

        event Action MovingForwardPressed;
        event Action ShootBulletPressed;
        event Action ShootLaserPressed;
        event Action MovingReleased;
    }
}