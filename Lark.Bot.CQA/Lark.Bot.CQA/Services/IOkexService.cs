namespace Lark.Bot.CQA.Services
{
    public interface IOkexService
    {
        string LegalTender(string key);

        string Ticker(string key);
    }
}
