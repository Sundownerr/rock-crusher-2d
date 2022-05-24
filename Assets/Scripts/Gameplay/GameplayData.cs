using Game.Enemy.Asteroid.Factory;
using Game.Enemy.UFO.Factory;
using Game.Input;
using Game.Ship.Factory;
using Game.Ship.Movement;
using Game.Ship.Weapons;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "GameplayData", menuName = "Data/Gameplay")]
    public class GameplayData : ScriptableObject
    {
        [SerializeField] private AsteroidFactoryData asteroidFactoryData;
        [SerializeField] private UfoFactoryData ufoFactoryData;
        [SerializeField] private ShipFactoryData shipFactoryData;
        [SerializeField] private ShipWeaponsData shipWeaponsData;
        [SerializeField] private ShipMovementData shipMovementData;
        [SerializeField] private PlayerInputData playerInputData;
        [SerializeField] private ShipSpeedData shipShipSpeedData;
        [SerializeField] private ScoreData scoreData;

        public ScoreData ScoreData => scoreData;
        public AsteroidFactoryData AsteroidFactoryData => asteroidFactoryData;
        public UfoFactoryData UfoFactoryData => ufoFactoryData;
        public ShipFactoryData ShipFactoryData => shipFactoryData;
        public ShipWeaponsData ShipWeaponsData => shipWeaponsData;
        public ShipMovementData ShipMovementData => shipMovementData;
        public ShipSpeedData ShipShipSpeedData => shipShipSpeedData;
        public PlayerInputData PlayerInputData => playerInputData;
    }
}