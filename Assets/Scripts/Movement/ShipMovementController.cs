using Game.Base;
using Game.Movement.Interface;
using UnityEngine;

namespace Game.Ship.Movement
{
    public class ShipMovementController : Controller<ShipMovementData>, IShipMovementController
    {
        private readonly ShipSpeedController shipSpeedController;
        private readonly ShipSpeedData shipSpeedData;
        private readonly Transform targetTransform;
        private Vector3 inertia;
        private bool isMoving;

        public ShipMovementController(ShipMovementData model,
                                      ShipSpeedData shipSpeedData,
                                      Transform targetTransform) :
            base(model)
        {
            this.shipSpeedData = shipSpeedData;
            this.targetTransform = targetTransform;

            shipSpeedController = new ShipSpeedController(shipSpeedData);
        }

        public void Update()
        {
            model.X = targetTransform.position.x;
            model.Y = targetTransform.position.y;
            model.Angle = targetTransform.rotation.eulerAngles.z;
            model.Speed = shipSpeedData.CurrentSpeed;

            shipSpeedController.Update();

            if (isMoving)
                return;

            targetTransform.position += inertia;
        }

        public void Move()
        {
            shipSpeedController.Accelerate();
            isMoving = true;

            var direction = targetTransform.up * (shipSpeedData.CurrentSpeed * Time.deltaTime);

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