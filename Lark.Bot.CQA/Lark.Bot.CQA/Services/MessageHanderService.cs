using Newbe.Mahua.MahuaEvents;
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
            //看币价 btc
            if (context.Message.Length > 5 && context.Message.Substring(0, 4).Equals("看币价 "))
            {
                //var key = context.Message.Remove(0, 4);
                //var re = _coinmarketcapService.GetTicker(key);

                //return re;
            }

            //查币价 eos btc
            if (context.Message.Length > 4 && context.Message.Substring(0, 4).Equals("查币价 "))
            {
                string key = context.Message.Remove(0, 4);

                var reOkex = _okexService.Ticker(key);
                var reHuobi = _huobiService.Ticker(key);

                string[] keys = context.Message.Split(' ');
                //eos
                var reCM =_coinmarketcapService.GetTicker(keys[1]);

                return "【okex】" + reOkex +"\n【火币】"+ reHuobi+"\n【cm】"+ reCM;
            }

            return null;
        }
    }
}
