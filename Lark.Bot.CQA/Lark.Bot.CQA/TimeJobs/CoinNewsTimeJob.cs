using Lark.Bot.CQA.Uitls;
using Lark.Bot.CQA.Uitls.Config;
using Lark.Bot.CQA.Services;
using Newbe.Mahua;
using Newtonsoft.Json;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lark.Bot.CQA.Services.News;

namespace Lark.Bot.CQA.TimeJobs
{
    public class CoinNewsTimeJob : ICoinNewsTimeJob
    {
        private readonly IMahuaApi _mahuaApi;

        private readonly IScheduler _scheduler;

        private readonly INewsService _newsService;
        private readonly ICoinmarketcapService _coinmarketcapService;
        private readonly IHuobiService _huobiService;
        private readonly IPmtownService _pmtownService;

        public CoinNewsTimeJob(
            IMahuaApi mahuaApi,
            IScheduler scheduler,
            INewsService newsService,
            ICoinmarketcapService coinmarketcapService,
            IHuobiService huobiService,
            IPmtownService pmtownService
            )
        {
            _mahuaApi = mahuaApi;

            _scheduler = scheduler;

            _newsService = newsService;
            _coinmarketcapService = coinmarketcapService;
            _huobiService = huobiService;
            _pmtownService = pmtownService;
        }

        public void StartPushNews()
        {
            var scheduler = _scheduler;
            // and start it off
            scheduler.Start();

            // define the job and tie it to our HelloJob class
            IJobDetail job = JobBuilder.Create<GetNewsJob>()
                .WithIdentity("job1", "group1")
                .Build();

            // Trigger the job to run now, and then repeat every 10 seconds
            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(ConfigManager.pushNewsConfig.LoopCheckTime)
                    .RepeatForever())
                .Build();

            // Tell quartz to schedule the job using our trigger
            scheduler.ScheduleJob(job, trigger);
        }

        private int sendCount = 0;
        private int morningPaperSendCount = 0;
        private static string lastMsg1 = null;
        private static string lastMsg2 = null;
        private static string lastMsg3 = null;
        private static string lastMsg4 = null;
        private static string lastMorningPaper = null;

        private static List<string> normalMsgList = new List<string>();
        private int nowNormalNewsPushIntervalCount = 0;

        /// <summary>
        /// 检查是否有新的新闻
        /// http://larkbot.evesgf.com/api/News/GetPaomianLatestNews
        /// </summary>
        public void CheckNews()
        {
            CheckCoinNews();

            CheckMorningPaper();

        }

