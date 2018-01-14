using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Business
{
    public class CoinNewsService: ICoinNewsService
    {
        private const string url = "http://larkbot.evesgf.com";

        public string[] RequestBiQuanApi()
        {
            string[] reStr;

            try
            {
                var jinseLatestNewsFlash = JsonHelper.DeserializeJsonToObject<ResultModel<NewsModel>>(HttpUitls.Get(url + "/api/News/GetJinseLatestNewsFlash"));
                var bishijieLatestNewsFlash = JsonHelper.DeserializeJsonToObject<ResultModel<NewsModel>>(HttpUitls.Get(url + "/api/News/GetBishijieLatestNewsFlash"));
                var bitcoinLatestNewsFlash = JsonHelper.DeserializeJsonToObject<ResultModel<NewsModel>>(HttpUitls.Get(url + "/api/News/GetBitcoinLatestNewsFlash"));

                reStr = new string[] { jinseLatestNewsFlash.Data.Content, bishijieLatestNewsFlash.Data.Content, bitcoinLatestNewsFlash.Data.Content };

            }
            catch (Exception e)
            {
                reStr = new string[] { e.ToString() + "\n席马达！程序BUG了，快召唤老铁来维修!" };
            }

            return reStr;
        }

        public class NewsModel
        {
            /// <summary>
            /// Id
            /// </summary>
            public int Id { get; set; }
            /// <summary>
            /// 标题
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// 重要性等级
            /// </summary>
            public int ImportantLevel { get; set; }
            /// <summary>
            /// 来源
            /// </summary>
            public string From { get; set; }
            /// <summary>
            /// 来源地址
            /// </summary>
            public string FromUrl { get; set; }
            /// <summary>
            /// 来源方推送时间
            /// </summary>
            public string PushTime { get; set; }
            /// <summary>
            /// 内容
            /// </summary>
            public string Content { get; set; }
            /// <summary>
            /// 标签
            /// </summary>
            public string Tag { get; set; }
            /// <summary>
            /// 推送等级
            /// </summary>
            public int PushLevel { get; set; }
            /// <summary>
            /// 添加时间
            /// </summary>
            public string AddTime { get; set; }
        }

        public class ResultModel<T>
        {
            /// <summary>
            /// 返回状态
            /// </summary>
            public bool Success { get; set; }

            /// <summary>
            /// 返回结果
            /// </summary>
            public T Data { get; set; }

            /// <summary>
            /// 说明
            /// </summary>
            public string Msg { get; set; }
        }
    }
}
