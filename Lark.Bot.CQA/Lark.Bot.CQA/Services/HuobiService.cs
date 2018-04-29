using Lark.Bot.CQA.Commons;
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

        public string LegalTender(string key)
        {
            return null;
        }

        public string Ticker(string key)
        {
            //火币形式为btcusdt
            var newKey = key.Split(' ');

            if (newKey.Length != 2) return "key 错误，形式为btc usdt";

            var symbol = newKey[0] + newKey[1];
            var url = "https://api.huobi.pro/market/trade?symbol=" + symbol;
            var pageSorce = HttpUitls.HttpsGet(url);
            var reModel = JsonConvert.DeserializeObject<HuobiResult>(pageSorce);

            if (reModel == null) return "数据为空？这个币是瓦特了吧";

            var reStr = "【" + key + "】价格:" + reModel.tick.data[0].price + " 方向:" + reModel.tick.data[0].direction + " 成交量:" + reModel.tick.data[0].amount;
            return reStr;
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
            public Datum[] data { get; set; }
        }

        public class Datum
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
