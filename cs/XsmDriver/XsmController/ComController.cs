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
        static Dictionary<Commands, string> commands = new Dictionary<Commands, string>();

        public event ControllerReceiveHandler CommandReceived;

        static ComController()
        {
            commands.Add(Commands.Led_string, "STR");
            commands.Add(Commands.led_color, "RGB");
        }

        public ComController(string port, int rate = 9600)
        {
            com = new SerialPort(port, rate);
            com.Encoding = Encoding.GetEncoding(1251);
            com.DataReceived += Com_DataReceived;
        }

        private void Com_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            var s = com.ReadLine();
            CommandInfo c = new CommandInfo();
            c.command = StringToEnum(s.Substring(0, 3));
            c.p = Convert.ToInt32(s.Substring(3, 2));
            c.v = s.Substring(5);
        }

        public void SendCommand(Commands command, int p, string v)
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
            return commands[c];
        }

        private static Commands StringToEnum(string s)
        {
            foreach (var o in commands)
                if (o.Value == s)
                    return o.Key;
            throw new NotImplementedException();
        }

    }
}
