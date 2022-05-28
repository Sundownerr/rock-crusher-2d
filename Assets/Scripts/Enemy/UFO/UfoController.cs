using System;
using Game.Base;
using Game.Enemy.Interface;
using Game.Enemy.UFO.Movement;

namespace Game.Enemy.UFO
{
    public class UfoController : Controller<UfoData>, IEnemy
    {
        private readonly UfoMovementController movementController;

        public UfoController(UfoData model, UfoMovementController movementController) : base(model)
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