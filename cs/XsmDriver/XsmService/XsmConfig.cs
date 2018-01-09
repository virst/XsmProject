using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace XsmService
{
    [DataContract]
    class XsmConfig
    {
        [DataMember] public string port;
        [DataMember] public int rows;
        [DataMember] public int cols;
        [DataMember] public int leds;
        [DataMember] public List<string[]> diplays;
        [DataMember] public int Tick = 1500;
    }
}
