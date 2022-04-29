namespace Game
{
    public class ShipController : IUpdate
    {
        private readonly IMovementController movementController;
        private readonly IPlayerInputController playerInputController;
        private readonly ISpeedController speedController;

        public ShipController(IMovementController movementController, ISpeedController speedController,
            IPlayerInputController playerInputController)
        {
            this.movementController = movementController;
            this.speedController = speedController;
            this.playerInputController = playerInputController;
        }

        public void Update()
        {
            playerInputController.Update();
            movementController.Update();
            speedController.Update();

            if (playerInputController.IsMovingForwardPressed)
            {
                speedController.Accelerate();
                movementController.Move(speedController.Speed);
            }
            else
            {
                movementController.Stop();
            }

            movementController.Turn(playerInputController.TurnDirection);
        }
    }
}