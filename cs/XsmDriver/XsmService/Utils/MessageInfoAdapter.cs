using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ViSysMon.SysMonInfo;

namespace XsmService.Utils
{
    class MessageInfoAdapter : IFormattable
    {
        public MessageInfo[] mails = null;
        int k;

        public int MailsCount
        {
            get
            {
                if (mails == null)
                    return -1;
                return mails.Length;
            }
        }

        public int CurrenIndex
        {
            set { k = value; }
            get
            {
                if (MailsCount < 1)
                    k = -1;
                else
                    k = Incr(k, 0, MailsCount);
                return k;
            }
        }

        static public int Incr(int n, int i, int m)
        {
            return (n + i + m) % m;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (CurrenIndex == -1)
                return "------";
            if (format == "SenderName")
                return mails[CurrenIndex].SenderName;
            if (format == "Subject")
                return mails[CurrenIndex].Subject;
            return "######";
        }
    }
}
