using UnityEngine;

namespace Game.Movement.Interface
{
    public interface IShipMovementController : IMovementController
    {
        void Turn(Vector2 direction);
    }
}