using Lark.Bot.CQA.Services.News;
using Lark.Bot.CQA.Services.Problem;
using Lark.Bot.CQA.Uitls.Config;
using Newbe.Mahua.MahuaEvents;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services
{
    public class MessageHanderService: IMessageHanderService
    {
        private readonly ICoinmarketcapService _coinmarketcapService;
        private readonly IOkexService _okexService;
        private readonly IHuobiService _huobiService;
        private readonly INewsService _newsService;
        private readonly IPmtownService _pmtownService;
        private readonly IProblemService _problemService;
        private readonly ILintCodeService _lintCodeService;

        public MessageHanderService(ICoinmarketcapService coinmarketcapService,
            IOkexService okexService, IHuobiService huobiService, INewsService newsService, IPmtownService pmtownService, IProblemService problemService,ILintCodeService lintCodeService)
        {
            _coinmarketcapService = coinmarketcapService;
            _okexService = okexService;
            _huobiService = huobiService;
            _newsService = newsService;
            _pmtownService = pmtownService;
            _problemService = problemService;
            _lintCodeService = lintCodeService;
        }

        /// <summary>
        /// 检查关键词
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public string CheckKeyWordAsync(GroupMessageReceivedContext context)
        {
            //btc
            Regex regEnglish = new Regex("^[a-z]");
            if (context.Message.Length < 7  && regEnglish.IsMatch(context.Message))
            {
                string key = ConfigManager.CheckSymbol(context.Message);
                if (!string.IsNullOrEmpty(key))
                {
                    string re = _coinmarketcapService.GetTicker(context.Message).Result;
                    return re;
                }
            }

            //查币价 eos btc
            if (context.Message.Length > 4 && context.Message.Substring(0, 4).Equals("查币价 "))
            {
                string key = context.Message.Remove(0, 4);

                string reOkex = _okexService.Ticker(key).Result;
                string reHuobi = _huobiService.Ticker(key).Result;

                string[] keys = context.Message.Split(' ');
                //eos
                string reCM =_coinmarketcapService.GetTicker(keys[1]).Result;

                return "【okex】" + reOkex +"\n【火币】"+ reHuobi+"\n【cm】"+ reCM;
            }

            //场外币价
            if (context.Message.Equals("场外币价"))
            {
                string huobiOTC = _huobiService.LegalTender().Result;

                return huobiOTC;
            }

            //币圈消息
            if (context.Message.Equals("币圈消息"))
            {
                NewsResult[] re = _newsService.RequestBiQuanApi();
                string reNews = re[0].From+re[0].Content;
                for (int i = 1; i < re.Length; i++)
                {
                    if (re[i] != null)
                    {
                        reNews += "\n" + re[i].From + re[i].Content;
                    }
                }
                return reNews;
            }

            //早报
            if (context.Message.Equals("早报"))
            {
                string reNews = _pmtownService.GetMorningPapaer().Result;
                return reNews;
            }

            //早报
            if (context.Message.Equals("每日一题"))
            {
                string reNews = _problemService.GetRundomProblem();
                return reNews;
            }

            //搜题 unity
            if (context.Message.Length > 3 && context.Message.Substring(0, 3).Equals("搜题 "))
            {
                string key = context.Message.Remove(0, 3);

                string reNews = _problemService.GetRundomRoblemToTag(key);
                return reNews;
            }

            //Roll
            if (context.Message.Length ==4 && context.Message.Equals("roll"))
            {
                var coin = new Random().Next(0, 100);
                if (coin == 100)
                {
                    return coin.ToString() + " 欧皇在世!!!";
                }
                if (coin >= 90)
                {
                    return coin.ToString() + " 金色传说!";
                }
                else if (coin >= 80)
                {
                    return coin.ToString() + " 传说!";
                }
                else if (coin >= 70)
                {
                    return coin.ToString() + " 史诗!";
                }
                else if (coin >= 60)
                {
                    return coin.ToString() + " 稀有!";
                }
                else if (coin < 60 && coin > 10)
                {
                    return coin.ToString() + " 非酋预定~";
                }
                else
                {
                    return coin.ToString() + " 真·非酋~";
                }
            }

            //Roll
            if (context.Message.Length == 2 && context.Message.Equals("刷题"))
            {
                return _lintCodeService.RefshNowProblem();
            }

            //Roll
            if (context.Message.Length == 4 && context.Message.Equals("今日题目"))
            {
                return _lintCodeService.GetNowProblem();
            }

            //Roll
            if (context.Message.Length == 4 && context.Message.Equals("查看答案"))
            {
                return _lintCodeService.GetNowAnswer();
            }

            //Roll
            if (context.Message.Length == 5 && context.Message.Equals("杨老师好帅"))
            {

                return "等杨老师下班回家就加上答案";
            }

            //复读机
            if (new Random().Next(0, 100) > 98)
            {
                return context.Message;
            }

            //检测复读
            if (context.Message.Equals(ConfigManager.lastMessage) && ConfigManager.lastMessage.Equals(ConfigManager.lastLastMessage) && ConfigManager.lastLastMessage.Equals(ConfigManager.lastLastLastMessage) && (DateTime.Now.Minute - ConfigManager.reReadTime.Minute) > 1)
            {
                ConfigManager.lastMessage = "";
                ConfigManager.lastLastMessage = "";
                ConfigManager.lastLastLastMessage = "";
                ConfigManager.reReadTime = DateTime.Now;
                return context.Message;
            }

            
            ConfigManager.lastLastLastMessage = ConfigManager.lastLastMessage;
            ConfigManager.lastLastMessage = ConfigManager.lastMessage;
            ConfigManager.lastMessage = context.Message;

            return null;
        }
    }
}
