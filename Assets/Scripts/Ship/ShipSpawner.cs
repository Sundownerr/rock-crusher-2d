using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game
{
    public class ShipSpawner : Spawner<ShipSpawnerData, ShipController>, IDestroyable
    {
        private readonly ShipSpawnerData shipSpawnerData;
        private ShipController shipController;
        private WeaponHitController weaponHitController;

        public ShipSpawner(ShipSpawnerData model) : base(model)
        {
            shipSpawnerData = model;
        }

        public Ship Ship { get; private set; }

        public void Destroy()
        {
            weaponHitController.Destroy();
            shipController.Destroy();
        }

        public override event Action<ShipController> Created;

        public ShipController Spawn(Transform bulletParent, ScreenBoundsController screenBoundsController)
        {
            Ship = Object.Instantiate(shipSpawnerData.Prefab).GetComponent<Ship>();

            var speedController = new SpeedController(model.SpeedData);
            var movementController = new ShipMovementController(model.ShipMovementData, Ship.transform);
            var bulletWeaponController = new BulletWeaponController(model.BulletWeaponData, Ship.BulletShootPoint,
                bulletParent);

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
            screenBoundsController.Add(bulletWeaponController);

            Created?.Invoke(shipController);

            return shipController;
        }
    }
}