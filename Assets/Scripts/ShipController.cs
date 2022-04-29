namespace Game
{
    public class ShipController : IUpdate
    {
        private readonly IMovementController movementController;
        private readonly ISpeedController speedController;

        public ShipController(IMovementController movementController, ISpeedController speedController)
        {
            this.movementController = movementController;
            this.speedController = speedController;
        }

        public void Update()
        {
            movementController.Update();
            speedController.Update();
        }
    }
}