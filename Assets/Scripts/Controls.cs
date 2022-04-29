using System;

namespace Game
{
    public class Controls : IUpdate
    {
        public enum TurnDirection
        {
            None,
            Left,
            Right
        }

        public void Update()
        {
            throw new NotImplementedException();
        }

        public event Action TurnReleased;
        public event Action<TurnDirection> TurnPressed;
        public event Action MovePressed;
        public event Action MoveReleased;
        public event Action ShootPressed;
        public event Action ShootReleased;
    }
}