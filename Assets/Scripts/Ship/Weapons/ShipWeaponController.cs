using Game.Base;
using Game.Gameplay.Utility;
using Game.Ship.Weapons;
using Game.Ship.Weapons.Interface;
using Game.Weapons.Bullet;
using Game.Weapons.Bullet.Interface;
using Game.Weapons.Laser;
using Game.Weapons.Laser.Interface;

namespace Game.Ship
{
    public class ShipWeaponController : Controller<ShipWeaponsData>, IShipWeaponController
    {
        private readonly IBulletWeaponController bulletWeaponController;
        private readonly ILaserWeaponController laserWeaponController;
        private readonly ShipData shipData;
        private readonly WeaponHitController weaponHitController;

        public ShipWeaponController(ShipWeaponsData model,
                                    ShipData shipData,
                                    BulletFactory bulletFactory,
                                    CoroutineRunner runner) : base(model)
        {
            this.shipData = shipData;

            laserWeaponController = new LaserWeaponController(model.LaserWeaponData, shipData.LaserShootPoint, runner);
            bulletWeaponController = new BulletWeaponController(model.BulletWeaponData, bulletFactory);

            weaponHitController = new WeaponHitController();
            weaponHitController.Add(bulletWeaponController);
            weaponHitController.Add(laserWeaponController);
        }

        public void Destroy()
        {
            weaponHitController.Destroy();
        }

        public void ShootLaser()
        {
            laserWeaponController.Shoot();

            if (laserWeaponController.CanShoot)
                shipData.Animator.Play(shipData.LaserAnimationKey);
        }

        public void ShootBullet()
        {
            bulletWeaponController.Shoot();
            shipData.BulletShootVFX.Play();
        }

        public void Update()
        {
            bulletWeaponController.Update();
        }
    }
}