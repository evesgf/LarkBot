using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Modules.Coin
{
    public class CoinHandler: IMsgHandler
    {
        private readonly ICoinService _coinService;

        public CoinHandler(ICoinService coinService)
        {
            _coinService = coinService;
        }

        /// <summary>
        /// 检查是否村咋属于Coin的关键词
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public HnadlerResult CheckKeyWords(string str)
        {
            HnadlerResult hnadlerResult = new HnadlerResult { HasKeyWord = true, ResultMsg = string.Empty };

            switch (str)
            {
                case "场外币价":
                    hnadlerResult.ResultMsg = _coinService.OTCPrice();
                    break;


                default:        //未命中
                    hnadlerResult.HasKeyWord = false;
                    hnadlerResult.ResultMsg = "未命中";
                    break;
            }

            return hnadlerResult;
        }
    }
}
