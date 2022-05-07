using UnityEngine;

namespace Game
{
    public interface IShipMovementController : IMovementController
    {
        void Turn(Vector2 direction);
    }
}