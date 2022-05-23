using Game.Base;
using Game.Movement.Interface;
using UnityEngine;

namespace Game.Movement
{
    public class ShipMovementController : Controller<ShipMovementData>, IShipMovementController
    {
        private readonly Transform targetTransform;
        private Vector3 inertia;
        private bool isMoving;

        public ShipMovementController(ShipMovementData model, Transform targetTransform) : base(model)
        {
            this.targetTransform = targetTransform;
        }

        public void Update()
        {
            if (isMoving)
                return;

            targetTransform.position += inertia;
        }

        public void Move(float speed)
        {
            isMoving = true;

            var direction = targetTransform.up * (speed * Time.deltaTime);

            inertia = Vector3.Lerp(inertia, direction, Time.deltaTime);
            targetTransform.position += inertia;
        }

        public void Stop()
        {
            isMoving = false;
        }

        public void Turn(Vector2 direction)
        {
            targetTransform.Rotate(Vector3.back, direction.x * model.turnSpeed);
        }
    }
}