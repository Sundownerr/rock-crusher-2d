using UnityEngine;

namespace Game
{
    public interface IPlayerInputController : IUpdate
    {
        Vector2 TurnDirection { get; }
        bool IsMovingForwardPressed { get; }
        bool IsShootBulletPressed { get; }
        bool IsShootLaserPressed { get; }
    }
}