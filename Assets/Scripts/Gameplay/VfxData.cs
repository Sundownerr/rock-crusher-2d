using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "VfxData", menuName = "Data/Vfx Data")]
    public class VfxData : ScriptableObject
    {
        [SerializeField] private GameObject bigAsteroidDeathVfx;
        [SerializeField] private GameObject mediumAsteroidDeathVfx;
        [SerializeField] private GameObject smallAsteroidDeathVfx;
        [SerializeField] private GameObject ufoDeathVfx;
        [SerializeField] private GameObject shipDeathVfx;

        public GameObject BigAsteroidDeathVfx => bigAsteroidDeathVfx;
        public GameObject MediumAsteroidDeathVfx => mediumAsteroidDeathVfx;
        public GameObject SmallAsteroidDeathVfx => smallAsteroidDeathVfx;
        public GameObject UfoDeathVfx => ufoDeathVfx;
        public GameObject ShipDeathVfx => shipDeathVfx;
    }
}