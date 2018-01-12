using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Coin
{
    /// <summary>
    /// 火币法币页面返回
    /// </summary>
    public class LegalTenderPage
    {
        public int code { get; set; }
        public string message { get; set; }
        public int totalCount { get; set; }
        public int pageSize { get; set; }
        public int currPage { get; set; }
        public LegalTenderItem[] data { get; set; }
        public bool success { get; set; } 
    }

    /// <summary>
    /// 火币法币页面返回条目
    /// </summary>
    public class LegalTenderItem
    {
        public string id { get; set; }
        public string tradeNo { get; set; }
        public int country { get; set; }
        public int coinId { get; set; }
        public int tradeType { get; set; }
        public int merchant { get; set; }
        public int currency { get; set; }
        public string payMethod { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public string isFixed { get; set; }
        public decimal minTradeLimit { get; set; }
        public decimal maxTradeLimit { get; set; }
        public decimal fixedPrice { get; set; }
        public decimal calcRate { get; set; }
        public decimal price { get; set; }
        public decimal tradeCount { get; set; }
        public bool isOnline { get; set; }
        public int tradeMonthTimes { get; set; }
        public int appealMonthTimes { get; set; }
        public int appealMonthWinTimes { get; set; }
    }
}
