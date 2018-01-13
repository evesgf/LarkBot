namespace DTO
{
    public class ResultModel<T>
    {
        /// <summary>
        /// 返回状态
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 返回结果
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Msg { get; set; }
    }
}
