using System;
using System.Collections;
using Game.Base;
using Game.Gameplay.Utility;
using Game.Ship.Weapons.Laser.Interface;
using UnityEngine;

namespace Game.Ship.Weapons.Laser
{
    public class LaserWeaponController : Controller<LaserWeaponData>, ILaserWeaponController
    {
        private readonly float cooldownDelta = 0.1f;
        private readonly WaitForSeconds cooldownDeltaWaitForSeconds;
        private readonly WaitForSeconds delay;
        private readonly RaycastHit2D[] results = new RaycastHit2D[10];
        private readonly CoroutineRunner runner;
        private readonly Transform shootPoint;
        private int chargesToCooldown;

        public LaserWeaponController(LaserWeaponData model, Transform shootPoint, CoroutineRunner runner) : base(model)
        {
            this.shootPoint = shootPoint;
            this.runner = runner;

            model.CurrentCharges = model.MaxCharges;
            delay = new WaitForSeconds(model.Delay);
            cooldownDeltaWaitForSeconds = new WaitForSeconds(cooldownDelta);
        }

        public event Action<Transform> Hit;

        public void Shoot()
        {
            CanShoot = model.CurrentCharges - 1 >= 0;

            if (model.CurrentCharges <= 0)
                return;

            model.CurrentCharges--;
            runner.StartCoroutine(ShootLaser());

            if (chargesToCooldown == 0)
                runner.StartCoroutine(Cooldown());

            chargesToCooldown++;
        }

        public bool CanShoot { get; private set; }

        private IEnumerator Cooldown()
        {
            model.CurrentCooldown = model.CooldownTime;

            while (model.CurrentCooldown > 0)
            {
                model.CurrentCooldown -= cooldownDelta;
                yield return cooldownDeltaWaitForSeconds;
            }

            model.CurrentCharges++;
            chargesToCooldown--;

            if (chargesToCooldown > 0)
                runner.StartCoroutine(Cooldown());
        }

        private IEnumerator ShootLaser()
        {
            yield return delay;

            var size = new Vector2(model.SizeX, model.SizeY);

            var boxCast = Physics2D.BoxCastNonAlloc(
                shootPoint.transform.position,
                size,
                0,
                shootPoint.transform.up,
                results);

            for (var i = 0; i < boxCast; i++)
                Hit?.Invoke(results[i].transform);
        }
    }
}