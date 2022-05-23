using Game.Ship.Weapons.Interface;

namespace Game.Weapons.Laser.Interface
{
    public interface ILaserWeaponController : IWeaponController
    {
        bool CanShoot { get; }
    }
}