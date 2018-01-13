using System;
using System.Collections.Generic;
using System.Text;

namespace Business
{
    public class CrawlerResult<T>
    {
        /// <summary>
        /// 返回状态
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Msg { get; set; }
    }
}
