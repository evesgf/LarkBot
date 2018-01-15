﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Business
{
    public class CoinService: ICoinService
    {
        /// <summary>
        /// 场外币价
        /// </summary>
        /// <returns></returns>
        public OTCPrice OTCPrice()
        {
            try
            {
                var url = "http://larkbot.evesgf.com/api/Coin/OTCPrice";
                return JsonHelper.DeserializeJsonToObject<OTCPrice>(HttpUitls.Get(url));
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取okex币价
        /// </summary>
        /// <param name="key">btc_usdt</param>
        /// <returns></returns>
        public string GetOKEXCoinPrice(string key)
        {
            try
            {
                string url = "https://www.okex.com/api/v1/ticker.do?symbol=" + key;

                var market = JsonHelper.DeserializeJsonToObject<Market>(HttpUitls.Get(url));

                var re = "哎哟？这是什么稀奇玩意？老铁们快来看看能炒一波不";

                if (market.date != null && market.ticker != null)
                {
                    re = "【"+key + "】买一:" + market.ticker.buy + " 最高:" + market.ticker.high + " 最新成交:" + market.ticker.last + " 最低:" + market.ticker.low + " 卖一:" + market.ticker.sell + " 24h成交:" + market.ticker.vol;
                }

                return re;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        /// <summary>
        /// 获取MyToken币价
        /// </summary>
        /// <param name="key">btc</param>
        /// <returns></returns>
        public string GetMyTokenPrice(string key)
        {
            string re = "哎哟？这是什么稀奇玩意？老铁们快来看看能炒一波不";

            try
            {
                string typeBcURL = "http://api.lb.mytoken.org/currency/filterlist";

                string data = "keyword=" + key + "&market_id=1303&timestamp=1514259573450&code=7c976da9f6797a8a85a73bf30562c6f6&platform=m&";

                var r = HttpUitls.Post(typeBcURL, data, "http://app.mytoken.io/");
                var a = JsonHelper.DeserializeJsonToObject<MTSelectBit>(r);
                var x = a.data.list.FirstOrDefault();

                string listUrl = "http://api.lb.mytoken.org/ticker/currencyexchangelist?currency_id=" + x.currency_id + "&page=1&timestamp=1514266516737&code=d690c07d97539898d1387f7cf112d172&platform=m&";

                var rex = HttpUitls.Get(listUrl);
                var b = JsonHelper.DeserializeJsonToObject<MTSelectBit>(rex);
                if (b != null && b.data.list != null)
                {

                    MTBit bithumb = b.data.list.Where(asx => asx.market_name.Equals("Bithumb")).FirstOrDefault();
                    MTBit coincheck = b.data.list.Where(asx => asx.market_name.Equals("Coincheck")).FirstOrDefault();
                    MTBit bitfinex = b.data.list.Where(asx => asx.market_name.Equals("Bitfinex")).FirstOrDefault();
                    MTBit gdax = b.data.list.Where(asx => asx.market_name.Equals("GDAX")).FirstOrDefault();
                    MTBit okex = b.data.list.Where(asx => asx.market_name.Equals("OKEx")).FirstOrDefault();
                    MTBit bitstamp = b.data.list.Where(asx => asx.market_name.Equals("Bitstamp")).FirstOrDefault();

                    re = "【"+ key+"】";
                    re += "\nBithumb: " + FormartMTBit(bithumb) + " | Coincheck: " + FormartMTBit(coincheck) + " | Bitfinex: " + FormartMTBit(bitfinex);
                    re += "\nGDAX: " + FormartMTBit(gdax) + " | OKEx: " + FormartMTBit(okex) + " | Bitstamp: " + FormartMTBit(bitstamp);

                    return re;
                };

                return "没数据啊";
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public string FormartMTBit(MTBit bit)
        {
            if (bit == null)
            {
                return "咱不卖";
            }
            return bit.price_usd.ToString("f4") + "$ ";
        }
    }

    public class OTCPrice
    {
        public bool Access { get; set; }
        public string[] Data { get; set; }
        public object Msg { get; set; }
    }

    #region OKEXMdel
    /// <summary>
    /// OKEX币价
    /// </summary>
    public class Market
    {
        /// <summary>
        /// 返回数据时服务器时间
        /// </summary>
        public string date { get; set; }
        public Tacker ticker { get; set; }
    }

    public class Tacker
    {
        /// <summary>
        /// 买一价
        /// </summary>
        public decimal buy { get; set; }
        /// <summary>
        /// 最高价
        /// </summary>
        public decimal high { get; set; }
        /// <summary>
        ///  最新成交价
        /// </summary>
        public decimal last { get; set; }
        /// <summary>
        /// 最低价
        /// </summary>
        public decimal low { get; set; }
        /// <summary>
        /// 卖一价
        /// </summary>
        public decimal sell { get; set; }
        /// <summary>
        /// 成交量(最近的24小时)
        /// </summary>
        public decimal vol { get; set; }
    }
    #endregion

    #region MyTokenModel
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
    #endregion
}