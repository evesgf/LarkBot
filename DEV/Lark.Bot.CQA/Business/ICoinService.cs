using System.Collections.Generic;

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
        /// 获取火币币价
        /// </summary>
        /// <param name="key">btc_usdt</param>
        /// <returns></returns>
        string GetHuobiPrice(string key);

        /// <summary>
        /// 获取MyToken币价
        /// </summary>
        /// <param name="key">btc</param>
        /// <returns></returns>
        string GetMyTokenPrice(string key);

        /// <summary>
        /// 获取OKEX所有币对的列表
        /// </summary>
        /// <returns></returns>
        OkexTackers GetOkexAllTackers();

        /// <summary>
        /// 获取OKEX涨幅前十的币
        /// </summary>
        /// <returns></returns>
        string GetOkexTopTracks();

        /// <summary>
        /// 获取OKEX跌幅前十的币
        /// </summary>
        /// <returns></returns>
        string GetOkexBottomTracks();
    }
}