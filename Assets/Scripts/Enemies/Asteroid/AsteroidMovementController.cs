using Game.Base;
using Game.Movement;
using UnityEngine;

namespace Game.Enemies.Asteroid
{
    public class AsteroidMovementController: Controller<SpeedData> , IUpdate
    {
        private readonly Transform targetTransform;

        public AsteroidMovementController(SpeedData model, Transform targetTransform) : base(model)
        {
            this.targetTransform = targetTransform;

            var randomRotation = Random.rotation.eulerAngles;
            randomRotation.x = 0;
            randomRotation.y = 0;

            targetTransform.rotation = Quaternion.Euler(randomRotation);
        }

        public void Update()
        {
            targetTransform.position += targetTransform.up * model.CurrentSpeed;
        }
    }
}