using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;

namespace LarkFramework.Download
{
    /// <summary>
    /// 单个下载任务
    /// </summary>
    public class DownloadTask
    {
        /// <summary>
        /// 下载文件名
        /// </summary>
        public string FileName { get; set; }
        /// <summary>
        /// 下载地址
        /// </summary>
        public string Url { get; set; }

        private string m_SavePath;                              //文件保存地址
        private int m_TimeOut = 5 * 1000;                       //连接超时时间（毫秒）
        private int m_RWTimeOut = 3 * 1000;                     //读写超时时间（毫秒）
        private int m_ReStartCount = 5;                         //断线重连次数
        private int m_ReStartInterval = 1000;                   //断线重连间隔时间
        private int m_FlushSize;                                //缓冲区大小
        private ThreadPriority m_ThreadPriority;                //线程优先级

        public LoadSuccessCallback m_LoadSuccessCallback;       //成功回调
        public LoadFailureCallback m_LoadFailureCallback;       //失败回调
        public LoadUpdateCallback m_LoadUpdateCallback;         //Tick回调

        /// <summary>
        /// 下载进度
        /// </summary>
        public float Progress { get; set; }
        /// <summary>
        /// 下载是否完成
        /// </summary>
        public bool IsDone { get; set; }
        /// <summary>
        /// 已经下载的文件大小
        /// </summary>
        public long LoadLength { get; set; }
        /// <summary>
        /// 文件总长度
        /// </summary>
        public long TotalLength { get; set; }

        private Stopwatch stopWatch;                            //下载计时
        private FileStream fs;                                  //下载文件流
        private HttpWebRequest request;                         //文件流

        //涉及子线程要注意,Unity关闭的时候子线程不会关闭，所以要有一个标识
        private bool isStop;
        //子线程负责下载，否则会阻塞主线程，Unity界面会卡住
        private Thread thread;

        /// <summary>
        /// 重试次数
        /// </summary>
        public int reStartCount = 5;
        private int m_nowReStartCount = 0;
        /// <summary>
        /// 重试间隔(毫秒)
        /// </summary>
        public int reStartInterval = 1000;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="url">下载地址</param>
        /// <param name="savePath">保存地址</param>
        /// <param name="flushSize">缓冲区大小</param>
        /// <param name="timeOut">超时时间</param>
        /// <param name="reStartCount">断线重连次数</param>
        /// <param name="reStartInterval">断线重连间隔时间</param>
        /// <param name="loadSuccessCallback">下载成功回调</param>
        /// <param name="loadFailureCallback">下载失败回调</param>
        /// <param name="loadUpdateCallback">下载中回调</param>
        /// <param name="threadPriority">线程优先级</param>
        public DownloadTask(string fileName, string url, string savePath, int flushSize = 1024 * 1024, int timeOut = 5 * 1000, int reStartCount = 5, int reStartInterval = 1000, LoadSuccessCallback loadSuccessCallback = null, LoadFailureCallback loadFailureCallback = null, LoadUpdateCallback loadUpdateCallback = null, ThreadPriority threadPriority = ThreadPriority.Normal)
        {
            FileName = fileName;
            Url = url;
            m_SavePath = savePath;
            m_FlushSize = flushSize;
            m_TimeOut = timeOut;
            m_ReStartCount = reStartCount;
            m_ReStartInterval = reStartInterval;


            m_LoadSuccessCallback = loadSuccessCallback;
            m_LoadFailureCallback = loadFailureCallback;
            m_LoadUpdateCallback = loadUpdateCallback;

            m_ThreadPriority = threadPriority;
        }

