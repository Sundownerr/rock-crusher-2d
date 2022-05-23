using Game.Ship.Weapons.Interface;

namespace Game.Ship.Weapons.Laser.Interface
{
    public interface ILaserWeaponController : IWeaponController
    {
        bool CanShoot { get; }
    }
}