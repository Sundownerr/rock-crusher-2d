namespace Game.Movement.Interface
{
    public interface ISpeedController : IUpdate
    {
        float Speed { get; }
        void Accelerate();
    }
}