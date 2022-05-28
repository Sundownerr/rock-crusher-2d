using Game.Base.Interface;
using UnityEngine;

namespace Game.Ship.Factory.Interface
{
    public interface IShipFactory : IFactory<ShipController, ShipData, IContainer<Transform>>
    {
        (ShipController, ShipData, IContainer<Transform>) Create();
    }
}