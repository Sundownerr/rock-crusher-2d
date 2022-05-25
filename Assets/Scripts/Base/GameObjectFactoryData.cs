using UnityEngine;

namespace Game.Base
{
    public abstract class GameObjectFactoryData : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        public GameObject Prefab => prefab;
    }
}