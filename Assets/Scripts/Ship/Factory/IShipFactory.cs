using Game.Ship.Interface;
using UnityEngine;

namespace Game.Ship.Factory.Interface
{
    public interface IShipFactory : IFactory<(IShipController, IFactory<Transform>, ShipData)>
    { }
}