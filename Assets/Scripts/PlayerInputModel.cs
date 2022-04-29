using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    public class PlayerInputModel : MonoBehaviour
    {
        [SerializeField] public InputActionReference fire;
        [SerializeField] public InputActionReference move;
    }
}