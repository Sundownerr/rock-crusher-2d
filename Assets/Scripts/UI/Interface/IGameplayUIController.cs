using Game.Movement;
using Game.Weapons.Laser;

namespace Game.UI.Interface
{
    public interface IGameplayUIController : IUiController, IUpdate
    {
        void SetShipMovemenData(ShipMovementData data);
        void SetLaserData(LaserWeaponData data);
    }
}