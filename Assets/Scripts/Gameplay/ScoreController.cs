using Game.Base;
using Game.Enemy;

namespace Game
{
    public class ScoreController : Controller<ScoreData>, IDestroyable
    {
        private readonly EnemyController enemyController;

        public ScoreController(ScoreData model, EnemyController enemyController) : base(model)
        {
            this.enemyController = enemyController;

            enemyController.SmallAsteroidDestroyed += OnSmallAsteroidDestroyed;
            enemyController.MediumAsteroidDestroyed += OnMediumAsteroidDestroyed;
            enemyController.BigAsteroidDestroyed += OnBigAsteroidDestroyed;
            enemyController.UfoDestroyed += OnUfoDestroyed;
        }

        public void Destroy()
        {
            enemyController.SmallAsteroidDestroyed -= OnSmallAsteroidDestroyed;
            enemyController.MediumAsteroidDestroyed -= OnMediumAsteroidDestroyed;
            enemyController.BigAsteroidDestroyed -= OnBigAsteroidDestroyed;
            enemyController.UfoDestroyed -= OnUfoDestroyed;

            model.CurrentScore = 0;
        }

        private void OnUfoDestroyed()
        {
            model.CurrentScore += model.Ufo;
        }

        private void OnBigAsteroidDestroyed()
        {
            model.CurrentScore += model.BigAsteroid;
        }

        private void OnMediumAsteroidDestroyed()
        {
            model.CurrentScore += model.MediumAsteroid;
        }

        private void OnSmallAsteroidDestroyed()
        {
            model.CurrentScore += model.SmallAsteroid;
        }
    }
}