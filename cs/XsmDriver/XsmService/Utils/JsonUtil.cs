using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace XsmService.Utils
{
    internal class JsonUtil<T>
    {
        static DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));

        public static string ObjToStr(T o)
        {
            MemoryStream stream1 = new MemoryStream();
            ser.WriteObject(stream1, o);
            stream1.Position = 0;
            StreamReader reader = new StreamReader(stream1);
            string text = reader.ReadToEnd();
            return text;
        }

        public static T ObjFromStr(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return (T)ser.ReadObject(stream);
        }
    }
}
