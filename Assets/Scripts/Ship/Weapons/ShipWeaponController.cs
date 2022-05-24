using System.Collections;
using Game.Base;
using Game.Gameplay.Utility;
using Game.Ship.Weapons;
using Game.Ship.Weapons.Bullet;
using Game.Ship.Weapons.Bullet.Interface;
using Game.Ship.Weapons.Interface;
using Game.Ship.Weapons.Laser;
using Game.Ship.Weapons.Laser.Interface;
using UnityEngine;

namespace Game.Ship
{
    public class ShipWeaponController : Controller<ShipWeaponsData>, IShipWeaponController
    {
        private readonly WaitForSeconds bulletShootInterval;
        private readonly IBulletWeaponController bulletWeaponController;
        private readonly ILaserWeaponController laserWeaponController;
        private readonly CoroutineRunner runner;
        private readonly ShipData shipData;
        private readonly WeaponHitController weaponHitController;

        private bool canShoot = true;

        public ShipWeaponController(ShipWeaponsData model,
                                    ShipData shipData,
                                    BulletFactory bulletFactory,
                                    CoroutineRunner runner) : base(model)
        {
            this.shipData = shipData;
            this.runner = runner;

            laserWeaponController = new LaserWeaponController(model.LaserWeaponData, shipData.LaserShootPoint, runner);
            bulletWeaponController = new BulletWeaponController(model.BulletWeaponData, bulletFactory);

            weaponHitController = new WeaponHitController();
            weaponHitController.Add(bulletWeaponController);
            weaponHitController.Add(laserWeaponController);

            bulletShootInterval = new WaitForSeconds(model.BulletWeaponData.ShootInterval);
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

        public void ShootBullets()
        {
            if (!canShoot)
                return;

            runner.StartCoroutine(ShootBulletsWithInterval());
            canShoot = false;
        }

        public void Update()
        {
            bulletWeaponController.Update();
        }

        private IEnumerator ShootBulletsWithInterval()
        {
            bulletWeaponController.Shoot();
            shipData.BulletShootVFX.Play();

            yield return bulletShootInterval;

            canShoot = true;
        }
    }
}