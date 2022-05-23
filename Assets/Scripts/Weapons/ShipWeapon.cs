using UnityEngine;

namespace Game.Weapons
{
    public abstract class ShipWeapon
    {
        protected readonly Transform shootPoint;

        protected ShipWeapon(Transform shootPoint)
        {
            this.shootPoint = shootPoint;
        }
    }
}