using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services
{
    public interface IHuobiService
    {
        string LegalTender(string key);

        Task<string> Ticker(string key);
    }
}
