using Game.Base;
using Game.Ship.Movement.Interface;
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
        private float rotateValue;

        public ShipMovementController(ShipMovementData model,
                                      ShipSpeedData shipSpeedData,
                                      Transform targetTransform) :
            base(model)
        {
            this.shipSpeedData = shipSpeedData;
            this.targetTransform = targetTransform;

            shipSpeedController = new ShipSpeedController(this.shipSpeedData);
        }

        public void Update()
        {
            model.X = targetTransform.position.x;
            model.Y = targetTransform.position.y;
            model.Angle = targetTransform.rotation.eulerAngles.z;
            model.Inertia = inertia.sqrMagnitude;

            shipSpeedController.Update();

            if (isMoving)
                return;

            targetTransform.position += inertia * Time.deltaTime;
            inertia *= model.InertiaFadeMultiplier;
        }

        public void Move()
        {
            shipSpeedController.Accelerate();
            isMoving = true;

            var direction = targetTransform.up * (shipSpeedData.CurrentSpeed * Time.deltaTime);

            inertia = Vector3.Lerp(inertia, direction, Time.deltaTime);
            targetTransform.position += inertia * Time.deltaTime;
        }

        public void Stop()
        {
            isMoving = false;
            rotateValue = 0;
        }

        public void Turn(Vector2 direction)
        {
            rotateValue = Mathf.Lerp(rotateValue, direction.x * model.TurnSpeed, Time.deltaTime * 7f);

            targetTransform.Rotate(Vector3.back, direction.x * model.TurnSpeed * Time.deltaTime);
        }
    }
}