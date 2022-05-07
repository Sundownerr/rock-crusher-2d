using UnityEngine;
using UnityEngine.InputSystem;

namespace Game
{
    [CreateAssetMenu(fileName = "PlayerInputModel", menuName = "Models/PlayerInputModel")]
    public class PlayerInputModel : ScriptableObject
    {
        [SerializeField] public InputActionReference shootBullet;
        [SerializeField] public InputActionReference shootLaser;
        [SerializeField] public InputActionReference move;
    }
}