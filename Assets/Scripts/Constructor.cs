using UnityEngine;

namespace Game
{
    public class Constructor : MonoBehaviour
    {
        [SerializeField] private GameObject shipPrefab;
        [SerializeField] private MovementModel movementModel;
        [SerializeField] private SpeedModel speedModel;
        [SerializeField] private PlayerInputModel playerInputModel;

        private ShipController ship;

        private void Start()
        {
            var shipGO = Instantiate(shipPrefab);

            ship = new ShipController(
                new MovementController(movementModel, shipGO.transform),
                new SpeedController(speedModel),
                new PlayerInputController(playerInputModel)
            );
        }

        private void Update()
        {
            ship.Update();
        }
    }
}