        /// <summary>
        /// 开始下载
        /// </summary>
        public void DownLoad()
        {
            try
            {
                isStop = false;
                stopWatch = new Stopwatch();
                stopWatch.Start();

                //判断保存路径是否存在
                if (!Directory.Exists(m_SavePath))
                {
                    Directory.CreateDirectory(m_SavePath);
                }

                //这是要下载的文件名，比如从服务器下载a.zip到D盘，保存的文件名是test
                string filePath = m_SavePath + "/" + FileName;

                //使用流操作文件
                fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
                //获取文件现在的长度
                LoadLength = fs.Length;
                //获取下载文件的总长度
                TotalLength = GetLength(Url);

                Debuger.Log("<color=green>文件:" + FileName + " 已下载" + LoadLength / 1024 + "K，剩余" + ((TotalLength - LoadLength) / 1024) + "K</color>");

                //如果没下载完
                if (LoadLength < TotalLength)
                {
                    //断点续传核心，设置本地文件流的起始位置
                    fs.Seek(LoadLength, SeekOrigin.Begin);

                    request = WebRequest.Create(Url) as HttpWebRequest;

                    if (request != null)
                    {
                        request.ReadWriteTimeout = m_RWTimeOut;
                        request.Timeout = m_TimeOut;

                        //断点续传核心，设置远程访问文件流的起始位置
                        request.AddRange((int)LoadLength);

                        Stream stream = request.GetResponse().GetResponseStream();
                        byte[] buffer = new byte[1024];
                        //使用流读取内容到buffer中
                        //注意方法返回值代表读取的实际长度,并不是buffer有多大，stream就会读进去多少
                        if (stream != null)
                        {
                            int length = stream.Read(buffer, 0, buffer.Length);
                            //Debuger.Log("<color=red>length:{"+ length + "}</color>");
                            while (length > 0)
                            {

                                //如果Unity客户端关闭，停止下载
                                if (isStop)
                                {
                                    break;
                                }

                                //将内容再写入本地文件中
                                fs.Write(buffer, 0, length);
                                //计算进度
                                LoadLength += length;
                                Progress = (float)LoadLength / (float)TotalLength;
                                //UnityEngine.Debug.Log(progress);
                                //类似尾递归
                                length = stream.Read(buffer, 0, buffer.Length);
                            }
                        }
                        else
                        {
                            Debuger.Log("<color=yellow>文件:" + FileName + " stream请求为空！</color>");
                        }
                        stream.Close();
                        stream.Dispose();
                    }
                    else
                    {
                        Debuger.Log("<color=yellow>文件:" + FileName + " request请求为空！</color>");
                    }
                }
                else
                {
                    Progress = 1;
                }

                Debuger.Log("文件下载耗时: " + stopWatch.ElapsedMilliseconds);
            }
            catch (WebException ex)
            {
                Debuger.Log("<color=yellow>文件:" + FileName + " 下载出现异常:"+ex+"</color>");
                throw ex;
            }
            finally
            {
                stopWatch.Stop();
                fs.Close();
                fs.Dispose();

                if (request != null) request.Abort();

                //如果下载完毕，执行回调
                if (Progress == 1)
                {
                    IsDone = true;

                    if (m_LoadSuccessCallback != null) m_LoadSuccessCallback.Invoke();

                    Debuger.Log("<color=green>文件:"+FileName+" 下载完成</color>");
                }
                else
                {
                    if (m_nowReStartCount < reStartCount)
                    {
                        m_nowReStartCount++;
                        Debuger.Log("<color=orange>下载任务:"+FileName+" 正尝试第 "+m_nowReStartCount+ " 次重试</color>");

                        Thread.Sleep(reStartInterval);
                        DownLoad();
                    }
                    else
                    {
                        Debuger.Log("<color=red>文件:" + FileName + " 下载失败</color>");
                        if (m_LoadFailureCallback != null) m_LoadFailureCallback.Invoke();
                    }
                }

                if (thread != null) thread.Abort();
            }
        }


        /// <summary>
        /// 获取下载文件的大小
        /// </summary>
        /// <returns>The length.</returns>
        /// <param name="url">URL.</param>
        long GetLength(string url)
        {
            HttpWebRequest re = WebRequest.Create(url) as HttpWebRequest;
            re.Method = "HEAD";
            HttpWebResponse response = re.GetResponse() as HttpWebResponse;
            return response.ContentLength;
        }
    }
}
