using Game.Combat.Interface;

namespace Game.Weapons.Laser
{
    public interface ILaserWeaponController : IWeaponController
    {
        bool CanShoot { get; }
    }
}