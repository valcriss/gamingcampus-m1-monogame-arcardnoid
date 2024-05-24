using arcardnoid.Models.Implementations;
using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using System;

namespace arcardnoid.Models.Services
{
    public class RandomService : IRandomService
    {
        #region Public Methods

        public IRandom GetRandom()
        {
            return new MonoGameRandom(DateTime.Now.Year + DateTime.Now.Month + DateTime.Now.Day + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + DateTime.Now.Millisecond);
        }

        public IRandom GetRandom(int seed)
        {
            return new MonoGameRandom(seed);
        }

        #endregion Public Methods
    }
}