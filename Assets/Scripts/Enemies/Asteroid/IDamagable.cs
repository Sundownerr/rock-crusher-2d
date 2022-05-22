using System;

namespace Game
{
    public  interface IDamagable
    {
        event Action Damaged;
        void HandleDamaged();
    }
}