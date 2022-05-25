using System;
using System.Collections.Generic;
using Game.Base.Interface;

namespace Game
{
    public abstract class Pool<T> : IPool<T>
    {
        private readonly Stack<T> pool = new();

        public event Action<T> ItemGiven;
        public event Action<T> ItemReturned;

        public virtual T Get()
        {
            var item = pool.TryPop(out var deactivatedItem) ? deactivatedItem : GetNew();

            ItemGiven?.Invoke(item);
            return item;
        }

        public virtual void Return(T item)
        {
            pool.Push(item);
            ItemReturned?.Invoke(item);
        }

        protected abstract T GetNew();
    }
}