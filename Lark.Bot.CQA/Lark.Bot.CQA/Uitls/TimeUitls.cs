using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Uitls
{
    public static class TimeUitls
    {
        public static DateTime Unix2Datetime(long unixTimeStamp)
        {
            DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1)); // 当地时区

            return startTime.AddSeconds(unixTimeStamp);
        }
    }
}
