using Game.Combat.Interface;

namespace Game.Weapons.Laser.Interface
{
    public interface ILaserWeaponController : IWeaponController
    {
        bool CanShoot { get; }
    }
}