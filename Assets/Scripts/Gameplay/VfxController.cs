using Game.Base;
using Game.Enemy;
using UnityEngine;

namespace Game
{
    public class VfxController : Controller<VfxData>, IDestroyable
    {
        private readonly ParticleSystem bigAsteroidDeathVfx;
        private readonly EnemyController enemyController;
        private readonly GameplayController gameplayController;
        private readonly ParticleSystem mediumAsteroidDeathVfx;
        private readonly ParticleSystem shipDeathVfx;
        private readonly ParticleSystem smallAsteroidDeathVfx;
        private readonly ParticleSystem ufoDeathVfx;

        public VfxController(VfxData model,
                             EnemyController enemyController,
                             GameplayController gameplayController,
                             Transform parent) :
            base(model)
        {
            this.enemyController = enemyController;
            this.gameplayController = gameplayController;
            enemyController.SmallAsteroidDestroyed += OnSmallDeath;
            enemyController.MediumAsteroidDestroyed += OnMediumDeath;
            enemyController.BigAsteroidDestroyed += OnBigDeath;
            enemyController.UfoDestroyed += OnUfoDeath;
            gameplayController.ShipDestroyed += OnShipDeath;

            bigAsteroidDeathVfx = Object.Instantiate(model.BigAsteroidDeathVfx, parent).GetComponent<ParticleSystem>();
            mediumAsteroidDeathVfx =
                Object.Instantiate(model.MediumAsteroidDeathVfx, parent).GetComponent<ParticleSystem>();
            smallAsteroidDeathVfx =
                Object.Instantiate(model.SmallAsteroidDeathVfx, parent).GetComponent<ParticleSystem>();
            ufoDeathVfx = Object.Instantiate(model.UfoDeathVfx, parent).GetComponent<ParticleSystem>();
            shipDeathVfx = Object.Instantiate(model.ShipDeathVfx, parent).GetComponent<ParticleSystem>();
        }

        public void Destroy()
        {
            enemyController.SmallAsteroidDestroyed -= OnSmallDeath;
            enemyController.MediumAsteroidDestroyed -= OnMediumDeath;
            enemyController.BigAsteroidDestroyed -= OnBigDeath;
            enemyController.UfoDestroyed -= OnUfoDeath;
            gameplayController.ShipDestroyed -= OnShipDeath;
        }

        public void PlayVfx(ParticleSystem vfx, Transform target)
        {
            vfx.transform.position = target.position;
            vfx.Play();
        }

        private void OnShipDeath(Transform target)
        {
            PlayVfx(shipDeathVfx, target);
        }

        private void OnUfoDeath(Transform target)
        {
            PlayVfx(ufoDeathVfx, target);
        }

        private void OnBigDeath(Transform target)
        {
            PlayVfx(bigAsteroidDeathVfx, target);
        }

        private void OnMediumDeath(Transform target)
        {
            PlayVfx(mediumAsteroidDeathVfx, target);
        }

        private void OnSmallDeath(Transform target)
        {
            PlayVfx(smallAsteroidDeathVfx, target);
        }
    }
}