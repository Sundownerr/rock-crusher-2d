using Game.Base;
using Game.Base.Interface;
using UnityEngine;

namespace Game.Enemy.UFO.Movement
{
    public class UfoMovementController : Controller<UfoMovementData>, IUpdate
    {
        private readonly Transform ufoTarget;
        private readonly Transform ufoTransform;

        public UfoMovementController(UfoMovementData model, Transform ufoTransform, Transform ufoTarget) : base(model)
        {
            this.ufoTransform = ufoTransform;
            this.ufoTarget = ufoTarget;
        }

        public void Update()
        {
            var direction = ufoTarget.position - ufoTransform.position;

            ufoTransform.position += direction.normalized * (Time.deltaTime * model.Speed);
        }
    }
}