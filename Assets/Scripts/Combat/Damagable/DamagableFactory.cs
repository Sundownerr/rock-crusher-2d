using System;
using Game.Base;
using Game.Base.Interface;
using Game.Damagables.Interface;
using UnityEngine;

namespace Game.Damagables
{
    public abstract class DamagableFactory<T1, T2, T3> : IFactory<T2, T3>
        where T1 : GameObjectFactoryData
        where T2 : IDamagable
        where T3 : Damagable
    {
        protected readonly T1 factoryData;
        protected readonly Transform parent;

        protected DamagableFactory(T1 factoryData, Transform parent)
        {
            this.factoryData = factoryData;
            this.parent = parent;
        }

        public abstract event Action<T2, T3> Created;
    }
}