namespace Lark.Bot.CQA.Business
{
    public interface ICoinService
    {
        /// <summary>
        /// 场外币价
        /// </summary>
        /// <returns></returns>
        OTCPrice OTCPrice();

        /// <summary>
        /// 获取okex币价
        /// </summary>
        /// <param name="key">btc_usdt</param>
        /// <returns></returns>
        string GetOKEXCoinPrice(string key);

        /// <summary>
        /// 获取MyToken币价
        /// </summary>
        /// <param name="key">btc</param>
        /// <returns></returns>
        string GetMyTokenPrice(string key);
    }
}