using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Drawing;

namespace XsmController
{
    public class ComController
    {
        SerialPort com;

        public ComController(string port,int rate = 9600)
        {
            com = new SerialPort(port, rate);
            com.Encoding = Encoding.GetEncoding(1251);
        }

        public void SendCommand(Commands command,int p,string v)
        {
            string s = EnumToString(command) + p.ToString("D2") + v;
            if (!com.IsOpen)
                com.Open();
            com.WriteLine(s);
        }

        public void SendCommand(Commands command, int p, int v)
        {
            SendCommand(command, p, v.ToString());
        }

        public void SendCommand(Commands command, int p, Color v)
        {
            int d = v.R * (256 * 256) + v.G * 256 + v.B;
            SendCommand(command, p, d);
        }

        private static string EnumToString(Commands c)
        {
            string s;
            switch(c)
            {
                case Commands.Led_string:
                    s = "STR";
                    break;
                case Commands.led_color:
                    s = "RGB";
                    break;
                default:
                    s = c.ToString();
                    break;
            }

            return s;
        }

    }
}
