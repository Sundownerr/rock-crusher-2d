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

        public float Speed { get; private set; }

        public void Update()
        {
            Speed = Mathf.Clamp(Speed + speedDelta, 0, model.maxSpeed);
            speedDelta = -model.deceleration;
        }

        public void Accelerate()
        {
            speedDelta = model.acceleration;
        }
    }
}