namespace Game
{
    public interface IGameplayUIController : IUiController, IUpdate
    {
        void SetShip(Ship ship);
    }
}