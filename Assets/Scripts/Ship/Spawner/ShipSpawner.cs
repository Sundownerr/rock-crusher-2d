using System;
using Game.Base;
using Game.Gameplay.Utility;
using Game.Input;
using Game.Ship.Interface;
using Game.Ship.Movement;
using Game.Ship.Weapons;
using Game.Ship.Weapons.Bullet;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Ship.Spawner
{
    public class ShipSpawner : Controller<ShipSpawnerData>, IFactory<(IShipController, IFactory<Transform>, Transform)>
    {
        private readonly Transform bulletParent;
        private readonly PlayerInputData playerInputData;
        private readonly ShipMovementData shipMovementData;
        private readonly ShipSpeedData shipSpeedData;
        private readonly ShipWeaponsData shipWeaponsData;

        public ShipSpawner(ShipSpawnerData model,
                           ShipWeaponsData shipWeaponsData,
                           Transform bulletParent,
                           ShipMovementData shipMovementData,
                           ShipSpeedData shipSpeedData,
                           PlayerInputData playerInputData) : base(model)
        {
            this.shipWeaponsData = shipWeaponsData;
            this.bulletParent = bulletParent;
            this.shipMovementData = shipMovementData;
            this.shipSpeedData = shipSpeedData;
            this.playerInputData = playerInputData;
        }

        public event Action<(IShipController, IFactory<Transform>, Transform)> Created;

        public void Spawn(CoroutineRunner runner)
        {
            var ship = Object.Instantiate(model.Prefab, bulletParent).GetComponent<ShipData>();

            var playerInputController = new PlayerInputController(playerInputData);

            var movementController = new ShipMovementController(
                shipMovementData,
                shipSpeedData,
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

            Created?.Invoke((shipController, bulletFactory, ship.transform));
        }
    }
}