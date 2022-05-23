namespace Game.PlayerShip.Interface
{
    public interface IShipWeaponController : IUpdate, IDestroyable
    {
        void ShootLaser();
        void ShootBullet();
    }
}