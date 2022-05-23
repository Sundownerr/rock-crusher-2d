using Game.Base;
using Game.Movement.Interface;
using UnityEngine;

namespace Game.Movement
{
    public class ShipMovementController : Controller<ShipMovementData>, IShipMovementController
    {
        private readonly SpeedController speedController;
        private readonly SpeedData speedData;
        private readonly Transform targetTransform;
        private Vector3 inertia;
        private bool isMoving;

        public ShipMovementController(ShipMovementData model,
                                      SpeedData speedData,
                                      Transform targetTransform) :
            base(model)
        {
            this.speedData = speedData;
            this.targetTransform = targetTransform;

            speedController = new SpeedController(speedData);
        }

        public void Update()
        {
            model.X = targetTransform.position.x;
            model.Y = targetTransform.position.y;
            model.Angle = targetTransform.rotation.eulerAngles.z;
            model.Speed = speedData.CurrentSpeed;

            speedController.Update();

            if (isMoving)
                return;

            targetTransform.position += inertia;
        }

        public void Move()
        {
            speedController.Accelerate();
            isMoving = true;

            var direction = targetTransform.up * (speedData.CurrentSpeed * Time.deltaTime);

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