namespace Game.Base.Interface
{
    public interface IPool<T>
    {
        T Give();
        void Take(T item);
    }
}