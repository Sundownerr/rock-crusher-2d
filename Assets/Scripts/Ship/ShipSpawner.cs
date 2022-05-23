using System;
using Game.Base;
using Game.Combat;
using Game.Gameplay.Utility;
using Game.Input;
using Game.Movement;
using Game.Weapons.Bullet;
using Game.Weapons.Laser;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.PlayerShip
{
    public class ShipSpawner : Controller<ShipSpawnerData>, IDestroyable, IFactory<ShipController>
    {
        private ShipController shipController;
        private WeaponHitController weaponHitController;

        public ShipSpawner(ShipSpawnerData model) : base(model)
        { }

        public void Destroy()
        {
            weaponHitController.Destroy();
            shipController.Destroy();
        }

        public event Action<ShipController> Created;

        public (ShipData model, ShipController controller, LaserWeaponData laserWeaponData)
            Spawn(Transform bulletParent,
                  ScreenBoundsController screenBoundsController,
                  CoroutineRunner runner)
        {
            var ship = Object.Instantiate(model.Prefab, bulletParent).GetComponent<ShipData>();

            var speedController = new SpeedController(model.SpeedData);
            var movementController = new ShipMovementController(model.ShipMovementData, ship.transform);

            var bulletFactory =
                new BulletFactory(model.BulletWeaponData.BulletPrefab, ship.BulletShootPoint, bulletParent);
            var bulletWeaponController = new BulletWeaponController(model.BulletWeaponData, bulletFactory);

            var laserWeaponController = new LaserWeaponController(model.LaserWeaponData, ship.LaserShootPoint, runner);
            var playerInputController = new PlayerInputController(model.PlayerInputData);

            shipController = new ShipController(
                ship,
                movementController,
                speedController,
                bulletWeaponController,
                laserWeaponController,
                playerInputController);

            weaponHitController = new WeaponHitController();
            weaponHitController.Add(bulletWeaponController);
            weaponHitController.Add(laserWeaponController);

            screenBoundsController.Add(ship.transform);
            screenBoundsController.Add(bulletFactory);

            Created?.Invoke(shipController);

            return (ship, shipController, model.LaserWeaponData);
        }
    }
}