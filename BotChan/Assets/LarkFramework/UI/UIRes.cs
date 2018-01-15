using UnityEngine;

namespace LarkFramework.UI
{
    public static class UIRes
    {
        public static string UIResRoot = "UI/";

        /// <summary>
        /// 加载UI的Prefab
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static GameObject LoadPrefab(string name)
        {
            Debug.Log("Load Asset:"+UIResRoot+name);
            GameObject asset = (GameObject)Resources.Load(UIResRoot + name);
            return asset;
        }
    }
}
