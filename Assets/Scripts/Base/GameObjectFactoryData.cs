using System;
using UnityEngine;

namespace Game.Base
{
    [Serializable]
    public abstract class GameObjectFactoryData
    {
        [SerializeField] private GameObject prefab;
        public GameObject Prefab => prefab;
    }
}