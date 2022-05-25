using UnityEngine;

namespace Game.Enemy.UFO
{
    [CreateAssetMenu(fileName = "UfoMovementData", menuName = "Data/Ufo/Movement")]
    public class UfoMovementData : ScriptableObject
    {
        [SerializeField] private float speed;

        public float Speed => speed;
    }
}