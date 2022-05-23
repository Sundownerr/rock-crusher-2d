using Game.Base;
using Game.Movement;
using UnityEngine;

namespace Game.Enemies.Asteroid
{
    public class AsteroidMovementController : Controller<AsteroidSpeedData>, IUpdate
    {
        private readonly Transform targetTransform;
        private readonly float speed;

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