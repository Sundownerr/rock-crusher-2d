namespace Game.Movement.Interface
{
    public interface IMovementController : IUpdate
    {
        void Move();
        void Stop();
    }
}