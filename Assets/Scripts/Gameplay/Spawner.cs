using System;

namespace Game
{
    public abstract class Spawner<T, S> : IFactory<S>
    {
        protected readonly T model;

        protected Spawner(T model)
        {
            this.model = model;
        }

        public abstract event Action<S> Created;
    }
}