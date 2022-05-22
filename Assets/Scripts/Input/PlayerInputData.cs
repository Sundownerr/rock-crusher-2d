using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    [CreateAssetMenu(fileName = "PlayerInputData", menuName = "Data/Player Input")]
    public class PlayerInputData : ScriptableObject
    {
        [SerializeField] public InputActionReference shootBullet;
        [SerializeField] public InputActionReference shootLaser;
        [SerializeField] public InputActionReference move;
    }
}