using Game.Enemy;
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
        [SerializeField] private VfxData vfxData;
        [SerializeField] private EnemyData enemyData;
        [SerializeField] private ShipFactoryData shipFactoryData;
        [SerializeField] private ShipWeaponsData shipWeaponsData;
        [SerializeField] private ShipMovementData shipMovementData;
        [SerializeField] private PlayerInputData playerInputData;
        [SerializeField] private ShipSpeedData shipShipSpeedData;
        [SerializeField] private ScoreData scoreData;
        [SerializeField] private bool spawnEnemies;

        public EnemyData EnemyData => enemyData;
        public VfxData VFXData => vfxData;
        public ScoreData ScoreData => scoreData;
        public ShipFactoryData ShipFactoryData => shipFactoryData;
        public ShipWeaponsData ShipWeaponsData => shipWeaponsData;
        public ShipMovementData ShipMovementData => shipMovementData;
        public ShipSpeedData ShipShipSpeedData => shipShipSpeedData;
        public PlayerInputData PlayerInputData => playerInputData;

        public bool SpawnEnemies => spawnEnemies;
    }
}