using ArcardnoidShared.Framework.ServiceProvider.Interfaces;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace arcardnoid.Models
{
    public class MonoGameRandom : IRandom
    {
        private FastRandom _random;

        public MonoGameRandom(int seed)
        {
            _random = new FastRandom(seed);
        }

        public MonoGameRandom()
        {
            _random = new FastRandom();
        }
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
    }
}
