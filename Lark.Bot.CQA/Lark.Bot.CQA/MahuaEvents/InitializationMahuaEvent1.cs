using Lark.Bot.CQA.Modules.TrackCoin;
using Newbe.Mahua;
using Newbe.Mahua.MahuaEvents;
using System;
using System.Collections.Generic;

namespace Lark.Bot.CQA.MahuaEvents
{
    /// <summary>
    /// 插件初始化事件
    /// </summary>
    public class InitializationMahuaEvent1
        : IInitializationMahuaEvent
    {
        private readonly IMahuaApi _mahuaApi;

        public InitializationMahuaEvent1(
            IMahuaApi mahuaApi)
        {
            _mahuaApi = mahuaApi;
        }

        System.Timers.Timer t = new System.Timers.Timer(1000 * 10 * 3);

        public void Initialized(InitializedContext context)
        {
            // todo 填充处理逻辑
            t.Elapsed += new System.Timers.ElapsedEventHandler(SendBiMessage);
            t.AutoReset = true;//设置是执行一次（false）还是一直执行(true)；
            t.Enabled = true;//是否执行System.Timers.Timer.Elapsed事件；

            //币价追踪
            Singleton<TrackManager>.Create();
            Singleton<TrackManager>.Instance.Init(Send);
        }

        public void Send(string from,string msg)
        {
            _mahuaApi.SendGroupMessage(from, msg);
        }


        #region 消息推送
        private int sendCount = 0;
        private static string lastMsg1 = null;
        private static string lastMsg2 = null;
        private static string lastMsg3 = null;
        public void SendBiMessage(object source, System.Timers.ElapsedEventArgs e)
        {

            //查询币圈
            var msgs = RequestHandler.RequestBiQuanApi();

            string msg1 = null;
            if (lastMsg1 != msgs[0])
            {
                lastMsg1 = msgs[0];
                msg1 = msgs[0] + "\n";
            }
            else
            {
                msg1 = null;
            }

            string msg2 = null;
            if (lastMsg2 != msgs[1])
            {
                lastMsg2 = msgs[1];
                msg2 = msgs[1] + "\n";
            }
            else
            {
                msg2 = null;
            }

            string msg3 = null;
            if (lastMsg3 != msgs[2])
            {
                lastMsg3 = msgs[2];
                msg3 = msgs[2] + "\n";
            }
            else
            {
                msg3 = null;
            }

            if (msg1 != null || msg2 != null || msg3 != null)
            {
                string reMsg = msg1 + msg2 + msg3 + RequestHandler.OTCPrice() + "\n" + RequestHandler.GetBitPrice2("btc");
                _mahuaApi.SendGroupMessage("693739965", reMsg + "\n第" + sendCount + "次主动推送消息");
                sendCount++;
            }
        }
        #endregion
    }
}
