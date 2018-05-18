using Lark.Bot.CQA.Uitls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services
{
    public class HuobiService : IHuobiService
    {
        public const string api = "https://api.huobi.pro/market";

        /// <summary>
        /// coinId 1|btc,2|usdt
        /// 请求地址：https://otc-api.huobipro.com/v1/otc/trade/list/public?country=0&currency=1&payMethod=0&currPage=1&coinId=1&tradeType=0&merchant=1&online=1
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public async Task<string> LegalTender()
        {
            var url1 = "https://otc-api.huobipro.com/v1/otc/trade/list/public?country=0&currency=1&payMethod=0&currPage=1&coinId=" + 1 + "&tradeType=0&merchant=1&online=1";
            HttpResult httpResult1 = await HttpUitls.HttpsGetRequestAsync(url1);

            string otc_btc = null;
            if (httpResult1.Success)
            {
                var reModel1 = JsonConvert.DeserializeObject<HuobiOTCResult>(httpResult1.StrResponse);

                if (reModel1 == null) return "数据为空？这个币是瓦特了吧";

                otc_btc = reModel1.data[0].price;
            }
            else
            {
                otc_btc = httpResult1.StrResponse.Length < 48 ? httpResult1.StrResponse : httpResult1.StrResponse.Substring(0, 48);
                otc_btc += "oh~锅咩锅咩~程序跪了~";
            }


            var url2 = "https://otc-api.huobipro.com/v1/otc/trade/list/public?country=0&currency=1&payMethod=0&currPage=1&coinId=" + 2 + "&tradeType=0&merchant=1&online=1";
            HttpResult httpResult2 = await HttpUitls.HttpsGetRequestAsync(url2);

            string otc_usdt = null;
            if (httpResult2.Success)
            {
                var reModel2 = JsonConvert.DeserializeObject<HuobiOTCResult>(httpResult2.StrResponse);

                if (reModel2 == null) return "数据为空？这个币是瓦特了吧";

                otc_usdt = reModel2.data[0].price;
            }
            else
            {
                otc_usdt = httpResult2.StrResponse.Length < 48 ? httpResult2.StrResponse : httpResult2.StrResponse.Substring(0, 48);
                otc_usdt += "oh~锅咩锅咩~程序跪了~";
            }

            return "【火币场外】btc:" + otc_btc + "￥/usdt:"+ otc_usdt+"￥";
        }

        #region Huobi OTC Pojo

        public class HuobiOTCResult
        {
            public string code { get; set; }
            public string message { get; set; }
            public string totalCount { get; set; }
            public string pageSize { get; set; }
            public string totalPage { get; set; }
            public string currPage { get; set; }
            public HuobiOTCReModel[] data { get; set; }
            public bool success { get; set; }
        }

        public class HuobiOTCReModel
        {
            public string id { get; set; }
            public string tradeNo { get; set; }
            public string country { get; set; }
            public string coinId { get; set; }
            public string tradeType { get; set; }
            public string merchant { get; set; }
            public string merchantLevel { get; set; }
            public string currency { get; set; }
            public string payMethod { get; set; }
            public string userId { get; set; }
            public string userName { get; set; }
            public string isFixed { get; set; }
            public string minTradeLimit { get; set; }
            public string maxTradeLimit { get; set; }
            public string fixedPrice { get; set; }
            public string calcRate { get; set; }
            public string price { get; set; }
            public string gmtSort { get; set; }
            public string tradeCount { get; set; }
            public string isOnline { get; set; }
            public string tradeMonthTimes { get; set; }
            public string appealMonthTimes { get; set; }
            public string appealMonthWinTimes { get; set; }
            public string takerRealLevel { get; set; }
            public string takerIsPhoneBind { get; set; }
            public string takerTradeTimes { get; set; }
            public string takerLimit { get; set; }
            public string orderCompleteRate { get; set; }
        }

        #endregion

        public async Task<string> Ticker(string key)
        {
            //火币形式为btcusdt
            var newKey = key.Split(' ');

            if (newKey.Length != 2) return "key 错误，形式为btc usdt";

            var symbol = newKey[0] + newKey[1];
            var url = "https://api.huobi.pro/market/trade?symbol=" + symbol;
            HttpResult httpResult = await HttpUitls.HttpsGetRequestAsync(url);

            if (httpResult.Success)
            {
                var reModel = JsonConvert.DeserializeObject<HuobiResult>(httpResult.StrResponse);

                if (reModel == null) return "数据为空？这个币是瓦特了吧";

                var reStr = "【" + key + "】价格:" + reModel.tick.data[0].price + " 方向:" + reModel.tick.data[0].direction + " 成交量:" + reModel.tick.data[0].amount;
                return reStr;
            }
            else
            {
                var re = httpResult.StrResponse.Length < 48 ? httpResult.StrResponse : httpResult.StrResponse.Substring(0, 48);
                return re + "oh~锅咩锅咩~程序跪了~";
            }
        }

        #region Huobi Pojo
        public class HuobiResult
        {
            public string status { get; set; }
            public string ch { get; set; }
            public long ts { get; set; }
            public Tick tick { get; set; }
        }

        public class Tick
        {
            /// <summary>
            /// 消息id
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 最新成交时间
            /// </summary>
            public long ts { get; set; }
            public HuobiTickReModel[] data { get; set; }
        }

        public class HuobiTickReModel
        {
            /// <summary>
            /// 成交量
            /// </summary>
            public float amount { get; set; }
            /// <summary>
            /// 成交时间
            /// </summary>
            public long ts { get; set; }
            /// <summary>
            /// 成交id
            /// </summary>
            public string id { get; set; }
            /// <summary>
            /// 成交价钱,
            /// </summary>
            public decimal price { get; set; }
            /// <summary>
            /// 主动成交方向
            /// </summary>
            public string direction { get; set; }
        }
        #endregion
    }
}
