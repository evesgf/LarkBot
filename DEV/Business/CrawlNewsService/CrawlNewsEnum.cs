using System;
using System.Collections.Generic;
using System.Text;

namespace Business.CrawlNewsService
{
    /// <summary>
    /// 新闻的重要等级,0-5，默认为0
    /// </summary>
    public enum EnumImportantLevel
    {
        /// <summary>
        /// 默认为0
        /// </summary>
        Level0 = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
        Level4 = 4,
        Level5 = 5
    }

    /// <summary>
    /// 新闻的推送等级，0-3，0为不推送，默认为0
    /// </summary>
    public enum EnumPushLevel
    {
        /// <summary>
        /// 默认为0
        /// </summary>
        Level0 = 0,
        Level1 = 1,
        Level2 = 2,
        Level3 = 3,
    }
}
