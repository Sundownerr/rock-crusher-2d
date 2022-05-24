using System;
using Game.Base;
using Game.Gameplay.Utility;
using Game.Input.Interface;
using Game.Ship.Factory.Interface;
using Game.Ship.Interface;
using Game.Ship.Movement;
using Game.Ship.Weapons;
using Game.Ship.Weapons.Bullet;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Ship.Factory
{
    public class ShipFactory : Controller<ShipFactoryData>, IShipFactory
    {
        private readonly Transform bulletParent;
        private readonly IPlayerInputController playerInputController;
        private readonly CoroutineRunner runner;
        private readonly ShipMovementData shipMovementData;
        private readonly ShipSpeedData shipSpeedData;
        private readonly ShipWeaponsData shipWeaponsData;

        public ShipFactory(ShipFactoryData model,
                           ShipWeaponsData shipWeaponsData,
                           Transform bulletParent,
                           ShipMovementData shipMovementData,
                           ShipSpeedData shipSpeedData,
                           CoroutineRunner runner,
                           IPlayerInputController playerInputController) : base(model)
        {
            this.shipWeaponsData = shipWeaponsData;
            this.bulletParent = bulletParent;
            this.shipMovementData = shipMovementData;
            this.shipSpeedData = shipSpeedData;
            this.runner = runner;
            this.playerInputController = playerInputController;
        }

        public event Action<(IShipController, IFactory<Transform>, Transform)> Created;

        public (IShipController, IFactory<Transform>, Transform) Create()
        {
            var ship = Object.Instantiate(model.Prefab, bulletParent).GetComponent<ShipData>();

            var movementController = new ShipMovementController(
                shipMovementData,
                shipSpeedData,
                ship.transform);

            var bulletFactory = new BulletFactory(
                shipWeaponsData.BulletWeaponData.BulletPrefab,
                ship.BulletShootPoint,
                bulletParent);

            var weaponController = new ShipWeaponController(
                shipWeaponsData,
                ship,
                bulletFactory,
                runner);

            var controller = new ShipController(
                ship,
                movementController,
                playerInputController,
                weaponController);

            var result = (controller, bulletFactory, ship.transform);

            Created?.Invoke(result);

            return result;
        }
    }
}