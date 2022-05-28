using System;
using Game.Base.Interface;
using Game.Enemy.Interface;

namespace Game.Enemy.Asteroid.Factory
{
    public class AsteroidStageController : IDestroyable
    {
        private readonly AsteroidProvider bigAsteroidProvider;
        private readonly AsteroidProvider mediumAsteroidProvider;
        private readonly AsteroidProvider smallAsteroidProvider;

        public AsteroidStageController(AsteroidProvider bigAsteroidProvider,
                                       AsteroidProvider mediumAsteroidProvider,
                                       AsteroidProvider smallAsteroidProvider)
        {
            this.bigAsteroidProvider = bigAsteroidProvider;
            this.mediumAsteroidProvider = mediumAsteroidProvider;
            this.smallAsteroidProvider = smallAsteroidProvider;

            bigAsteroidProvider.ItemGiven += AsteroidFactoryOnCreated;
            mediumAsteroidProvider.ItemGiven += AsteroidFactoryOnCreated;
            smallAsteroidProvider.ItemGiven += AsteroidFactoryOnCreated;
        }

        public void Destroy()
        {
            bigAsteroidProvider.ItemGiven -= AsteroidFactoryOnCreated;
            mediumAsteroidProvider.ItemGiven -= AsteroidFactoryOnCreated;
            smallAsteroidProvider.ItemGiven -= AsteroidFactoryOnCreated;
        }

        private void AsteroidFactoryOnCreated(IEnemy enemy, AsteroidData asteroid)
        {
            enemy.Damaged += OnDamaged;

            void OnDamaged()
            {
                enemy.Damaged -= OnDamaged;

                if (asteroid.IsCompletlyDestroyed)
                    return;

                switch (asteroid.Stage)
                {
                    case AsteroidData.AsteroidStage.Big:
                        for (var j = 0; j < 2; j++)
                        {
                            var newAsteroid = mediumAsteroidProvider.Get();
                            newAsteroid.Item2.transform.position = asteroid.transform.position;
                        }

                        break;
                    case AsteroidData.AsteroidStage.Medium:
                        for (var j = 0; j < 2; j++)
                        {
                            var newAsteroid = smallAsteroidProvider.Get();
                            newAsteroid.Item2.transform.position = asteroid.transform.position;
                        }

                        break;
                    case AsteroidData.AsteroidStage.Small:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}