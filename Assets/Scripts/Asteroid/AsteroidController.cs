namespace Game
{
    public class AsteroidController
    {
        public AsteroidController(IDamagable asteroidDamagable)
        {
            asteroidDamagable.Damaged += OnDamaged;
        }

        private void OnDamaged()
        {
        }

        public static class Size
        {
            public const float Small = 0.25f;
            public const float Medium = 0.5f;
            public const float Big = 1f;
        }
    }
}