using UnityEngine;

namespace Game
{
    public class Constructor : MonoBehaviour
    {
        [SerializeField] private GameObject shipPrefab;
        [SerializeField] private Transform bulletContainer;
        [SerializeField] private ShipMovementModel shipMovementModel;
        [SerializeField] private SpeedModel speedModel;
        [SerializeField] private PlayerInputModel playerInputModel;
        [SerializeField] private BulletWeaponModel bulletWeaponModel;

        private ScreenBoundsController screenBoundsController;
        private ShipController ship;
        private WeaponHitController weaponHitController;

        private void Start()
        {
            var shipModel = Instantiate(shipPrefab).GetComponent<ShipModel>();

            screenBoundsController = new ScreenBoundsController();
            screenBoundsController.Add(shipModel.transform);

            var movementController = new ShipMovementController(shipMovementModel, shipModel.transform);
            var speedController = new SpeedController(speedModel);
            var bulletWeaponController =
                new BulletWeaponController(bulletWeaponModel, shipModel.BulletShootPoint, bulletContainer);
            var laserWeaponController = new LaserWeaponController(shipModel.LaserShootPoint);
            var playerInputController = new PlayerInputController(playerInputModel);

            ship = new ShipController(
                movementController,
                speedController,
                bulletWeaponController,
                laserWeaponController,
                playerInputController
            );

            screenBoundsController.Add(bulletWeaponController);

            weaponHitController = new WeaponHitController();
            weaponHitController.Add(bulletWeaponController);
        }

        private void Update()
        {
            ship.Update();
            screenBoundsController.Update();
        }

        private void OnDestroy()
        {
            screenBoundsController.Destroy();
            weaponHitController.Destroy();
        }
    }
}