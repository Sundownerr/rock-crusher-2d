using UnityEngine;

namespace Game
{
    public interface ISpeedController : IUpdate
    {
        void StartAccelerating();
        void StopAccelerating();
    }

    public class SpeedController : ISpeedController
    {
        private readonly SpeedModel model;
        private bool isAccelerating;

        public SpeedController(SpeedModel model)
        {
            this.model = model;
        }

        public float Speed { get; private set; }

        public void Update()
        {
            var speedDelta = isAccelerating ? model.acceleration : model.deceleration;

            Speed = Mathf.Clamp(Speed + speedDelta, 0, model.maxSpeed);
        }

        public void StartAccelerating()
        {
            isAccelerating = true;
        }

        public void StopAccelerating()
        {
            isAccelerating = false;
        }
    }
}