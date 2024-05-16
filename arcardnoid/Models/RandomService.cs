using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models
{
    public class RandomService : IRandomService
    {
        public IRandom GetRandom()
        {
            return new MonoGameRandom();
        }

        public IRandom GetRandom(int seed)
        {
            return new MonoGameRandom(seed);
        }
    }
}
