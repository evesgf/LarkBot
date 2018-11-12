using Lark.Bot.CQA.Uitls;
using Lark.Bot.CQA.Uitls.Config;
using Newbe.Mahua.MahuaEvents;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services
{
    public class CoinmarketcapService : ICoinmarketcapService
    {
        public const string api = "https://api.coinmarketcap.com/v1/ticker/";

        public CoinmarketcapTicker[] GetTickerList()
        {
            var reStr = HttpUitls.Get(api);
            var models = JsonConvert.DeserializeObject<CoinmarketcapTicker[]>(reStr);
            return models;
        }

        /// <summary>
        /// 形式为：https://api.coinmarketcap.com/v1/ticker/bitcoin/
        /// </summary>
        /// <param name="key">bitcoin</param>
        /// <returns></returns>
        public async Task<string> GetTicker(string key)
        {
            key = ConfigManager.CheckSymbol(key);

            HttpResult httpResult =await HttpUitls.HttpGetRequestAsync(api + key + "/");

            if (httpResult.Success)
            {
                if (httpResult.StrResponse.Substring(7, 12).Equals("error"))
                {
                    return "这啥玩意啊？我咋没见过咧？";
                }
                else
                {
                    var model = JsonConvert.DeserializeObject<CoinmarketcapTicker[]>(httpResult.StrResponse);

                    var reMessage = "【" + key + "】" + model[0].price_btc + "btc/" + model[0].price_usd + "usdt/"+ (Convert.ToDecimal(model[0].price_usd) * 6.9m).ToString("f4")+"￥";
                    reMessage += " " + model[0].percent_change_1h + "%/" + model[0].percent_change_24h + "%/" + model[0].percent_change_7d + "% " + TimeUitls.Unix2Datetime(Convert.ToInt64(model[0].last_updated)).ToShortTimeString();
                    return reMessage;
                }
            }
            else
            {
                var re = httpResult.StrResponse.Length < 48 ? httpResult.StrResponse : httpResult.StrResponse.Substring(0, 48);
                return re + "oh~锅咩锅咩~程序跪了~";
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
