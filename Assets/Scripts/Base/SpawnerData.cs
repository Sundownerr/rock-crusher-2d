using UnityEngine;

namespace Game.Gameplay
{
    public class FactoryData : ScriptableObject
    {
        [SerializeField] public GameObject[] prefabs;
    }
}