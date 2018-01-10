using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XsmController
{
    public delegate void ControllerReceiveHandler(object o, CommandInfo c);

    public class CommandInfo
    {
        public Commands command;
        public int p;
        public string v;

        public override string ToString()
        {
            return string.Format("{0}|{1}-{2}", command, p, v);
        }
    }
}
