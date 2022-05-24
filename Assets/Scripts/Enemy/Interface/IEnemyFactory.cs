using Game.Enemy.Interface;
using UnityEngine;

namespace Game.Enemy.Factory.Interface
{
    public interface IEnemyFactory : IFactory<(IEnemy, Transform)>
    { }
}