using UnityEngine;

namespace Game
{
    public class Constructor : MonoBehaviour
    {
        [SerializeField] private GameObject shipPrefab;
        [SerializeField] private Transform bulletContainer;
        [SerializeField] private MovementModel movementModel;
        [SerializeField] private SpeedModel speedModel;
        [SerializeField] private PlayerInputModel playerInputModel;
        [SerializeField] private BulletWeaponModel bulletWeaponModel;
        private ScreenBoundsController screenBoundsController;

        private ShipController ship;

        private void Start()
        {
            var shipModel = Instantiate(shipPrefab).GetComponent<ShipModel>();

            screenBoundsController = new ScreenBoundsController();
            screenBoundsController.Add(shipModel.transform);

            var movementController = new MovementController(movementModel, shipModel.transform);
            var speedController = new SpeedController(speedModel);
            var bulletWeaponController =
                new BulletWeaponController(bulletWeaponModel, shipModel, screenBoundsController, bulletContainer);
            var laserWeaponController = new LaserWeaponController(shipModel);
            var playerInputController = new PlayerInputController(playerInputModel);

            ship = new ShipController(
                movementController,
                speedController,
                bulletWeaponController,
                laserWeaponController,
                playerInputController
            );
        }

        private void Update()
        {
            ship.Update();
            screenBoundsController.Update();
        }
    }
}