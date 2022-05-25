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

        public bool IsDead { get; private set; }

        public void Update()
        {
            movementController.Update();

            if (model.IsDamaged)
                HandleDamaged();
        }

        public void HandleDamaged()
        {
            IsDead = true;
            Dead?.Invoke(model);
        }

        public event Action<AsteroidData> Dead;
    }
}