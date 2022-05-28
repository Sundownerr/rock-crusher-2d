using System;
using System.Collections.Generic;
using Game.Base.Interface;

namespace Game.Base
{
    public abstract class Pool<T> : IContainer<T>
    {
        private readonly Stack<T> pool = new();

        public event Action<T> ItemGiven;
        public event Action<T> ItemReturned;

        public virtual T Get()
        {
            var item = pool.TryPop(out var poolItem) ? poolItem : GetNew();

            ActivateItem(item);
            ItemGiven?.Invoke(item);
            return item;
        }

        public virtual void Return(T item)
        {
            pool.Push(item);
            DeactivateItem(item);
            ItemReturned?.Invoke(item);
        }

        protected abstract T GetNew();
        protected abstract void ActivateItem(T item);
        protected abstract void DeactivateItem(T item);
    }

    public abstract class Pool<T1, T2> : IContainer<T1, T2>
    {
        private readonly Stack<(T1, T2)> pool = new();

        public event Action<T1, T2> ItemGiven;
        public event Action<T1, T2> ItemReturned;

        public (T1, T2) Get()
        {
            var item = pool.TryPop(out var poolItem) ? poolItem : GetNew();

            ActivateItem(item.Item1, item.Item2);
            ItemGiven?.Invoke(item.Item1, item.Item2);
            return item;
        }

        public void Return(T1 item1, T2 item2)
        {
            pool.Push((item1, item2));
            DeactivateItem(item1, item2);
            ItemReturned?.Invoke(item1, item2);
        }

        protected abstract (T1, T2) GetNew();
        protected abstract void ActivateItem(T1 item1, T2 item2);
        protected abstract void DeactivateItem(T1 item1, T2 item2);
    }
}