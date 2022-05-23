using Game.PlayerShip;
using Game.Weapons.Laser;

namespace Game.UI.Interface
{
    public interface IGameplayUIController : IUiController, IUpdate
    {
        void SetShipData(ShipData data);
        void SetLaserData(LaserWeaponData data);
    }
}