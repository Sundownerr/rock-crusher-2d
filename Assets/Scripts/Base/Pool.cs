using System.Collections.Generic;
using Game.Base.Interface;

namespace Game
{
    public abstract class Pool<T> : IPool<T>
    {
        private readonly Stack<T> pool = new();

        public virtual T Give()
        {
            var item = pool.TryPop(out var deactivatedItem) ? deactivatedItem : GetNewItem();
            return item;
        }

        public virtual void Take(T item)
        {
            pool.Push(item);
        }

        protected abstract T GetNewItem();
    }
}