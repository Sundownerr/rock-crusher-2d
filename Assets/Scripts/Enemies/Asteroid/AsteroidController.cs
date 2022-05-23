using Game.Base;
using UnityEngine;

namespace Game.Enemies.Asteroid
{
    public class AsteroidController : Controller<AsteroidData>, IUpdate
    {
        private readonly Transform target;

        public AsteroidController(AsteroidData model, Transform target) : base(model)
        {
            this.target = target;
        }

        public void Update()
        { }
    }
}