using Game.Enemy.Asteroid.Movement;

namespace Game.Enemy.Asteroid
{
    public class AsteroidController : IUpdate
    {
        private readonly AsteroidMovementController movementController;

        public AsteroidController(AsteroidMovementController movementController)
        {
            this.movementController = movementController;
        }

        public void Update()
        {
            movementController.Update();
        }
    }
}