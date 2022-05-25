using Game.Base.Interface;
using Game.Enemy.Asteroid;
using Game.Enemy.Asteroid.Interface;
using UnityEngine;

namespace Game.Enemy.Factory.Interface
{
    public interface IAsteroidFactory : IFactory<(IAsteroid asteroid, AsteroidData data)>
    {
        void CreateSmall(Vector3 position);
        void CreateMedium(Vector3 position);
    }
}