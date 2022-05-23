using Game.Movement;
using UnityEngine;

namespace Game.Enemies.Asteroid
{
    [CreateAssetMenu(fileName = "AsteroidData", menuName = "Data/Asteroid Data")]
    public class AsteroidData : ScriptableObject
    {
        [SerializeField] private SpeedData speedData;
        [SerializeField] protected GameObject prefab;
        public GameObject Prefab => prefab;
    }
}