/*---------------------------------------------------------------
 * 作者：evesgf    创建时间：2017-11-15 17:10:47
 * 修改：evesgf    修改时间：2017-11-15 17:10:50
 *
 * 版本：V0.0.1
 * 
 * 描述：多线程文件下载，支持断线重连
 ---------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace LarkFramework.Download
{
    /// <summary>
    /// 下载管理器接口。
    /// </summary>
    public interface IDownLoadManager
    {
        /// <summary>
        /// 初始化下载管理器
        /// </summary>
        void Init(int maxLoad = 4, int minLoad=2, int flushSize = 1024 * 1024, int timeOut = 5 * 1000);

        /// <summary>
        /// 添加一个下载任务
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="url">下载地址</param>
        /// <param name="savePath">保存地址</param>
        /// <param name="reStartInterval">断线重连间隔时间</param>
        /// <param name="loadSuccessCallback">下载成功回调</param>
        /// <param name="loadFailureCallback">下载失败回调</param>
        /// <param name="flushSize">缓冲区大小</param>
        /// <param name="timeOut">超时时间</param>
        /// <param name="reStartCount">断线重连次数</param>
        /// <param name="loadUpdateCallback">下载中回调</param>
        /// <param name="threadPriority">线程优先级</param>
        void AddDownLoadTask(string fileName, string url, string savePath, LoadSuccessCallback loadSuccessCallback = null, LoadFailureCallback loadFailureCallback = null, LoadUpdateCallback loadUpdateCallback = null, int flushSize=1024*1024, int timeOut=5*1000, int reStartCount=5, int reStartInterval=1000, ThreadPriority threadPriority = ThreadPriority.Normal);

        /// <summary>
        /// 重新开始某个下载任务
        /// </summary>
        /// <param name="downloadTask"></param>
        void ReStartDownLoadTask(DownloadTask downloadTask);

        /// <summary>
        /// 暂停某个下载任务
        /// </summary>
        /// <param name="downloadTask"></param>
        void PauseDownLoadTask(DownloadTask downloadTask);

        /// <summary>
        /// 停止某个下载任务
        /// </summary>
        /// <param name="downloadTask"></param>
        void StopDownLoadTask(DownloadTask downloadTask);

        /// <summary>
        /// 移除某个下载任务
        /// </summary>
        /// <param name="downloadTask"></param>
        void RemoveDownLoadTask(DownloadTask downloadTask);

        /// <summary>
        /// 重新开始全部的下载任务
        /// </summary>
        void ReStartAllDowanLoadTask();

        /// <summary>
        /// 停止
        /// </summary>
        void StopAllDownLoadTask();

        /// <summary>
        /// 移除所有下载任务
        /// </summary>
        void RemoveAllDownLoadTask();
    }
}
