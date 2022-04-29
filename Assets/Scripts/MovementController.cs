using UnityEngine;

namespace Game
{
    public interface IMovementController : IUpdate
    {
        void Move(float speed);
    }

    public class MovementController : IMovementController
    {
        private readonly MovementModel model;
        private readonly Transform targetTransform;
        private Vector3 inertia;

        public MovementController(MovementModel model, Transform targetTransform)
        {
            this.model = model;
            this.targetTransform = targetTransform;
        }

        public void Update()
        {
            if (inertia.sqrMagnitude > 0)
                inertia -= inertia * model.inertiaFadeForce;

            targetTransform.position += inertia;
        }

        public void Move(float speed)
        {
            inertia = targetTransform.forward * speed * Time.deltaTime;
        }
    }
}