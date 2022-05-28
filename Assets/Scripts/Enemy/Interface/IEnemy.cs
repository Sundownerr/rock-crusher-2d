using System;
using Game.Damagables.Interface;

namespace Game.Enemy.Interface
{
    public interface IEnemy : IDamagable
    {
        event Action CompletelyDestroyed;
    }
}