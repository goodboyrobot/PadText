using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadText.Library
{
    class RandomNumber
    {
        private uint a;
        private uint b;

        public RandomNumber(uint seed)
        {
            a = b = seed;
        }

        public RandomNumber(uint seed1, uint seed2)
        {
            a = seed1;
            b = seed2;
        }

        public uint Next(uint max)
        {
            return Next() % max;
        }

        public uint Next()
        {
            b = 36969 * (b & 65535) + (b >> 16);
            a = 18000 * (a & 65535) + (b >> 16);
            return ((b << 16) + a);
        }
    }
}
