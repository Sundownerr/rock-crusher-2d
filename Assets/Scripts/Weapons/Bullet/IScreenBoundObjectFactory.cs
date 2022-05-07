using System;
using UnityEngine;

namespace Game
{
    public interface IScreenBoundObjectFactory
    {
        public event Action<Transform> ObjectCreated;
    }
}