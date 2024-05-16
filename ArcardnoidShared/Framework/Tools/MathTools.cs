using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArcardnoidShared.Framework.Tools
{
    public static class MathTools
    {
        public static float Lerp(float a, float b, float t)
        {
            return a + (b - a) * t;
        }
    }
}
