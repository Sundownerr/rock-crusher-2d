using Game.Base.Interface;
using Game.Enemy.Interface;
using UnityEngine;

namespace Game.Enemy.Factory.Interface
{
    public interface IEnemyFactory<T> : IFactory<IEnemy, T>
    {
        (IEnemy controller, T model) Create(Vector3 position);
    }
}