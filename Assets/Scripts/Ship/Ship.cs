using UnityEngine;

namespace Game.PlayerShip
{
    public class Ship : MonoBehaviour
    {
        [SerializeField] private Transform bulletShootPoint;
        [SerializeField] private Transform laserShootPoint;
        [SerializeField] private ParticleSystem engineVFX;
        [SerializeField] private ParticleSystem bulletShootVFX;

        public Transform BulletShootPoint => bulletShootPoint;
        public Transform LaserShootPoint => laserShootPoint;
        public float X => transform.position.x;
        public float Y => transform.position.y;
        public float Angle => transform.rotation.eulerAngles.z;
        public float Speed { get; set; }
        public int LaserCharges { get; set; }
        public float LaserCooldown { get; set; }

        public ParticleSystem EngineVFX => engineVFX;

        public ParticleSystem BulletShootVFX => bulletShootVFX;
    }
}