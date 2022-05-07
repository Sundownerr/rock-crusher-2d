using System;

namespace Game
{
    public class Damagable : IDamagable
    {
        public event Action Damaged;

        public void HandleDamaged()
        {
            Damaged?.Invoke();
        }
    }
}