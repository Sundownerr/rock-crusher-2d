using System;
using Game.Base.Interface;

namespace Game.Damagables.Interface
{
    public interface IDamagable : IUpdate
    {
        event Action Damaged;
    }
}