using System;
using Game.Base.Interface;

namespace Game.Base
{
    public abstract class Container<T1> : IContainer<T1>
    {
        public event Action<T1> ItemGiven;
        public event Action<T1> ItemReturned;

        public T1 Get()
        {
            var item = GetItem();
            ItemGiven?.Invoke(item);

            return item;
        }

        public void Return(T1 item)
        {
            ReturnItem(item);
            ItemReturned?.Invoke(item);
        }

        protected abstract T1 GetItem();
        protected abstract void ReturnItem(T1 item);
    }

    public abstract class Container<T1, T2> : IContainer<T1, T2>
    {
        public event Action<T1, T2> ItemGiven;
        public event Action<T1, T2> ItemReturned;

        public (T1, T2) Get()
        {
            var item = GetItem();
            ItemGiven?.Invoke(item.Item1, item.Item2);

            return item;
        }

        public void Return(T1 item1, T2 item2)
        {
            ReturnItem(item1, item2);
            ItemReturned?.Invoke(item1, item2);
        }

        protected abstract (T1, T2) GetItem();
        protected abstract void ReturnItem(T1 item1, T2 item2);
    }
}