using System;
using Game.Base;
using Game.Gameplay.Utility;
using Game.Input;
using Game.Movement;
using Game.Weapons.Bullet;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.PlayerShip
{
    public class ShipSpawner : Controller<ShipSpawnerData>, IFactory<ShipController>
    {
        private readonly PlayerInputData playerInputData;
        private readonly ShipMovementData shipMovementData;
        private readonly ShipWeaponsData shipWeaponsData;
        private readonly SpeedData speedData;

        public ShipSpawner(ShipSpawnerData model,
                           ShipWeaponsData shipWeaponsData,
                           ShipMovementData shipMovementData,
                           SpeedData speedData,
                           PlayerInputData playerInputData) : base(model)
        {
            this.shipWeaponsData = shipWeaponsData;
            this.shipMovementData = shipMovementData;
            this.speedData = speedData;
            this.playerInputData = playerInputData;
        }

        public event Action<ShipController> Created;

        public ShipController Spawn(Transform bulletParent,
                                    ScreenBoundsController screenBoundsController,
                                    CoroutineRunner runner)
        {
            var ship = Object.Instantiate(model.Prefab, bulletParent).GetComponent<ShipData>();

            var playerInputController = new PlayerInputController(playerInputData);

            var movementController = new ShipMovementController(
                shipMovementData,
                speedData,
                ship.transform);

            var bulletFactory =
                new BulletFactory(shipWeaponsData.BulletWeaponData.BulletPrefab, ship.BulletShootPoint, bulletParent);

            var shipWeaponController = new ShipWeaponController(
                shipWeaponsData,
                ship,
                bulletFactory,
                runner);

            var shipController = new ShipController(
                ship,
                movementController,
                playerInputController,
                shipWeaponController);

            screenBoundsController.Add(ship.transform);
            screenBoundsController.Add(bulletFactory);

            Created?.Invoke(shipController);

            return shipController;
        }
    }
}