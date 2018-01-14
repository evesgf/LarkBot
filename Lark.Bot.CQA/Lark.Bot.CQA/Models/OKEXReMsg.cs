using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Models
{
    public class OKEXReMsg
    {
        public int code { get; set; }
        public OKEXReMsgData data { get; set; }
        public string detailMsg { get; set; }
        public string msg { get; set; }
    }

    public class OKEXReMsgData
    {
        public OKEXTradingOrders[] buyTradingOrders { get; set; }
        public OKEXTradingOrders[] sellTradingOrders { get; set; }
    }

    public class OKEXTradingOrders
    {

    }
}
