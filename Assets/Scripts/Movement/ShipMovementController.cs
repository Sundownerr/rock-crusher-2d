using UnityEngine;

namespace Game
{
    public class ShipMovementController : IShipMovementController
    {
        private readonly ShipMovementModel model;
        private readonly Transform targetTransform;
        private Vector3 inertia;
        private bool isMoving;

        public ShipMovementController(ShipMovementModel model, Transform targetTransform)
        {
            this.model = model;
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