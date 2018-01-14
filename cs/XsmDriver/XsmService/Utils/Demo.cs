using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ViSysMon.SysMonInfo;

namespace XsmService.Utils
{
    class Demo
    {
        public static MessageInfo[] GetDemoMails()
        {
            List<MessageInfo> msm = new List<MessageInfo>();
            msm.Add(new MessageInfo() { SenderName = "admin@locaohost", Subject = "Importent mail" });
            msm.Add(new MessageInfo() { SenderName = "gvsf@eweqw.RU", Subject = "" });
            msm.Add(new MessageInfo() { SenderName = "jsadj@loc.COM;vczv@loc.COM;jsauiuydj@loc;bm,j@loc;", Subject = "Живо ко мне !!!" });
            msm.Add(new MessageInfo() { SenderName = "Админ вася", Subject = "Очень важное письмо" });
            msm.Add(new MessageInfo() { SenderName = "", Subject = "RE:Importent mail" });
            return msm.ToArray();
        }
    }
}
