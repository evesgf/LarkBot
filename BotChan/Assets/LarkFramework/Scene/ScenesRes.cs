using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LarkFramework.Scenes
{
    public static class ScenesRes
    {
        public static string SceneResRoot = "Scene/";

        /// <summary>
        /// 加载场景
        /// </summary>
        /// <param name="name"></param>
        /// /// <param name="mode"></param>
        /// <returns></returns>
        public static void LoadScene(string name,bool isAdditive=false)
        {
            Debug.Log("Load Asset:" + SceneResRoot + name);
            //GameObject asset = (Scene)Resources.Load(SceneResRoot + name);

            SceneManager.LoadScene(name);
        }
    }
}
