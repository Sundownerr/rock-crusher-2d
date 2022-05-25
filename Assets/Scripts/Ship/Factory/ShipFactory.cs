using System;
using Game.Base;
using Game.Base.Interface;
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

        public event Action<(IShipController, IFactory<Transform>, ShipData)> Created;

        public (IShipController, IFactory<Transform>, ShipData) Create()
        {
            var ship = Object.Instantiate(model.Prefab, bulletParent).GetComponent<ShipData>();
            var colliderData = ship.GetComponent<ColliderData>();

            var movementController = new ShipMovementController(
                shipMovementData,
                shipSpeedData,
                ship.transform);

            var bulletFactory = new BulletFactory(
                shipWeaponsData.BulletWeaponData.BulletPrefab,
                ship.BulletShootPoint,
                bulletParent);

            var bulletPool = new BulletPool(bulletFactory);

            var weaponController = new ShipWeaponController(
                shipWeaponsData,
                ship,
                bulletPool,
                runner);

            var controller = new ShipController(
                ship,
                colliderData,
                movementController,
                playerInputController,
                weaponController);

            var result = (controller, bulletFactory, ship);

            Created?.Invoke(result);

            return result;
        }
    }
}