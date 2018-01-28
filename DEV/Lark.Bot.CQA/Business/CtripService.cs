using AngleSharp.Parser.Html;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Business
{
    public class CtripService : ICtripService
    {
        /// <summary>
        /// 获取携程当前机票的价格
        /// </summary>
        /// <param name="start">出发</param>
        /// <param name="end">降落</param>
        /// <param name="time">日期</param>
        /// <returns></returns>
        public decimal GetCtripAirPrice(string start, string end,string time)
        {
            var rePageStr = string.Empty;

            var url = "http://flights.ctrip.com/domesticsearch/search/SearchFirstRouteFlights?DCity1="+start+"&ACity1="+end+"&SearchType=S&DDate1="+time+ "&IsNearAirportRecommond=0&LogToken=a7f25eca279a4e2babf2288a50d9f7d0&rk=1.7494950006292465130948&CK=7751F85A04464A712BEA1588E2F7D4D1&r=0.14128928566980926280410";
            rePageStr = HttpUitls.Get(url);

            Regex regex = new Regex(time);
            Match mc = regex.Match(rePageStr);
            int position = mc.Index;

            var price = rePageStr.Substring(rePageStr.IndexOf(time),5);

            return Convert.ToDecimal(price);
        }
    }
}
