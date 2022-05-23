using UnityEngine;

namespace Game.Ship
{
    public class ShipData : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private string laserAnimationKey;
        [SerializeField] private Transform bulletShootPoint;
        [SerializeField] private Transform laserShootPoint;
        [SerializeField] private ParticleSystem engineVFX;
        [SerializeField] private ParticleSystem bulletShootVFX;

        public Transform BulletShootPoint => bulletShootPoint;
        public Transform LaserShootPoint => laserShootPoint;
        public ParticleSystem EngineVFX => engineVFX;
        public ParticleSystem BulletShootVFX => bulletShootVFX;
        public Animator Animator => animator;
        public string LaserAnimationKey => laserAnimationKey;
    }
}