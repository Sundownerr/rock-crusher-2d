using UnityEngine;

namespace Game
{
    public abstract class ShipWeapon
    {
        protected readonly Transform shootPoint;

        protected ShipWeapon(Transform shootPoint)
        {
            this.shootPoint = shootPoint;
        }
    }

    public interface IShipWeapon
    {
        void Shoot();
    }
}