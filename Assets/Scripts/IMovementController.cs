using UnityEngine;

namespace Game
{
    public interface IMovementController : IUpdate
    {
        void Move(float speed);
        void Turn(Vector2 direction);

        void Stop();
    }
}