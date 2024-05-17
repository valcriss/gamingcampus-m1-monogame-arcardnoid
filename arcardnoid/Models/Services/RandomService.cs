using arcardnoid.Models.Implementations;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;

namespace arcardnoid.Models.Services
{
    public class RandomService : IRandomService
    {
        #region Public Methods

        public IRandom GetRandom()
        {
            return new MonoGameRandom();
        }

        public IRandom GetRandom(int seed)
        {
            return new MonoGameRandom(seed);
        }

        #endregion Public Methods
    }
}