using UnityEngine;

namespace Game
{
    [CreateAssetMenu(fileName = "ScoreData", menuName = "Data/Score")]
    public class ScoreData : ScriptableObject
    {
        [SerializeField] private int smallAsteroid;
        [SerializeField] private int mediumAsteroid;
        [SerializeField] private int bigAsteroid;
        [SerializeField] private int ufo;

        public int SmallAsteroid => smallAsteroid;
        public int MediumAsteroid => mediumAsteroid;
        public int BigAsteroid => bigAsteroid;
        public int Ufo => ufo;

        public int CurrentScore { get; set; }
    }
}