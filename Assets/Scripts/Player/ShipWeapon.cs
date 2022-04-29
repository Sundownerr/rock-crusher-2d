namespace Game
{
    public abstract class ShipWeapon
    {
        protected readonly ShipModel shipModel;

        protected ShipWeapon(ShipModel shipModel)
        {
            this.shipModel = shipModel;
        }
    }
}