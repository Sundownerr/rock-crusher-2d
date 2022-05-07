namespace Game
{
    public interface IMovementController : IUpdate
    {
        void Move(float speed);
        void Stop();
    }
}