using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "GameplayData", menuName = "Data/Gameplay")]
    public class GameplayData : ScriptableObject
    {
        [SerializeField] public AsteroidSpawnerData asteroidSpawnerData;
        [SerializeField] public UfoSpawnerData ufoSpawnerData;
        [SerializeField] public ShipSpawnerData shipSpawnerData;
    }
}