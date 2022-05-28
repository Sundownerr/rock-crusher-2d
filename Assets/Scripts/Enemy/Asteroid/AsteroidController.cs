using System;
using Game.Base;
using Game.Enemy.Asteroid.Movement;
using Game.Enemy.Interface;

namespace Game.Enemy.Asteroid
{
    public class AsteroidController : Controller<AsteroidData>, IEnemy
    {
        private readonly AsteroidMovementController movementController;

        public AsteroidController(AsteroidData model,
                                  AsteroidMovementController movementController) : base(model)
        {
            this.movementController = movementController;
        }

        public void Update()
        {
            movementController.Update();

            if (model.IsCompletlyDestroyed)
                CompletelyDestroyed?.Invoke();

            if (model.IsDamaged)
                Damaged?.Invoke();
        }

        public event Action Damaged;
        public event Action CompletelyDestroyed;
    }
}