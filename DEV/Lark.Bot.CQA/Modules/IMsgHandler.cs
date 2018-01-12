using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Modules
{
    public interface IMsgHandler
    {
        //检查关键词
        HnadlerResult CheckKeyWords(string str);
    }
}
