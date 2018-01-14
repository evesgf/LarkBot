using Lark.Bot.CQA.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Modules.Coin
{
    public interface ICoinService: IDependency
    {
        /// <summary>
        /// 场外币价
        /// </summary>
        /// <returns></returns>
        string OTCPrice();
    }
}
