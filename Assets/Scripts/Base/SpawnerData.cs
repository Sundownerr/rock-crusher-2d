using UnityEngine;

namespace Game.Gameplay
{
    public class SpawnerData : ScriptableObject
    {
        [SerializeField] public GameObject[] prefabs;
    }
}