using UnityEngine;

namespace Game.Movement
{
    [CreateAssetMenu(fileName = "AsteroidSpeedData", menuName = "Data/Asteroid/Speed Data")]
    public class AsteroidSpeedData : ScriptableObject
    {
        [SerializeField] public Vector2 minMaxSpeed;
    }
}