using Lark.Bot.CQA.Uitls;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services.News
{
    public class JinseService : IJinseService
    {
        public async Task<string> GetLatestNewsFlash()
        {
            var reNews = string.Empty;

            var url = "https://api.jinse.com/v4/live/list?limit=1&reading=false";
            HttpResult httpResult = await HttpUitls.HttpsGetRequestAsync(url);

            if (httpResult.Success)
            {
                var reModel = JsonConvert.DeserializeObject<JinseFlash>(httpResult.StrResponse);

                if (reModel == null) return "好像什么都没有呢~";

                reNews = reModel.list.FirstOrDefault().lives.FirstOrDefault().content;
            }
            else
            {
                reNews = httpResult.StrResponse.Length < 48 ? httpResult.StrResponse : httpResult.StrResponse.Substring(0, 48);
                reNews = "oh~锅咩锅咩~程序跪了~";
            }

            return reNews;
        }
    }

    #region pojo
    /// <summary>
    /// 金色财经快讯
    /// </summary>
    public class JinseFlash
    {
        public int news { get; set; }
        public int count { get; set; }
        public int top_id { get; set; }
        public int bottom_id { get; set; }
        public JinseFlashList[] list { get; set; }
    }

    public class JinseFlashList
    {
        public string date { get; set; }
        public JsinseFlashLife[] lives { get; set; }
    }

    public class JsinseFlashLife
    {
        public int id { get; set; }
        public string content { get; set; }
        public string link_name { get; set; }
        public string link { get; set; }
        public int grade { get; set; }
        public string sort { get; set; }
        public string highlight_color { get; set; }
        public object[] images { get; set; }
        public int created_at { get; set; }
        public int up_counts { get; set; }
        public int down_counts { get; set; }
        public string zan_status { get; set; }
        public object[] readings { get; set; }
    }

    #endregion
}
