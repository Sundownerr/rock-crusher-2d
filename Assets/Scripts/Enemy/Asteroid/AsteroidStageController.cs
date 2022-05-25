using System;
using System.Collections.Generic;
using Game.Enemy.Asteroid;
using Game.Enemy.Interface;

namespace Game.Enemy
{
    public class AsteroidStageController : IDestroyable, IUpdate
    {
        private readonly List<AsteroidData> asteroids = new();
        private readonly EnemyContainer<AsteroidData> bigAsteroidContainer;
        private readonly EnemyContainer<AsteroidData> mediumAsteroidContainer;
        private readonly EnemyContainer<AsteroidData> smallAsteroidContainer;

        public AsteroidStageController(EnemyContainer<AsteroidData> bigAsteroidContainer,
                                       EnemyContainer<AsteroidData> mediumAsteroidContainer,
                                       EnemyContainer<AsteroidData> smallAsteroidContainer)
        {
            this.bigAsteroidContainer = bigAsteroidContainer;
            this.mediumAsteroidContainer = mediumAsteroidContainer;
            this.smallAsteroidContainer = smallAsteroidContainer;

            bigAsteroidContainer.ItemGiven += AsteroidFactoryOnCreated;
            mediumAsteroidContainer.ItemGiven += AsteroidFactoryOnCreated;
            smallAsteroidContainer.ItemGiven += AsteroidFactoryOnCreated;
        }

        public void Destroy()
        {
            bigAsteroidContainer.ItemGiven -= AsteroidFactoryOnCreated;
            mediumAsteroidContainer.ItemGiven -= AsteroidFactoryOnCreated;
            smallAsteroidContainer.ItemGiven -= AsteroidFactoryOnCreated;
        }

        public void Update()
        {
            for (var i = 0; i < asteroids.Count; i++)
            {
                if (!asteroids[i].IsDamaged || asteroids[i].IsCompletlyDestroyed)
                    continue;

                switch (asteroids[i].Stage)
                {
                    case AsteroidData.AsteroidStage.Big:
                        for (var j = 0; j < 2; j++)
                        {
                            var newAsteroid = mediumAsteroidContainer.Get();
                            newAsteroid.Item2.transform.position = asteroids[i].transform.position;
                        }

                        break;
                    case AsteroidData.AsteroidStage.Medium:
                        for (var j = 0; j < 2; j++)
                        {
                            var newAsteroid = smallAsteroidContainer.Get();
                            newAsteroid.Item2.transform.position = asteroids[i].transform.position;
                        }

                        break;
                    case AsteroidData.AsteroidStage.Small:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                asteroids.RemoveAt(i);
            }
        }

        private void AsteroidFactoryOnCreated(IEnemy arg1, AsteroidData arg2)
        {
            asteroids.Add(arg2);
        }
    }
}