        private void CheckCoinNews()
        {
            var re = _newsService.RequestBiQuanApi();

            NewsResult msg1 = null;
            NewsResult msg2 = null;
            NewsResult msg3 = null;
            NewsResult msg4 = null;

            if (!re[0].Content.Equals(lastMsg1))
            {
                lastMsg1 = re[0].Content;
                msg1 = re[0];
            }
            else
            {
                msg1 = null;
            }

            if (!re[1].Content.Equals(lastMsg2))
            {
                lastMsg2 = re[1].Content;
                msg2 = re[1];
            }
            else
            {
                msg2 = null;
            }

            if (!re[2].Content.Equals(lastMsg3))
            {
                lastMsg3 = re[2].Content;
                msg3 = re[2];
            }
            else
            {
                msg3 = null;
            }

            if (!re[3].Content.Equals(lastMsg4))
            {
                lastMsg4 = re[3].Content;
                msg4 = re[3];
            }
            else
            {
                msg4 = null;
            }

            if (ConfigManager.pushNewsConfig.UsCompute)
            {
                if (msg1 != null)
                {
                    StringCompute stringcompute1 = new StringCompute();
                    stringcompute1.Compute(msg1.Content, lastMsg2);
                    if ((float)stringcompute1.ComputeResult.Rate > ConfigManager.pushNewsConfig.ComputeRate)
                    {
                        msg1 = null;
                    }
                }
                if (msg2 != null)
                {
                    StringCompute stringcompute1 = new StringCompute();
                    stringcompute1.Compute(msg2.Content, lastMsg1);
                    if ((float)stringcompute1.ComputeResult.Rate > ConfigManager.pushNewsConfig.ComputeRate)
                    {
                        msg2 = null;
                    }
                }

                if (msg1 != null && msg2 != null)
                {
                    StringCompute stringcompute1 = new StringCompute();
                    stringcompute1.Compute(msg1.Content, msg2.Content);
                    if ((float)stringcompute1.ComputeResult.Rate > ConfigManager.pushNewsConfig.ComputeRate)
                    {
                        msg2 = null;
                    }
                }
            }

            var reMsg = string.Empty;
            if (msg1 != null)
            {
                if (msg1.NewsLevel == NewsLevel.Normal)
                {
                    normalMsgList.Add(msg1.From+msg1.Content + "\n");
                }
                else
                {
                    reMsg += msg1.From + msg1.Content + "\n";
                }
            }
            if (msg2 != null)
            {
                if (msg2.NewsLevel == NewsLevel.Normal)
                {
                    normalMsgList.Add(msg2.From + msg2.Content + "\n");
                }
                else
                {
                    reMsg += msg2.From + msg2.Content + "\n";
                }
            }
            if (msg3 != null)
            {
                if (msg3.NewsLevel == NewsLevel.Normal)
                {
                    normalMsgList.Add(msg3.From + msg3.Content + "\n");
                }
                else
                {
                    reMsg += msg3.From + msg3.Content + "\n";
                }
            }
            if (msg4 != null)
            {
                if (msg4.NewsLevel == NewsLevel.Normal)
                {
                    normalMsgList.Add(msg4.From + msg4.Content + "\n");
                }
                else
                {
                    reMsg += msg4.From + msg4.Content + "\n";
                }
            }

            if (!string.IsNullOrEmpty(reMsg))
            {
                //查询币圈
                reMsg += _coinmarketcapService.GetTicker("btc").Result;
                reMsg += "\n" + _huobiService.LegalTender().Result;

                //涨跌幅排名
                //reMsg += "\n【OK涨幅排名】"+_coinService.GetOkexTopTracks();
                //reMsg += "\n【OK跌幅排名】" + _coinService.GetOkexBottomTracks();

                if (sendCount != 0)
                {
                    foreach (var group in ConfigManager.pushNewsConfig.CoinNewsPushGroupList)
                    {
                        _mahuaApi.SendGroupMessage(group, reMsg + "\n第" + sendCount + "次主动推送消息");
                    }
                }
                sendCount++;
            }

            if (nowNormalNewsPushIntervalCount < ConfigManager.pushNewsConfig.NormalNewsPushIntervalCount)
            {
                nowNormalNewsPushIntervalCount++;
            }
            else
            {
                if (sendCount != 0)
                {
                    var normalMsg = string.Empty;
                    foreach (var item in normalMsgList)
                    {
                        normalMsg += item;
                    }
                    normalMsgList.Clear();
                    foreach (var group in ConfigManager.pushNewsConfig.CoinNewsPushGroupList)
                    {
                        _mahuaApi.SendGroupMessage(group, normalMsg + "\n第" + sendCount + "次主动推送消息");
                    }
                }
                sendCount++;
                nowNormalNewsPushIntervalCount = 0;
            }
        }

        private void CheckMorningPaper()
        {
            var reMesg = _pmtownService.GetMorningPapaer().Result;
            if (lastMorningPaper == null)
            {
                lastMorningPaper = reMesg;
            }
            else if (!reMesg.Substring(0, 24).Equals(lastMorningPaper.Substring(0, 24)))
            {
                lastMorningPaper = reMesg;
            }
            else
            {
                reMesg = null;
            }

            if (reMesg != null)
            {
                foreach (var group in ConfigManager.pushNewsConfig.MorningPaperPushGroupList)
                {
                    _mahuaApi.SendGroupMessage(group, reMesg + "\n第" + morningPaperSendCount + "天推送消息");
                }
                foreach (var group in ConfigManager.pushNewsConfig.MorningPaperPushPrivateList)
                {
                    _mahuaApi.SendPrivateMessage(group, reMesg + "\n第" + morningPaperSendCount + "天推送消息");
                }
            }
            morningPaperSendCount++;
        }
    }

    #region News Job
    public class GetNewsJob : IJob
    {
        private readonly ICoinNewsTimeJob _coinNewsTimeJob;

        public GetNewsJob(
            ICoinNewsTimeJob coinNewsTimeJob)
        {
            _coinNewsTimeJob = coinNewsTimeJob ?? throw new ArgumentNullException(nameof(coinNewsTimeJob));
        }

        public void Execute(IJobExecutionContext context)
        {
            _coinNewsTimeJob.CheckNews();
        }
    }
    #endregion
}
