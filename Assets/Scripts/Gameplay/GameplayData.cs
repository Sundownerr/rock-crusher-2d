using System;
using Game.Enemy;
using Game.Input;
using Game.Score;
using Game.Ship.Factory;
using Game.Vfx;
using UnityEngine;

namespace Game
{
    [CreateAssetMenu(menuName = "Gameplay Data", fileName = "Data/Gameplay Data", order = 0)]
    [Serializable]
    public class GameplayData : ScriptableObject
    {
        [SerializeField] private bool spawnEnemies;
        [Space] [SerializeField] private VfxData vfxData;
        [Space] [SerializeField] private EnemyData enemyData;
        [Space] [SerializeField] private ShipFactoryData shipFactoryData;
        [Space] [SerializeField] private PlayerInputData playerInputData;
        [Space] [SerializeField] private ScoreData scoreData;

        public EnemyData EnemyData => enemyData;
        public VfxData VFXData => vfxData;
        public ScoreData ScoreData => scoreData;
        public ShipFactoryData ShipFactoryData => shipFactoryData;
        public PlayerInputData PlayerInputData => playerInputData;

        public bool SpawnEnemies => spawnEnemies;
    }
}