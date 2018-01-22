using UnityEngine;
using System.Collections;

namespace LarkFramework.ResourcesMgr
{

    public interface IResourcesListener
    {
        /// <summary>
        /// 加载成功
        /// </summary>
        /// <param name="asset"></param>
        void OnLoaded(string assetName, object asset);
    }

}
