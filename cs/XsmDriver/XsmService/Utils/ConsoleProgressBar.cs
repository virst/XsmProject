using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XsmService.Utils
{
    public class ConsoleProgressBar : IFormattable
    {
        //public static ConsoleProgressBar DefaultBar = new ConsoleProgressBar();

        private float prc;
        private string z;

        public ConsoleProgressBar(char a = '#')
        {
            z = a.ToString();
        }

        public ConsoleProgressBar(int p,char a = '#')
        {
            z = a.ToString();
            SetPrcInt(p);
        }

        public ConsoleProgressBar SetPrcInt(int p) // проценты 0-100
        {
            prc = (float)p/100;
            return this;
        }

        public ConsoleProgressBar SetPrc(float p) // проценты 0-1
        {
            prc = p;
            return this;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            int v = Convert.ToInt32(format);
            int p = (int)(v * prc);
            string s = "[";
            for (int i = 0; i < v; i++)
                if (p > i)
                    s += z;
                else
                    s += " ";
            s += "]";
            return s;
        }


    }
}
