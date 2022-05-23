using Game.PlayerShip;

namespace Game.UI.Interface
{
    public interface IGameplayUIController : IUiController, IUpdate
    {
        void SetShip(ShipData shipData);
    }
}