using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace R1StatsModel.Extensions
{
    public static class FloatExtension
    {
        public static float RoundDown(this float i, double decimalPlaces)
        {
            var power = (float)(Math.Pow(10, decimalPlaces));
            return MathF.Floor(i * power) / power;
        }
    }
}
