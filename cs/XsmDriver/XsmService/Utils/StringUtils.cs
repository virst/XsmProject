using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XsmService.Utils
{
    internal static class StringUtils
    {
        public static string Normalize(this string s,int l)
        {
            if (s.Length < l)
                return s.PadRight(l, ' ');
            if (s.Length > l)
                return s.Substring(0, l);
            return s;
        }

        public static string Normalize(this int s, int num_l,int max_l)
        {
            return s.ToString("D" + num_l).PadLeft(max_l);
        }
    }

    
}
