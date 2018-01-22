using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace LarkFramework.ResourcesMgr
{

    /// <summary>
    /// 资源类
    /// </summary>
    public class LoadAssets
    {
        /// <summary>
        /// 资源信息
        /// </summary>
        private ResourceRequest request;

        /// <summary>
        /// 资源名称
        /// </summary>
        public string AssetName;

        /// <summary>
        /// 资源类型
        /// </summary>
        public Type AssetType;

        /// <summary>
        /// 是否加载完成
        /// </summary>
        public bool IsDone
        {
            get { return request != null && request.isDone; }
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        public object GetAsset
        {
            get
            {
                if (request == null)
                    return null;
                return request.asset;
            }
        }

        /// <summary>
        /// 异步加载
        /// </summary>
        public void LoadAsync()
        {
            request = Resources.LoadAsync(AssetName, AssetType);
        }

        /// <summary>
        /// 回调的集合
        /// </summary>
        public List<IResourcesListener> Listeners;

        public void AddListener(IResourcesListener listener)
        {
            if (Listeners == null)
                Listeners = new List<IResourcesListener>();

            if (Listeners.Contains(listener))
                return;

            Listeners.Add(listener);
        }
    }

}
