using System.Threading.Tasks;

namespace Lark.Bot.CQA.Services
{
    public interface IOkexService
    {
        string LegalTender(string key);

        Task<string> Ticker(string key);
    }
}
