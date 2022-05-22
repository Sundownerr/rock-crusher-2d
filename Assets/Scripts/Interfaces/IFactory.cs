using System;

namespace Game
{
    public interface IFactory<T>
    {
        public event Action<T> Created;
    }
}