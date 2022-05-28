using System;
using Game.Base;
using Game.Base.Interface;
using Game.Damagables.Interface;
using Game.Enemy.Asteroid;
using Game.Enemy.Base;
using Game.Enemy.Interface;
using Game.Enemy.UFO;
using Game.Ship;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Vfx
{
    public class VfxController : Controller<VfxData>, IDestroyable
    {
        private readonly ParticleSystem bigAsteroidDeathVfx;
        private readonly IFactory<IEnemy, EnemyDamagable> enemyDeathFactory;
        private readonly ParticleSystem mediumAsteroidDeathVfx;
        private readonly IDamagable shipController;
        private readonly ShipData shipData;
        private readonly ParticleSystem shipDeathVfx;
        private readonly ParticleSystem smallAsteroidDeathVfx;
        private readonly ParticleSystem ufoDeathVfx;

        public VfxController(VfxData model,
                             Transform parent,
                             ShipData shipData,
                             IDamagable shipController,
                             IFactory<IEnemy, EnemyDamagable> enemyDeathFactory) :
            base(model)
        {
            this.shipData = shipData;
            this.shipController = shipController;
            this.enemyDeathFactory = enemyDeathFactory;

            enemyDeathFactory.Created += OnEnemyDead;
            shipController.Damaged += OnShipDeath;

            bigAsteroidDeathVfx = GetVfx(model.BigAsteroidDeathVfx);
            mediumAsteroidDeathVfx = GetVfx(model.MediumAsteroidDeathVfx);
            smallAsteroidDeathVfx = GetVfx(model.SmallAsteroidDeathVfx);
            ufoDeathVfx = GetVfx(model.UfoDeathVfx);
            shipDeathVfx = GetVfx(model.ShipDeathVfx);

            ParticleSystem GetVfx(GameObject vfxPrefab) =>
                Object.Instantiate(vfxPrefab, parent).GetComponent<ParticleSystem>();
        }

        public void Destroy()
        {
            enemyDeathFactory.Created -= OnEnemyDead;
            shipController.Damaged -= OnShipDeath;
        }

        private void OnEnemyDead(IEnemy enemy, EnemyDamagable damagable)
        {
            switch (damagable)
            {
                case AsteroidData asteroid:
                    switch (asteroid.Stage)
                    {
                        case AsteroidData.AsteroidStage.Big:
                            Play(bigAsteroidDeathVfx, asteroid.transform);
                            break;
                        case AsteroidData.AsteroidStage.Medium:
                            Play(mediumAsteroidDeathVfx, asteroid.transform);
                            break;
                        case AsteroidData.AsteroidStage.Small:
                            Play(smallAsteroidDeathVfx, asteroid.transform);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                    break;
                case UfoData ufo:
                    Play(ufoDeathVfx, ufo.transform);
                    break;
            }
        }

        private static void Play(ParticleSystem vfx, Transform target)
        {
            vfx.transform.position = target.position;
            vfx.Play();
        }

        private void OnShipDeath()
        {
            Play(shipDeathVfx, shipData.transform);
        }
    }
}