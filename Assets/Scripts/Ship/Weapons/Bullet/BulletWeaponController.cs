using System;
using System.Collections;
using System.Collections.Generic;
using Game.Base;
using Game.Gameplay.Utility;
using Game.Ship.Weapons.Bullet.Interface;
using UnityEngine;

namespace Game.Ship.Weapons.Bullet
{
    public class BulletWeaponController : Controller<BulletWeaponData>, IBulletWeaponController
    {
        private readonly List<Transform> bullets = new();
        private readonly Dictionary<Transform, Coroutine> destroyCoroutines = new();
        private readonly WaitForSeconds destroyDelay;
        private readonly BulletPool pool;
        private readonly RaycastHit2D[] results = new RaycastHit2D[1];
        private readonly CoroutineRunner runner;
        private readonly Transform shootPoint;

        public BulletWeaponController(BulletWeaponData model,
                                      BulletPool pool,
                                      Transform shootPoint,
                                      CoroutineRunner runner) : base(model)
        {
            this.pool = pool;
            this.shootPoint = shootPoint;
            this.runner = runner;

            destroyDelay = new WaitForSeconds(model.DestroyDelay);
        }

        public void Update()
        {
            for (var i = 0; i < bullets.Count; i++)
            {
                var bullet = bullets[i];
                var bulletActive = bullet.gameObject.activeSelf;

                if (!bulletActive)
                {
                    bullets.Remove(bullet);
                    continue;
                }

                bullet.position += bullet.up * (Time.deltaTime * model.BulletSpeed);
                var hitCount = Physics2D.RaycastNonAlloc(bullet.position, bullet.forward, results, 1,
                    model.BulletTargetLayer);

                for (var j = 0; j < hitCount; j++)
                {
                    bullets.Remove(bullet);
                    pool.Return(bullet);

                    if (destroyCoroutines.TryGetValue(bullet, out var coroutine))
                    {
                        runner.StopCoroutine(coroutine);
                        destroyCoroutines.Remove(bullet);
                    }

                    Hit?.Invoke(results[j].transform);
                }
            }
        }

        public event Action<Transform> Hit;

        public void Shoot()
        {
            var bullet = pool.Get();
            bullet.position = shootPoint.position;
            bullet.rotation = shootPoint.rotation;

            bullets.Add(bullet);

            var coroutine = runner.StartCoroutine(DestroyBullet(bullet));
            destroyCoroutines.Add(bullet, coroutine);
        }

        private IEnumerator DestroyBullet(Transform bullet)
        {
            yield return destroyDelay;

            if (bullet == null)
                yield break;

            if (!bullet.gameObject.activeSelf)
                yield break;

            destroyCoroutines.Remove(bullet);
            pool.Return(bullet);
        }
    }
}