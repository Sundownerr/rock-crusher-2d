using Game.Base.Interface;
using UnityEngine;

namespace Game.Ship.Movement.Interface
{
    public interface IShipMovementController : IUpdate
    {
        void Move();
        void Stop();
        void Turn(Vector2 direction);
    }
}