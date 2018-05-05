using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services
{
    public interface IHuobiService
    {
        Task<string> LegalTender();

        Task<string> Ticker(string key);
    }
}
