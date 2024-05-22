using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using MonoGame.Extended;

namespace arcardnoid.Models.Implementations
{
    public class MonoGameRandom : IRandom
    {
        #region Private Fields

        private readonly FastRandom _random;

        #endregion Private Fields

        #region Public Constructors

        public MonoGameRandom(int seed)
        {
            _random = new FastRandom(seed);
        }

        public MonoGameRandom()
        {
            _random = new FastRandom();
        }

        #endregion Public Constructors

        #region Public Methods

        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }

        public int Next(int maxValue)
        {
            return _random.Next(maxValue);
        }

        public int Next()
        {
            return _random.Next();
        }

        #endregion Public Methods
    }
}