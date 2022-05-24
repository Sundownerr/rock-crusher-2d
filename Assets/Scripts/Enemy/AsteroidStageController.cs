using System;
using System.Collections.Generic;
using Game.Enemy.Asteroid;
using Game.Enemy.Asteroid.Interface;
using Game.Enemy.Factory.Interface;
using Object = UnityEngine.Object;

namespace Game.Enemy
{
    public class AsteroidStageController : IDestroyable, IUpdate
    {
        private readonly IAsteroidFactory asteroidFactory;
        private readonly List<AsteroidData> asteroids = new List<AsteroidData>();

        public AsteroidStageController(IAsteroidFactory asteroidFactory)
        {
            this.asteroidFactory = asteroidFactory;
            asteroidFactory.Created += AsteroidFactoryOnCreated;
        }

        public void Destroy()
        {
            asteroidFactory.Created -= AsteroidFactoryOnCreated;
        }

        public void Update()
        {
            for (var i = 0; i < asteroids.Count; i++)
            {
                if (asteroids[i].IsCompletlyDestroyed)
                {
                    Object.Destroy(asteroids[i].gameObject);
                    asteroids.RemoveAt(i);
                    continue;
                }

                if (asteroids[i].IsDamaged)
                {
                    switch (asteroids[i].Stage)
                    {
                        case AsteroidData.AsteroidStage.Big:
                            for (var j = 0; j < 2; j++)
                                asteroidFactory.CreateMedium(asteroids[i].transform.position);
                            break;
                        case AsteroidData.AsteroidStage.Medium:
                            for (var j = 0; j < 2; j++)
                                asteroidFactory.CreateSmall(asteroids[i].transform.position);
                            break;
                        case AsteroidData.AsteroidStage.Small:
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    Object.Destroy(asteroids[i].gameObject);
                    asteroids.RemoveAt(i);
                }
            }
        }

        private void AsteroidFactoryOnCreated((IAsteroid asteroid, AsteroidData data) result)
        {
            asteroids.Add(result.data);
        }
    }
}