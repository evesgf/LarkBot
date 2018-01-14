using Business.CrawlNewsService.CoinNewsService;
using DTO;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace API.Controllers
{
    [EnableCors("any")] //设置跨域处理的代理
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    public class NewsController : Controller
    {
        private readonly IJinseService _jinseService;
        private readonly IBshijieService _bshijieService;
        private readonly IBitcoinService _bitcoinService;

        public NewsController(IJinseService jinseService,
            IBshijieService bshijieService,
            IBitcoinService bitcoinService)
        {
            _jinseService = jinseService;
            _bshijieService = bshijieService;
            _bitcoinService = bitcoinService;
        }

        #region Common
        /// <summary>
        /// 更新所有新闻
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<NewsModel>> UpdateAllNews()
        {
            var reModel = new ResultModel<NewsModel>();

            var job1 = await _jinseService.UpdatePushNewsFlash();
            var job2 = await _bshijieService.UpdatePushNewsFlash();
            var job3 = await _bitcoinService.UpdatePushNewsFlash();

            return reModel;
        }
        #endregion

        #region 金色财经
        /// <summary>
        /// 获取金色财经最新一条快讯
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<NewsModel>> GetJinseLatestNewsFlash()
        {
            var reModel = new ResultModel<NewsModel>();

            var news = await _jinseService.GetLatestNewsFlash();

            if (news == null)
            {
                reModel.Success = false;
                reModel.Msg = "查询结果为空";

                return reModel;
            }

            var data = new NewsModel();

            data.Title = news.Title;
            data.Content = news.Content;
            data.PushTime = news.PushTime;
            data.Tag = news.Tag;
            data.From = news.From;
            data.ImportantLevel = news.ImportantLevel;
            data.PushLevel = news.PushLevel;
            data.AddTime = news.AddTime;

            reModel.Success = true;
            reModel.Data = data;

            return reModel;
        }

        /// <summary>
        /// 更新金色财经消息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<NewsModel>> UpdateJinseNews()
        {
            var reModel = new ResultModel<NewsModel>();

            var news = await _jinseService.UpdatePushNewsFlash();

            if (!news.Success)
            {
                reModel.Success = false;
                reModel.Msg = news.Msg;

                return reModel;
            }

            var data = new NewsModel();

            data.Title = news.Result.Title;
            data.Content = news.Result.Content;
            data.PushTime = news.Result.PushTime;
            data.Tag = news.Result.Tag;
            data.From = news.Result.From;
            data.ImportantLevel = news.Result.ImportantLevel;
            data.PushLevel = news.Result.PushLevel;
            data.AddTime = news.Result.AddTime;

            reModel.Success = true;
            reModel.Data = data;

            return reModel;
        }
        #endregion

        #region 币世界
        /// <summary>
        /// 获取币世界最新一条快讯
        /// </summary>
        /// <returns></returns>
        public async Task<ResultModel<NewsModel>> GetBishijieLatestNewsFlash()
        {
            var reModel = new ResultModel<NewsModel>();

            var news = await _bshijieService.GetLatestNewsFlash();

            if (news == null)
            {
                reModel.Success = false;
                reModel.Msg = "查询结果为空";

                return reModel;
            }

            var data = new NewsModel();
            data.Title = news.Title;
            data.Content = news.Content;
            data.PushTime = news.PushTime;
            data.Tag = news.Tag;
            data.From = news.From;
            data.ImportantLevel = news.ImportantLevel;
            data.PushLevel = news.PushLevel;
            data.AddTime = news.AddTime;

            reModel.Success = true;
            reModel.Data = data;

            return reModel;
        }

        /// <summary>
        /// 更新币世界消息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<NewsModel>> UpdateBishijieNews()
        {
            var reModel = new ResultModel<NewsModel>();

            var news = await _bshijieService.UpdatePushNewsFlash();

            if (!news.Success)
            {
                reModel.Success = false;
                reModel.Msg = news.Msg;

                return reModel;
            }

            var data = new NewsModel();

            data.Title = news.Result.Title;
            data.Content = news.Result.Content;
            data.PushTime = news.Result.PushTime;
            data.Tag = news.Result.Tag;
            data.From = news.Result.From;
            data.ImportantLevel = news.Result.ImportantLevel;
            data.PushLevel = news.Result.PushLevel;
            data.AddTime = news.Result.AddTime;

            reModel.Success = true;
            reModel.Data = data;

            return reModel;
        }
        #endregion

        #region BitCoin
        /// <summary>
        /// 获取bitcoin最新一条快讯
        /// </summary>
        /// <returns></returns>
        public async Task<ResultModel<NewsModel>> GetBitcoinLatestNewsFlash()
        {
            var reModel = new ResultModel<NewsModel>();

            var news = await _bitcoinService.GetLatestNewsFlash();

            if (news == null)
            {
                reModel.Success = false;
                reModel.Msg = "查询结果为空";

                return reModel;
            }

            var data = new NewsModel();
            data.Title = news.Title;
            data.Content = news.Content;
            data.PushTime = news.PushTime;
            data.Tag = news.Tag;
            data.From = news.From;
            data.ImportantLevel = news.ImportantLevel;
            data.PushLevel = news.PushLevel;
            data.AddTime = news.AddTime;

            reModel.Success = true;
            reModel.Data = data;

            return reModel;
        }

        /// <summary>
        /// 更新bitcoin消息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ResultModel<NewsModel>> UpdateBitcoinNews()
        {
            var reModel = new ResultModel<NewsModel>();

            var news = await _bitcoinService.UpdatePushNewsFlash();

            if (!news.Success)
            {
                reModel.Success = false;
                reModel.Msg = news.Msg;

                return reModel;
            }

            var data = new NewsModel();

            data.Title = news.Result.Title;
            data.Content = news.Result.Content;
            data.PushTime = news.Result.PushTime;
            data.Tag = news.Result.Tag;
            data.From = news.Result.From;
            data.ImportantLevel = news.Result.ImportantLevel;
            data.PushLevel = news.Result.PushLevel;
            data.AddTime = news.Result.AddTime;

            reModel.Success = true;
            reModel.Data = data;

            return reModel;
        }
        #endregion
    }
}