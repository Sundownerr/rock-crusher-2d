using System;
using Game.Base.Interface;
using UnityEngine;

namespace Game.Input.Interface
{
    public interface IPlayerInputController : IUpdate, IDestroyable
    {
        Vector2 TurnDirection { get; }
        bool IsMovingForwardPressed { get; }
        bool IsShootingBulletsPressed { get; }

        event Action MovingPressed;
        event Action ShootLaserPressed;
        event Action MovingReleased;
    }
}