using UnityEngine;

namespace Game
{
    public class SpeedController : ISpeedController
    {
        private readonly SpeedData model;

        private float speedDelta;

        public SpeedController(SpeedData model)
        {
            this.model = model;
        }

        public void Update()
        {
            Speed = Mathf.Clamp(Speed + speedDelta, 0, model.maxSpeed);
            speedDelta = -model.deceleration;
        }

        public void Accelerate()
        {
            speedDelta = model.acceleration;
        }

        public float Speed { get; private set; }
    }
}