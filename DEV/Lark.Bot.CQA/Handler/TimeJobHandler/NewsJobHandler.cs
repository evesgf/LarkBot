using Newbe.Mahua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using static Lark.Bot.CQA.Business.CoinNewsService;

namespace Lark.Bot.CQA.Handler.TimeJobHandler
{
    public class NewsJobHandler: INewsJobHandler
    {
        private readonly IMahuaApi _mahuaApi;

        public NewsJobHandler(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        #region 币圈消息推送
        public string[] fromQQList { get; set; }
        Timer t = new Timer(1000 * 60 * 10);
        public bool StartPushNews(string[] groupQQList)
        {
            fromQQList = groupQQList;
            // todo 填充处理逻辑
            t.Elapsed += new ElapsedEventHandler(SendBiMessage);
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；

            return true;
        }

        private int sendCount = 0;
        private static string lastMsg = null;
        public void SendBiMessage(object source, System.Timers.ElapsedEventArgs e)
        {
            var rePageStr = HttpUitls.Get("http://larkbot.evesgf.com/api/News/GetPaomianLatestNews");
            var re = JsonHelper.DeserializeJsonToObject<ResultModel<NewsModel>>(rePageStr).Data;

            string msg = null;
            if (lastMsg != re.Title)
            {
                lastMsg = re.Title;
                msg = re.Content;
            }
            else
            {
                msg = null;
            }

            if (msg != null)
            {
                foreach (var fromQQ in fromQQList)
                {
                    _mahuaApi.SendGroupMessage(fromQQ, msg + "\n第" + sendCount + "天主动推送新闻");
                }

                sendCount++;
            }
        }
        #endregion
    }
}
