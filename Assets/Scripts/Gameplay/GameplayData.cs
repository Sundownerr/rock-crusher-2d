using Game.Enemies.Asteroid.Spawner;
using Game.Enemies.UFO.Spawner;
using Game.Input;
using Game.Ship.Movement;
using Game.Ship.Spawner;
using Game.Ship.Weapons;
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
        [SerializeField] private ShipSpeedData shipShipSpeedData;

        public AsteroidSpawnerData AsteroidSpawnerData => asteroidSpawnerData;
        public UfoSpawnerData UfoSpawnerData => ufoSpawnerData;
        public ShipSpawnerData ShipSpawnerData => shipSpawnerData;
        public ShipWeaponsData ShipWeaponsData => shipWeaponsData;
        public ShipMovementData ShipMovementData => shipMovementData;
        public ShipSpeedData ShipShipSpeedData => shipShipSpeedData;
        public PlayerInputData PlayerInputData => playerInputData;
    }
}