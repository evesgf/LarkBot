using Lark.Bot.CQA.Uitls.Config;
using Newbe.Mahua.MahuaEvents;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services
{
    public class MessageHanderService: IMessageHanderService
    {
        private readonly ICoinmarketcapService _coinmarketcapService;
        private readonly IOkexService _okexService;
        private readonly IHuobiService _huobiService;

        public MessageHanderService(ICoinmarketcapService coinmarketcapService,
            IOkexService okexService, IHuobiService huobiService)
        {
            _coinmarketcapService = coinmarketcapService;
            _okexService = okexService;
            _huobiService = huobiService;
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
            if (context.Message.Length < 5  && regEnglish.IsMatch(context.Message))
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

            return null;
        }
    }
}
