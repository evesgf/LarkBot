using Lark.Bot.CQA.Commons;
using Newbe.Mahua.MahuaEvents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services
{
    public class CoinmarketcapService : ICoinmarketcapService
    {
        public const string api = "https://api.coinmarketcap.com/v1/ticker/";

       

        /// <summary>
        /// 形式为：https://api.coinmarketcap.com/v1/ticker/bitcoin/
        /// </summary>
        /// <param name="key">bitcoin</param>
        /// <returns></returns>
        public string GetTicker(string key)
        {
            var reStr = HttpUitls.Get(api + key + "/");

            if (reStr.Substring(7, 12).Equals("error"))
            {
                return JsonConvert.DeserializeObject<CoinmarketcapError>(reStr).error;
            }
            else
            {
                var model = JsonConvert.DeserializeObject<CoinmarketcapTicker[]>(reStr);

                var reMessage ="【"+ key+"】" + model[0].price_btc+"btc/"+ model[0].price_usd+"usdt";
                return reMessage;
            }
        }

        #region POJO
        public class CoinmarketcapTicker
        {
            public string id { get; set; }
            public string name { get; set; }
            public string symbol { get; set; }
            public string rank { get; set; }
            public string price_usd { get; set; }
            public string price_btc { get; set; }
            public string _24h_volume_usd { get; set; }
            public string market_cap_usd { get; set; }
            public string available_supply { get; set; }
            public string total_supply { get; set; }
            public string max_supply { get; set; }
            public string percent_change_1h { get; set; }
            public string percent_change_24h { get; set; }
            public string percent_change_7d { get; set; }
            public string last_updated { get; set; }
        }



        public class CoinmarketcapError
        {
            public string error { get; set; }
        }

        #endregion

    }
}
