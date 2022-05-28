using System.Collections.Generic;
using System.Linq;
using Game.Base.Interface;
using Game.Enemy.Base;
using Game.Enemy.Interface;
using Game.Enemy.UFO;

namespace Game.Enemy
{
    public class EnemyController : IUpdate, IDestroyable
    {
        private readonly IFactory<IEnemy, EnemyDamagable> deathFactory;
        private readonly List<IEnemy> enemies = new();
        private readonly IFactory<IEnemy, EnemyDamagable> spawnFactory;

        public EnemyController(IFactory<IEnemy, EnemyDamagable> spawnFactory,
                               IFactory<IEnemy, EnemyDamagable> deathFactory)
        {
            this.spawnFactory = spawnFactory;
            this.deathFactory = deathFactory;

            spawnFactory.Created += OnEnemyCreated;
            deathFactory.Created += OnEnemyDead;
        }

        public void Destroy()
        {
            enemies.Clear();
            spawnFactory.Created -= OnEnemyCreated;
            deathFactory.Created -= OnEnemyDead;
        }

        public void Update()
        {
            for (var i = 0; i < enemies.Count; i++)
                enemies[i].Update();
        }

        private void OnEnemyDead(IEnemy enemy, EnemyDamagable damagable)
        {
            enemies.Remove(enemy);
        }

        private void OnEnemyCreated(IEnemy enemy, EnemyDamagable damagable)
        {
            enemies.Add(enemy);
        }

        public void HandleShipDestroyed()
        {
            var ufos = enemies.Where(x => x is UfoController).ToArray();

            foreach (var ufo in ufos)
                enemies.Remove(ufo);
        }
    }
}