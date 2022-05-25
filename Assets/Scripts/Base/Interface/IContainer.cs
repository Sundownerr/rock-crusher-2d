using System;

namespace Game.Enemy
{
    public interface IContainer<T>
    {
        event Action<T> ItemGiven;
        event Action<T> ItemReturned;
        T Get();
        void Return(T item);
    }

    public interface IContainer<T1, T2>
    {
        event Action<T1, T2> ItemGiven;
        event Action<T1, T2> ItemReturned;
        (T1, T2) Get();
        void Return((T1, T2) item);
    }
}