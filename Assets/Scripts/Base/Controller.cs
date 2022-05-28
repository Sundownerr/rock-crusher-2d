namespace Game.Base
{
    public abstract class Controller<T>
    {
        protected T model;

        protected Controller(T model)
        {
            this.model = model;
        }
    }
}