using Lark.Bot.CQA.Business;
using Newbe.Mahua;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Lark.Bot.CQA.Handler.TimeJobHandler
{
    public class TimeJobHandler : ITimeJobHandler
    {
        private readonly IMahuaApi _mahuaApi;
        private readonly ICoinNewsService _coinNewsService;
        private readonly ICoinService _coinService;

        public TimeJobHandler(
            IMahuaApi mahuaApi, ICoinNewsService coinNewsService, ICoinService coinService)
        {
            _mahuaApi = mahuaApi;
            _coinNewsService = coinNewsService;
            _coinService = coinService;
        }

        public string fromQQ { get; set; }
        Timer t = new Timer(1000 * 5 * 3);
        public bool StartPushNews(string groupQQ)
        {
            fromQQ = groupQQ;
            // todo 填充处理逻辑
            t.Elapsed += new ElapsedEventHandler(SendBiMessage);
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；

            return true;
        }

        private int sendCount = 0;
        private static string lastMsg1 = null;
        private static string lastMsg2 = null;
        private static string lastMsg3 = null;
        public void SendBiMessage(object source, System.Timers.ElapsedEventArgs e)
        {
            var re = _coinNewsService.RequestBiQuanApi();

            string msg1 = null;
            if (lastMsg1 != re[0])
            {
                lastMsg1 = re[0];
                msg1 = re[0] + "\n";
            }
            else
            {
                msg1 = null;
            }

            string msg2 = null;
            if (lastMsg2 != re[1])
            {
                lastMsg2 = re[1];
                msg2 = re[1] + "\n";
            }
            else
            {
                msg2 = null;
            }

            string msg3 = null;
            if (lastMsg3 != re[2])
            {
                lastMsg3 = re[2];
                msg3 = re[2] + "\n";
            }
            else
            {
                msg3 = null;
            }

            if (msg1 != null || msg2 != null || msg3 != null)
            {
                string reMsg = msg1 + msg2 + msg3;

                reMsg += "【场外币价】";
                var re2 = _coinService.OTCPrice().Data;
                foreach (var str in re2)
                {
                    reMsg += str + "\n";
                }

                //查询币圈
                reMsg += _coinService.GetOKEXCoinPrice("btc_usdt");

                _mahuaApi.SendGroupMessage(fromQQ, reMsg + "\n第" + sendCount + "次主动推送消息");
                sendCount++;
            }
        }
    }
}
