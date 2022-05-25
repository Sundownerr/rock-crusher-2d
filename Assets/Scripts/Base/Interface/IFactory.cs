using System;

namespace Game.Base.Interface
{
    public interface IFactory<T>
    {
        event Action<T> Created;

        T Create();
    }
}