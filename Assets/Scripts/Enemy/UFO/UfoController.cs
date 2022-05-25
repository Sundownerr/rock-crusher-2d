using Game.Enemy.Interface;

namespace Game.Enemy.UFO
{
    public class UfoController : IEnemy
    {
        private readonly UfoMovementController movementController;

        public UfoController(UfoMovementController movementController)
        {
            this.movementController = movementController;
        }

        public void Update()
        {
            movementController.Update();
        }
    }
}