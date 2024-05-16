using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidShared.Framework.ServiceProvider.Interfaces
{
    public interface IRandom
    {
        int Next(int minValue, int maxValue);
        int Next(int maxValue);
        int Next();
    }
}
