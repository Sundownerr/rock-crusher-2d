using Game.Enemies.Asteroid;
using Game.Enemies.UFO;
using Game.Input;
using Game.Movement;
using Game.PlayerShip;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "GameplayData", menuName = "Data/Gameplay")]
    public class GameplayData : ScriptableObject
    {
        [SerializeField] private AsteroidSpawnerData asteroidSpawnerData;
        [SerializeField] private UfoSpawnerData ufoSpawnerData;
        [SerializeField] private ShipSpawnerData shipSpawnerData;
        [SerializeField] private ShipWeaponsData shipWeaponsData;
        [SerializeField] private ShipMovementData shipMovementData;
        [SerializeField] private PlayerInputData playerInputData;
        [SerializeField] private SpeedData shipSpeedData;

        public AsteroidSpawnerData AsteroidSpawnerData => asteroidSpawnerData;
        public UfoSpawnerData UfoSpawnerData => ufoSpawnerData;
        public ShipSpawnerData ShipSpawnerData => shipSpawnerData;
        public ShipWeaponsData ShipWeaponsData => shipWeaponsData;
        public ShipMovementData ShipMovementData => shipMovementData;
        public SpeedData ShipSpeedData => shipSpeedData;
        public PlayerInputData PlayerInputData => playerInputData;
    }
}