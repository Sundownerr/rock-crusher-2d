using System;

namespace Game
{
    public interface IFactory<T>
    {
        event Action<T> Created;

        T Create();
    }
}