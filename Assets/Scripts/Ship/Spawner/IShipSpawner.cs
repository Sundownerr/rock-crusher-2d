using Game.Gameplay.Utility;
using Game.Ship.Interface;
using UnityEngine;

namespace Game.Ship.Spawner.Interface
{
    public interface IShipSpawner : IFactory<(IShipController, IFactory<Transform>, Transform)>
    {
        void Spawn(CoroutineRunner runner);
    }
}