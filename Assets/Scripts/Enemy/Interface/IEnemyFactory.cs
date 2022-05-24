using UnityEngine;

namespace Game.Enemy.Factory.Interface
{
    public interface IEnemyFactory : IFactory<(IUpdate, Transform)>
    { }
}