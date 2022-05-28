using Game.Base.Interface;

namespace Game.Ship.Movement.Interface
{
    public interface IShipSpeedController : IUpdate
    {
        void Accelerate();
    }
}