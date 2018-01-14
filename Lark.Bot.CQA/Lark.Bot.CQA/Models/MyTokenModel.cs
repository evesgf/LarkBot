using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Models
{
    public class MTSelectBit
    {
        public int code { get; set; }
        public string message { get; set; }
        public MTBitList data { get; set; }
    }

    public class MTBitList
    {
        public MTBit[] list { get; set; }
    }

    public class MTBit
    {
        public int id { get; set; }
        public int currency_id { get; set; }
        public string name { get; set; }
        public string symbol { get; set; }
        public string alias { get; set; }
        public int rank { get; set; }
        public string alphabet { get; set; }
        public string mytoken_id { get; set; }
        public string cmc_id { get; set; }
        public string cmc_url { get; set; }
        public string logo { get; set; }
        public int market_id { get; set; }
        public string market_name { get; set; }
        public string market_alias { get; set; }
        public string pair { get; set; }
        public string com_id { get; set; }
        public string currency { get; set; }
        public string anchor { get; set; }
        public decimal price { get; set; }
        public decimal price_usd { get; set; }
        public decimal volume_24h { get; set; }
        public decimal volume_24h_cny { get; set; }
        public decimal volume_24h_usd { get; set; }
        public string volume_24h_from { get; set; }
        public string volume_24h_to { get; set; }
        public string percent_change_today { get; set; }
        public string percent_change_utc0 { get; set; }
        public string percent_change_1h { get; set; }
        public string percent_change_24h { get; set; }
        public string percent_change_7d { get; set; }
        public string tv_symbol { get; set; }
        public string last_change { get; set; }
        public string review_status { get; set; }
        public string price_updated_at { get; set; }
        public decimal market_cap_usd { get; set; }
        public string cc_kline { get; set; }
        public string kline_enabled { get; set; }
        public string search_field { get; set; }
        public string kline_source { get; set; }
        public string price_display { get; set; }
        public string price_display_cny { get; set; }
        public string percent_change_display { get; set; }
        public string percent_change_range { get; set; }
        public bool is_favorite { get; set; }
    }
}
