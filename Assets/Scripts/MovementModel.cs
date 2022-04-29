using UnityEngine;

namespace Game
{
    public class MovementModel : MonoBehaviour
    {
        [Range(0f, 1f)] [SerializeField] public float inertiaFadeForce;
        [SerializeField] public float turnSpeed;
    }
}