using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "MovementModel", menuName = "Models/MovementModel")]
    public class MovementModel : ScriptableObject
    {
        [Range(0f, 1f)] [SerializeField] public float inertiaFadeForce;
        [SerializeField] public float turnSpeed;
    }
}