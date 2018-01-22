using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace LarkFramework.ResourcesMgr
{
    /// <summary>
    /// 资源管理类
    /// </summary>
    public class ResourcesMgr : SingletonMono<ResourcesMgr>
    {

        public int maxLoadNum = 5;

        /// <summary>
        /// 已经加载的资源
        /// </summary>
        private Dictionary<string, object> nameAssetDict = new Dictionary<string, object>();

        /// <summary>
        /// 正在加载的列表
        /// </summary>
        private List<LoadAssets> loadingList = new List<LoadAssets>();

        /// <summary>
        /// 等待加载的列表
        /// </summary>
        private Queue<LoadAssets> waitingQue = new Queue<LoadAssets>();

        private void Update()
        {
            if (loadingList.Count > 0)
            {
                for (int i = 0; i < loadingList.Count; i++)
                {
                    if (loadingList[i].IsDone)
                    {
                        LoadAssets asset = loadingList[i];
                        for (int j = 0; j < asset.Listeners.Count; j++)
                        {
                            asset.Listeners[j].OnLoaded(asset.AssetName, asset.GetAsset);
                        }
                        loadingList.RemoveAt(i);
                    }
                }
            }

            while (waitingQue.Count > 0 && loadingList.Count < maxLoadNum)
            {
                LoadAssets asset = waitingQue.Dequeue();
                loadingList.Add(asset);
                asset.LoadAsync();
            }
        }

        public void Load(string assetName, Type assetType, IResourcesListener listener)
        {
            //如果已经加载，直接返回
            if (nameAssetDict.ContainsKey(assetName))
            {
                listener.OnLoaded(assetName, nameAssetDict[assetName]);
                return;
            }
            else
            {
                //没有加载，开始异步加载
                LoadAsync(assetName, assetType, listener);
            }
        }

        public void LoadAsync(string assetName, Type assetType, IResourcesListener listener)
        {
            //资源正在被加载，还没加载完
            foreach (var item in loadingList)
            {
                if (item.AssetName == assetName)
                {
                    item.AddListener(listener);
                    return;
                }
            }

            //等待的队列里有
            foreach (var item in waitingQue)
            {
                if (item.AssetName == assetName)
                {
                    item.AddListener(listener);
                    return;
                }
            }

            //都没有，先创建
            LoadAssets asset = new LoadAssets();
            asset.AssetName = assetName;
            asset.AssetType = assetType;
            asset.AddListener(listener);
            //添加到等待队列
            waitingQue.Enqueue(asset);
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="assetName"></param>
        /// <returns></returns>
        public object GetAsset(string assetName)
        {
            object asset = null;
            nameAssetDict.TryGetValue(assetName, out asset);
            return asset;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="assetName"></param>
        public void ReleaseAsset(string assetName)
        {
            if (nameAssetDict.ContainsKey(assetName))
            {
                nameAssetDict[assetName] = null;
                nameAssetDict.Remove(assetName);
            }
        }

        /// <summary>
        /// 强制释放全部资源
        /// </summary>
        public void ReleaseAllAsset()
        {
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }
    }

}
