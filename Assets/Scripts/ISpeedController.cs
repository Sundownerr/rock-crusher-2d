namespace Game
{
    public interface ISpeedController : IUpdate
    {
        float Speed { get; }
        void Accelerate();
    }
}