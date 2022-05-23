using Game.Base;
using Game.Movement.Interface;
using UnityEngine;

namespace Game.Ship.Movement
{
    public class ShipSpeedController : Controller<ShipSpeedData>, IShipSpeedController
    {
        private float speedDelta;

        public ShipSpeedController(ShipSpeedData model) : base(model)
        { }

        public void Update()
        {
            model.CurrentSpeed = Mathf.Clamp(model.CurrentSpeed + speedDelta, 0, model.maxSpeed);
            speedDelta = -model.deceleration;
        }

        public void Accelerate()
        {
            speedDelta = model.acceleration;
        }
    }
}