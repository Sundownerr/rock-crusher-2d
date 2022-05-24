using Game.Base;
using UnityEngine;

namespace Game.Enemy.Asteroid.Movement
{
    public class AsteroidMovementController : Controller<AsteroidSpeedData>, IUpdate
    {
        private readonly float speed;
        private readonly Transform targetTransform;

        public AsteroidMovementController(AsteroidSpeedData model, Transform targetTransform) : base(model)
        {
            this.targetTransform = targetTransform;

            var randomRotation = Random.rotation.eulerAngles;
            randomRotation.x = 0;
            randomRotation.y = 0;

            targetTransform.rotation = Quaternion.Euler(randomRotation);
            speed = Random.Range(model.minMaxSpeed.x, model.minMaxSpeed.y);
        }

        public void Update()
        {
            targetTransform.position += targetTransform.up * (speed * Time.deltaTime);
        }
    }
}