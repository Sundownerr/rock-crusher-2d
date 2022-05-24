using System;
using UnityEngine;

namespace Game.Base
{
    public class ColliderData : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Enter?.Invoke(other);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            Exit?.Invoke(other);
        }

        public event Action<Collision2D> Enter;
        public event Action<Collision2D> Exit;
    }
}