using LarkFramework.Runtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LarkFramework.Scenes
{
    /// <summary>
    /// 界面组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Lark Framework/Scenes Component")]
    public class ScenesComponent : ComponentBase
    {
        public const string LOG_TAG = "ScenesComponent";

        private AsyncOperation async_operation;

        /// <summary>
        /// 当前场景中寻找UIRoot对象
        /// </summary>
        /// <returns></returns>
        public static ScenesComponent FindScenesComponent()
        {
            GameObject root = GameObject.Find("ScenesComponent");
            if (root != null && root.GetComponent<ScenesComponent>() != null)
            {
                return root.GetComponent<ScenesComponent>();
            }

            Debuger.LogError(LOG_TAG, "FindScenesComponent() ScenesComponent Is Not Exist!!!");
            return root.GetComponent<ScenesComponent>();
        }

        public void LoadSceneAnysc(string name, bool isAdditive = false, Action action = null)
        {
            StartCoroutine(LoadScene(name, isAdditive, action));
        }

        IEnumerator LoadScene(string name, bool isAdditive = false, Action action=null)
        {
            async_operation = SceneManager.LoadSceneAsync(name);
            yield return async_operation;

            if (action != null)
            {
                action.Invoke();
            }
        }
    }
}
