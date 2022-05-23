using UnityEngine;

namespace Game
{
    public interface IBulletWeaponController : IWeaponController, IFactory<Transform> {}
}