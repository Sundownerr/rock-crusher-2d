namespace Game.Enemy.Interface
{
    public interface IEnemy : IUpdate
    {
        void HandleDamaged();
    }
}