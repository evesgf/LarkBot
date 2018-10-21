using Lark.Bot.CQA.Services.News;
using Lark.Bot.CQA.Uitls;
using Lark.Bot.CQA.Uitls.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services
{
    public class NewsService:INewsService
    {
        private readonly IBishijieService _bishijieService;
        private readonly IJinseService _jinseService;

        public NewsService(
            IBishijieService bishijieService, 
            IJinseService jinseService
            )
        {
            _bishijieService = bishijieService;
            _jinseService = jinseService;
        }

        public NewsResult[] RequestBiQuanApi()
        {
            NewsResult[] reStr;

            try
            {
                reStr = new NewsResult[4];

                NewsResult jinseLatestNewsFlash = _jinseService.GetLatestNewsFlash().Result;
                if (jinseLatestNewsFlash != null) reStr[0] = jinseLatestNewsFlash;

                NewsResult bishijieLatestNewsFlash = _bishijieService.GetLatestNewsFlash().Result;
                if (bishijieLatestNewsFlash != null) reStr[1] = bishijieLatestNewsFlash;

                CoinNewsModel bitcoinLatestNewsFlash  = JsonConvert.DeserializeObject<CoinNewsResultModel<CoinNewsModel>>(HttpUitls.Get(ConfigManager.pushNewsConfig.NewsServerURL + "/News/GetBitcoinLatestNewsFlash")).Data;
                if (bitcoinLatestNewsFlash!=null)
                {
                    var model=new NewsResult
                    {
                        Success = true,
                        From = "【bitcoin】",
                        Content = bitcoinLatestNewsFlash.Content,
                        NewsLevel = NewsLevel.Importent
                    };
                    reStr[2] = model;
                }

                CoinNewsModel OkexNotice = JsonConvert.DeserializeObject<CoinNewsResultModel<CoinNewsModel>>(HttpUitls.Get(ConfigManager.pushNewsConfig.NewsServerURL + "/News/GetOkexLatestNotice")).Data;
                if (OkexNotice != null)
                {
                    var model = new NewsResult
                    {
                        Success = true,
                        From = "【Okex】",
                        Content = OkexNotice.Title + " " + OkexNotice.FromUrl,
                        NewsLevel = NewsLevel.Importent
                    };
                    reStr[3] = model;
                }
            }

            catch (Exception e)
            {
                reStr = new NewsResult[] {
                    new NewsResult
                    {
                        Success=false,
                        Content=e.ToString() + "\n席马达！程序BUG了，快召唤老铁来维修!"
                    }
                };
            }

            return reStr;
        }
    }

    #region News Pojo
    public class CoinNewsModel
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

    public class CoinNewsResultModel<T>
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
    #endregion

}
