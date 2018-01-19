using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

namespace XsmTiny
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            #region MyRegion

            AppParams.Add("port", SerialPort.GetPortNames().Last());
            AppParams.Add("interval","300");

            foreach (string s in args)
            {
                var ss = s.Split('=');
                if(ss.Length==2)
                    if (AppParams.ContainsKey(ss[0]))
                        AppParams[ss[0]] = ss[1];
            }

            #endregion

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        

         public static Dictionary<string,string> AppParams = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
    }
}
