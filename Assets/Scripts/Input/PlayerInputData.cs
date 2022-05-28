using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Input
{
    [Serializable]
    public class PlayerInputData
    {
        [SerializeField] public InputActionReference shootBullet;
        [SerializeField] public InputActionReference shootLaser;
        [SerializeField] public InputActionReference move;
    }
}