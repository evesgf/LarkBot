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
        private static string morningPaper = null;

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

            string msg1 = null;
            if (!lastMsg1.Equals(re[0]))
            {
                lastMsg1 = re[0];
                msg1 = re[0] + "\n";
            }
            else
            {
                msg1 = null;
            }

            string msg2 = null;
            if (!lastMsg2.Equals(re[1]))
            {
                lastMsg2 = re[1];
                msg2 = re[1] + "\n";
            }
            else
            {
                msg2 = null;
            }

            string msg3 = null;
            if (!lastMsg3.Equals(re[2]))
            {
                lastMsg3 = re[2];
                msg3 = re[2] + "\n";
            }
            else
            {
                msg3 = null;
            }

            string msg4 = null;
            if (!lastMsg4.Equals(re[3]))
            {
                lastMsg4 = re[3];
                msg4 = re[3] + "\n";
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
                    stringcompute1.Compute(msg1, lastMsg2);
                    if ((float)stringcompute1.ComputeResult.Rate > ConfigManager.pushNewsConfig.ComputeRate)
                    {
                        msg1 = null;
                    }
                }
                if (msg2 != null)
                {
                    StringCompute stringcompute1 = new StringCompute();
                    stringcompute1.Compute(msg2, lastMsg1);
                    if ((float)stringcompute1.ComputeResult.Rate > ConfigManager.pushNewsConfig.ComputeRate)
                    {
                        msg2 = null;
                    }
                }

                if (msg1 != null && msg2 != null)
                {
                    StringCompute stringcompute1 = new StringCompute();
                    stringcompute1.Compute(msg1, msg2);
                    if ((float)stringcompute1.ComputeResult.Rate > ConfigManager.pushNewsConfig.ComputeRate)
                    {
                        msg2 = null;
                    }
                }
            }

            if (msg1 != null || msg2 != null || msg3 != null || msg4 != null)
            {
                string reMsg = msg1 + msg2 + msg3 + msg4;

                //reMsg += "【场外币价】";
                //var re2 = _coinService.OTCPrice().Data;
                //foreach (var str in re2)
                //{
                //    reMsg += str + "\n";
                //}

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
        }

        private void CheckMorningPaper()
        {
            var re = _pmtownService.GetMorningPapaer().Result;
            if (!morningPaper.Equals(re))
            {
                morningPaper = re;
            }
            else
            {
                morningPaper = null;
            }

            if ( morningPaper!=null)
            {
                foreach (var group in ConfigManager.pushNewsConfig.MorningPaperPushGroupList)
                {
                    _mahuaApi.SendGroupMessage(group, morningPaper + "\n第" + morningPaperSendCount + "天推送消息");
                }
                foreach (var group in ConfigManager.pushNewsConfig.MorningPaperPushPrivateList)
                {
                    _mahuaApi.SendPrivateMessage(group, morningPaper + "\n第" + morningPaperSendCount + "天推送消息");
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
