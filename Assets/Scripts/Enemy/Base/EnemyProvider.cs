using Game.Base;
using Game.Base.Interface;
using Game.Enemy.Interface;

namespace Game.Enemy.Base
{
    public class EnemyProvider<T1> : Container<IEnemy, T1>
        where T1 : EnemyDamagable
    {
        protected IContainer<IEnemy, T1> pool;

        protected override (IEnemy, T1) GetItem() => pool.Get();

        protected override void ReturnItem(IEnemy item1, T1 item2)
        {
            pool.Return(item1, item2);
        }
    }
}