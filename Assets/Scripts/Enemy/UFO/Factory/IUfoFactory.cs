using Game.Base.Interface;
using Game.Enemy.Interface;

namespace Game.Enemy.UFO.Factory
{
    public interface IUfoFactory : IFactory<(IEnemy, UfoData)>
    { }
}