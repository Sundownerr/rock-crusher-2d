using Game.Base.Interface;

namespace Game.Ship.Weapons.Interface
{
    public interface IShipWeaponController : IUpdate, IDestroyable
    {
        void ShootLaser();
        void ShootBullets();
    }
}