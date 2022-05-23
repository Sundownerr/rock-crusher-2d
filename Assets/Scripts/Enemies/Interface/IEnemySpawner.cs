using UnityEngine;

namespace Game.Enemies.Asteroid.Spawner.Interface
{
    public interface IEnemySpawner : IFactory<(IUpdate, Transform)>, IDestroyable
    {
        void StartSpawn();
    }
}