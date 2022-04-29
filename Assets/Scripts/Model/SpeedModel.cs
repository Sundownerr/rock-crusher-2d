using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "SpeedModel", menuName = "Models/SpeedModel")]
    public class SpeedModel : ScriptableObject
    {
        [SerializeField] public float acceleration;
        [SerializeField] public float deceleration;
        [SerializeField] public float maxSpeed;
    }
}