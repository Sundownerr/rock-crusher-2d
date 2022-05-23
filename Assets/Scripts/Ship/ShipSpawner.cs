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

        public Ship Ship { get; private set; }

        public void Destroy()
        {
            weaponHitController.Destroy();
            shipController.Destroy();
        }

        public event Action<ShipController> Created;

        public ShipController Spawn(Transform bulletParent, ScreenBoundsController screenBoundsController)
        {
            Ship = Object.Instantiate(model.Prefab).GetComponent<Ship>();

            var speedController = new SpeedController(model.SpeedData);
            var movementController = new ShipMovementController(model.ShipMovementData, Ship.transform);

            var bulletFactory =
                new BulletFactory(model.BulletWeaponData.BulletPrefab, Ship.BulletShootPoint, bulletParent);
            var bulletWeaponController = new BulletWeaponController(model.BulletWeaponData, bulletFactory);

            var laserWeaponController = new LaserWeaponController(Ship.LaserShootPoint);
            var playerInputController = new PlayerInputController(model.PlayerInputData);

            shipController = new ShipController(
                Ship,
                movementController,
                speedController,
                bulletWeaponController,
                laserWeaponController,
                playerInputController);

            weaponHitController = new WeaponHitController();
            weaponHitController.Add(bulletWeaponController);
            weaponHitController.Add(laserWeaponController);

            screenBoundsController.Add(Ship.transform);
            screenBoundsController.Add(bulletFactory);

            Created?.Invoke(shipController);

            return shipController;
        }
    }
}