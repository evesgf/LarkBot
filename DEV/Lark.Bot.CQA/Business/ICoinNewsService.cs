using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Business
{
    public interface ICoinNewsService
    {
        /// <summary>
        /// 查询币圈消息
        /// </summary>
        /// <returns></returns>
        string[] RequestBiQuanApi();
    }
}
