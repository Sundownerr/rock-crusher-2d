using System;
using Game.Base;
using Game.Base.Interface;
using Game.Gameplay.Utility;
using Game.Input.Interface;
using Game.Ship.Factory.Interface;
using Game.Ship.Movement;
using Game.Ship.Weapons;
using Game.Ship.Weapons.Bullet.Factory;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Ship.Factory
{
    public class ShipFactory : Controller<ShipFactoryData>, IShipFactory
    {
        private readonly Transform bulletParent;
        private readonly Transform parent;
        private readonly IPlayerInputController playerInputController;
        private readonly CoroutineRunner runner;

        public ShipFactory(ShipFactoryData model,
                           Transform parent,
                           Transform bulletParent,
                           CoroutineRunner runner,
                           IPlayerInputController playerInputController) : base(model)
        {
            this.parent = parent;
            this.bulletParent = bulletParent;
            this.runner = runner;
            this.playerInputController = playerInputController;
        }

        public event Action<ShipController, ShipData, IContainer<Transform>> Created;

        public (ShipController, ShipData, IContainer<Transform>) Create()
        {
            var ship = Object.Instantiate(model.Prefab, parent).GetComponent<ShipData>();
            var colliderData = ship.GetComponent<ColliderData>();

            var movementController = new ShipMovementController(
                model.ShipMovementData,
                model.ShipSpeedData,
                ship.transform);

            var bulletProvider = new BulletProvider(
                model.ShipWeaponsData.BulletWeaponData.BulletPrefab,
                ship.BulletShootPoint,
                bulletParent);

            var weaponController = new ShipWeaponController(
                model.ShipWeaponsData,
                ship,
                bulletProvider,
                runner);

            var controller = new ShipController(
                ship,
                colliderData,
                movementController,
                playerInputController,
                weaponController);

            var result = (controller, ship, bulletProvider);

            Created?.Invoke(controller, ship, bulletProvider);

            return result;
        }
    }
}