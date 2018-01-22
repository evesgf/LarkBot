using LarkFramework.Module;
using LarkFramework.Tick;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace LarkFramework.Download
{
    /// <summary>
    /// 下载管理器
    /// 参考：http://blog.csdn.net/dingxiaowei2013/article/details/77814966
    /// 参考：http://blog.csdn.net/damenhanter/article/details/50273303
    /// </summary>
    public class DownloadManager : ServiceModule<DownloadManager>,IDownLoadManager
    {
        /// <summary>
        /// 最大同时下载数量
        /// </summary>
        public int MAX_DOWNLOAD_COUNT { get; set; }
        /// <summary>
        /// 缓冲区大小
        /// </summary>
        public int FLUSH_SIZE { get; set; }

        private List<DownloadTask> m_DownList;                      //下载队列
        private Queue<DownloadTask> m_WaitQue;                      //等待下载队列
        private Queue<DownloadTask> completeQue;                    //下载完成队列

        private int m_FlushSize= 1024 * 1024;                       //缓冲区大小
        private int m_Timeout= 30 * 1000;                           //超时时间

        /// <summary>
        /// 初始化操作
        /// </summary>
        /// <param name="maxLoad">最大同时下载数量</param>
        /// <param name="minLoad">最小活动线程数以及可等待线程数</param>
        /// <param name="flushSize">缓冲区大小</param>
        /// <param name="timeOut">超时时间（毫秒）</param>
        public void Init(int maxLoad=4, int minLoad=2, int flushSize = 1024 * 1024, int timeOut= 30*1000)
        {
            CheckSingleton();

            m_DownList = new List<DownloadTask>();
            m_WaitQue = new Queue<DownloadTask>();
            completeQue = new Queue<DownloadTask>();

            MAX_DOWNLOAD_COUNT = maxLoad;
            FLUSH_SIZE = flushSize;
            m_Timeout = timeOut;

            //设置最大活动线程数以及可等待线程数
            ThreadPool.SetMaxThreads(maxLoad, maxLoad);
            //设置最小活动线程数以及可等待线程数
            ThreadPool.SetMinThreads(minLoad, minLoad);

            TickComponent.Instance.onUpdate += Update;

            Debuger.Log("DownloadManager Init Complete!");
        }

        /// <summary>
        /// 有限状态机管理器轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        internal void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (m_WaitQue.Count > 0)
            {
                if (m_DownList.Count < MAX_DOWNLOAD_COUNT)
                {
                    //移入下载队列
                    MoveTaskFromWaitDicToDwonDict();
                }
            }

            if (m_DownList.Count > 0)
            {
                //输出下载队列状态
                for (int i = 0; i < m_DownList.Count; i++)
                {
                    if (m_DownList[i].m_LoadUpdateCallback != null)
                    {
                        m_DownList[i].m_LoadUpdateCallback.Invoke(m_DownList[i].Progress, m_DownList[i].LoadLength, m_DownList[i].TotalLength);
                    }

                    if (m_DownList[i].IsDone)
                    {
                        //移除下载队列
                        m_DownList.Remove(m_DownList[i]);
                    }
                }
            }

            Debuger.Log("m_WaitQue:"+m_WaitQue.Count+ " m_DownList:" + m_DownList.Count);
        }

        /// <summary>
        /// 根据优先级从等待队列移动一个任务到下载队列
        /// </summary>
        public void MoveTaskFromWaitDicToDwonDict()
        {
            var downLoadTask = m_WaitQue.Dequeue();
            m_DownList.Add(downLoadTask);

            ThreadPool.QueueUserWorkItem(state => {
                downLoadTask.DownLoad();
            }, null);
        }

        /// <summary>
        /// 添加一个下载任务
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="url">下载地址</param>
        /// <param name="savePath">保存地址</param>
        /// <param name="loadSuccessCallback">下载成功回调</param>
        /// <param name="loadFailureCallback">下载失败回调</param>
        /// <param name="loadUpdateCallback">下载中回调</param>
        /// <param name="flushSize">缓冲区大小</param>
        /// <param name="timeOut">超时时间</param>
        /// <param name="reStartCount">断线重连次数</param>
        /// <param name="reStartInterval">断线重连间隔时间</param>
        /// <param name="threadPriority">线程优先级</param>
        public void AddDownLoadTask(string fileName, string url, string savePath, LoadSuccessCallback loadSuccessCallback = null, LoadFailureCallback loadFailureCallback = null, LoadUpdateCallback loadUpdateCallback = null, int flushSize = 1024*1024, int timeOut = 5000, int reStartCount = 5, int reStartInterval = 1000,  ThreadPriority threadPriority = ThreadPriority.Normal)
        {
            if (string.IsNullOrEmpty(fileName))
            {
                throw new Exception("Download fileName is invalid.");
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("Download url is invalid.");
            }

            if (string.IsNullOrEmpty(savePath))
            {
                throw new Exception("Download savePath is invalid.");
            }

            DownloadTask downloadTask = new DownloadTask(fileName, url, savePath, flushSize, timeOut, reStartCount, reStartInterval,loadSuccessCallback, loadFailureCallback, loadUpdateCallback, threadPriority);

            m_WaitQue.Enqueue(downloadTask);
        }

        public void ReStartDownLoadTask(DownloadTask downloadTask)
        {
            throw new NotImplementedException();
        }

        public void PauseDownLoadTask(DownloadTask downloadTask)
        {
            throw new NotImplementedException();
        }

        public void StopDownLoadTask(DownloadTask downloadTask)
        {
            throw new NotImplementedException();
        }

        public void RemoveDownLoadTask(DownloadTask downloadTask)
        {
            throw new NotImplementedException();
        }

        public void ReStartAllDowanLoadTask()
        {
            throw new NotImplementedException();
        }

        public void StopAllDownLoadTask()
        {
            throw new NotImplementedException();
        }

        public void RemoveAllDownLoadTask()
        {
            throw new NotImplementedException();
        }
    }
}
