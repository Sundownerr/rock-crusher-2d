using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "Level", menuName = "Data/Level")]
    public class Level : ScriptableObject
    {
        [SerializeField] public AsteroidSpawnerData asteroidSpawnerData;
        [SerializeField] public UfoSpawnerData ufoSpawnerData;
        [SerializeField] public ShipSpawnerData shipSpawnerData;
    }
}