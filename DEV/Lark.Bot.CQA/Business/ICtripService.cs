using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Business
{
    public interface ICtripService
    {

        /// <summary>
        /// 获取携程当前机票的价格
        /// </summary>
        /// <param name="start">出发</param>
        /// <param name="end">降落</param>
        /// <param name="time">日期</param>
        /// <returns></returns>
        decimal GetCtripAirPrice(string start,string end, string time);
    }
}
