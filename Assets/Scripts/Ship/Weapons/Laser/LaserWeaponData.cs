using UnityEngine;

namespace Game.Ship.Weapons.Laser
{
    [CreateAssetMenu(fileName = "LaserWeaponData", menuName = "Data/Weapon/Laser")]
    public class LaserWeaponData : ScriptableObject
    {
        [SerializeField] private int maxCharges;
        [SerializeField] private float cooldownTime;
        [SerializeField] private float sizeX;
        [SerializeField] private float sizeY;
        [SerializeField] private float delay;
        [SerializeField] private LayerMask targetLayer;
        public int MaxCharges => maxCharges;
        public float CooldownTime => cooldownTime;
        public float SizeX => sizeX;
        public float SizeY => sizeY;
        public float Delay => delay;

        public int CurrentCharges { get; set; }
        public float CurrentCooldown { get; set; }

        public LayerMask TargetLayer => targetLayer;
    }
}