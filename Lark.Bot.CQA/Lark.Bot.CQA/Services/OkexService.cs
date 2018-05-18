using Lark.Bot.CQA.Uitls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services
{
    public class OkexService : IOkexService
    {
        public const string api = "https://www.okex.com/api/v1/ticker.do";

        /// <summary>
        /// 获取法币行情
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string LegalTender(string key)
        {
            return null;
        }

        #region 获取币币行情
        /// <summary>
        /// 获取OKEx币币行情
        /// </summary>
        /// <param name="key">ltc_btc</param>
        /// <returns></returns>
        public async Task<string> Ticker(string key)
        {
            //okex形式为btc_usdt
            var newKey = key.Split(' ');

            if (newKey.Length != 2) return "key 错误，形式为btc usdt";

            var symbol = newKey[0] + "_" + newKey[1];

            string url = api+"?symbol=" + symbol;

            HttpResult httpResult =await HttpUitls.HttpsGetRequestAsync(url);

            if (httpResult.Success)
            {
                var market = JsonConvert.DeserializeObject<Market>(httpResult.StrResponse);

                var re = "哎哟？这是什么稀奇玩意？老铁们快来看看能炒一波不";

                if (market.date != null && market.ticker != null)
                {
                    re = "【" + key + "】买一:" + market.ticker.buy + " 最高:" + market.ticker.high + " 最新成交:" + market.ticker.last + " 最低:" + market.ticker.low + " 卖一:" + market.ticker.sell + " 24h成交:" + market.ticker.vol;
                }
                return re;
            }
            else
            {
                var re = httpResult.StrResponse.Length < 48 ? httpResult.StrResponse : httpResult.StrResponse.Substring(0, 48);
                return re + "oh~锅咩锅咩~程序跪了~";
            }
        }


        #region Okex Pojo
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
        #endregion

    }
}
