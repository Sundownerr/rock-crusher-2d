using UnityEngine;

namespace Game.Gameplay.Utility
{
    public class ParentData : MonoBehaviour
    {
        [SerializeField] private Transform bulletParent;
        [SerializeField] private Transform asteroidParent;
        [SerializeField] private Transform ufoParent;

        public Transform BulletParent => bulletParent;
        public Transform AsteroidParent => asteroidParent;
        public Transform UfoParent => ufoParent;
    }
}