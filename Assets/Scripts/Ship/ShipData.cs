using UnityEngine;

namespace Game.PlayerShip
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
        public float X { get; set; }
        public float Y { get; set; }
        public float Angle { get; set; }
        public float Speed { get; set; }
        public int LaserCharges { get; set; }
        public float LaserCooldown { get; set; }
        public ParticleSystem EngineVFX => engineVFX;
        public ParticleSystem BulletShootVFX => bulletShootVFX;

        public Animator Animator => animator;
        public string LaserAnimationKey => laserAnimationKey;
    }
}