using System;
using Game.Base;
using Game.Base.Interface;
using Game.Enemy.Asteroid;
using Game.Enemy.Base;
using Game.Enemy.Interface;
using Game.Enemy.UFO;

namespace Game.Score
{
    public class ScoreController : Controller<ScoreData>, IDestroyable
    {
        private readonly IFactory<IEnemy, EnemyDamagable> enemyDeathFactory;

        public ScoreController(ScoreData model, IFactory<IEnemy, EnemyDamagable> enemyDeathFactory) :
            base(model)
        {
            this.enemyDeathFactory = enemyDeathFactory;

            enemyDeathFactory.Created += OnEnemyDead;
        }

        public void Destroy()
        {
            enemyDeathFactory.Created -= OnEnemyDead;
        }

        private void OnEnemyDead(IEnemy enemy, EnemyDamagable damagable)
        {
            switch (damagable)
            {
                case AsteroidData asteroid:
                    switch (asteroid.Stage)
                    {
                        case AsteroidData.AsteroidStage.Big:
                            AddScore(model.BigAsteroid);
                            break;
                        case AsteroidData.AsteroidStage.Medium:
                            AddScore(model.MediumAsteroid);
                            break;
                        case AsteroidData.AsteroidStage.Small:
                            AddScore(model.SmallAsteroid);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                case UfoData ufo:
                    AddScore(model.Ufo);
                    break;
            }

            void AddScore(int score) => model.CurrentScore += score;
        }
    }
}