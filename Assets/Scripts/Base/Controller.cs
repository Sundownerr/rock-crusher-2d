namespace Game.Base
{
    public abstract class Controller<T>
    {
        protected readonly T model;

        protected Controller(T model)
        {
            this.model = model;
        }
    }
}