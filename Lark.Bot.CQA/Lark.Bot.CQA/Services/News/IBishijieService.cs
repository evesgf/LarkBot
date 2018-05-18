using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services.News
{
    public interface IBishijieService
    {
        Task<string> GetLatestNewsFlash();
    }
}
