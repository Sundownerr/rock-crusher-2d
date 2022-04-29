using UnityEngine;

namespace Game
{
    public class SpeedModel : MonoBehaviour
    {
        [SerializeField]  public float acceleration;
        [SerializeField]  public float deceleration;
        [SerializeField]  public float maxSpeed;
    }
}