using Game.Combat.Interface;
using UnityEngine;

namespace Game.Weapons.Bullet.Interface
{
    public interface IBulletWeaponController : IWeaponController, IFactory<Transform>
    { }
}