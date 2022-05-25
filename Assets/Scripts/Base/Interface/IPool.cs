using System;

namespace Game.Base.Interface
{
    public interface IPool<T>
    {
        event Action<T> ItemGiven;
        event Action<T> ItemReturned;
        T Get();
        void Return(T item);
    }
}