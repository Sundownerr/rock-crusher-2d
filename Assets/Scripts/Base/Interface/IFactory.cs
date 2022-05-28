using System;

namespace Game.Base.Interface
{
    public interface IFactory<T>
    {
        event Action<T> Created;
    }

    public interface IFactory<T1, T2>
    {
        event Action<T1, T2> Created;
    }

    public interface IFactory<T1, T2, T3>
    {
        event Action<T1, T2, T3> Created;
    }
}