using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XsmService.Utils
{
    class IntNormalize : IFormattable
    {
        public static IntNormalize DefaultNormalize = new IntNormalize();

        private int n = 0;

        public IntNormalize SetInt(int v)
        {
            n = v;
            return this;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            int k = Convert.ToInt32(format);
            return n.ToString().PadLeft(k).Substring(0, k);
        }
    }
}
