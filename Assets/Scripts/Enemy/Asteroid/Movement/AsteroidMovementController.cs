using Game.Base;
using Game.Base.Interface;
using UnityEngine;

namespace Game.Enemy.Asteroid.Movement
{
    public class AsteroidMovementController : Controller<AsteroidSpeedData>, IUpdate
    {
        private readonly Transform targetTransform;

        public AsteroidMovementController(AsteroidSpeedData model, Transform targetTransform) : base(model)
        {
            this.targetTransform = targetTransform;

            var randomRotation = Random.rotation.eulerAngles;
            randomRotation.x = 0;
            randomRotation.y = 0;

            targetTransform.rotation = Quaternion.Euler(randomRotation);
            this.model.CurrentSpeed = Random.Range(model.minMaxSpeed.x, model.minMaxSpeed.y);
            this.model.CurrentRotationSpeed = Random.Range(model.minMaxRotationSpeed.x, model.minMaxRotationSpeed.y);
        }

        public void Update()
        {
            targetTransform.position += targetTransform.up * (model.CurrentSpeed * Time.deltaTime);
            targetTransform.GetChild(0).Rotate(0, 0, model.CurrentRotationSpeed * Time.deltaTime);
        }
    }
}