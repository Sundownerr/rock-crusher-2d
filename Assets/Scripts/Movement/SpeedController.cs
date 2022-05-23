using Game.Base;
using Game.Movement.Interface;
using UnityEngine;

namespace Game.Movement
{
    public class SpeedController : Controller<SpeedData>, ISpeedController
    {
        private float speedDelta;

        public SpeedController(SpeedData model) : base(model)
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