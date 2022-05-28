using Game.Base.Interface;
using Game.Ship.Movement;
using Game.Ship.Weapons.Laser;

namespace Game.UI.Interface
{
    public interface IGameplayUIController : IUiController, IUpdate
    {
        void SetShipMovemenData(ShipMovementData data);
        void SetLaserData(LaserWeaponData data);
    }
}