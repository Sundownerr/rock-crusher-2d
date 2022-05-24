using System;
using Game.Base;
using Game.Enemy.Asteroid.Interface;
using Game.Enemy.Asteroid.Movement;

namespace Game.Enemy.Asteroid
{
    public class AsteroidController : Controller<AsteroidData>, IAsteroid
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

            if (model.IsDamaged)
                HandleDamaged();
        }

        public bool IsDead { get; private set; }

        public void HandleDamaged()
        {
            IsDead = true;
            Dead?.Invoke(model);
        }

        public event Action<AsteroidData> Dead;
    }
}