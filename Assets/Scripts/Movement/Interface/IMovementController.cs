namespace Game.Movement.Interface
{
    public interface IMovementController : IUpdate
    {
        void Move(float speed);
        void Stop();
    }
}