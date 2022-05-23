using System;

namespace Game.Combat.Interface
{
    public interface IDamagable
    {
        event Action Damaged;
        void HandleDamaged();
    }
}