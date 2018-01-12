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

        public NewsController(IJinseService jinseService,
            IBshijieService bshijieService)
        {
            _jinseService = jinseService;
            _bshijieService = bshijieService;
        }

        /// <summary>
        /// 更新金色财经消息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<NewsModel> UpdateJinseNews()
        {
            var reModel = new NewsModel();

            var news = await _jinseService.UpdatePushNewsFlash();

            reModel.Title = news.Result.Title;
            reModel.Content = news.Result.Content;
            reModel.PushTime = news.Result.PushTime;
            reModel.Tag = news.Result.Tag;
            reModel.From = news.Result.From;
            reModel.ImportantLevel = news.Result.ImportantLevel;
            reModel.PushLevel = news.Result.PushLevel;
            reModel.AddTime = news.Result.AddTime;

            return reModel;
        }

        /// <summary>
        /// 更新币世界消息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<NewsModel> UpdateBishijieNews()
        {
            var reModel = new NewsModel();

            var news = await _bshijieService.UpdatePushNewsFlash();

            reModel.Title = news.Result.Title;
            reModel.Content = news.Result.Content;
            reModel.PushTime = news.Result.PushTime;
            reModel.Tag = news.Result.Tag;
            reModel.From = news.Result.From;
            reModel.ImportantLevel = news.Result.ImportantLevel;
            reModel.PushLevel = news.Result.PushLevel;
            reModel.AddTime = news.Result.AddTime;

            return reModel;
        }
    }
}