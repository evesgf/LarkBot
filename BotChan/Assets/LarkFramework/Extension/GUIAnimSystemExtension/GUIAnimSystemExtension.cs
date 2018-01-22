using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LarkFramework.UI
{
    public abstract partial class UIPanel
    {
        public GUIAnim[] guiAnims;

        public void GUIAniOpen()
        {
            foreach (var item in guiAnims)
            {
                item.MoveIn();
            }
        }

        /// <summary>
        /// GUIAni退出
        /// </summary>
        public void GUIAniClose()
        {
            foreach (var item in guiAnims)
            {
                item.MoveOut();
            }
        }

        /// <summary>
        /// 关闭UI
        /// </summary>
        public virtual void Close()
        {
            Close(0, null);
        }

        /// <summary>
        /// 等待几秒后关闭
        /// </summary>
        /// <param name="waitSeconds"></param>
        /// <param name="arg"></param>
        public virtual void Close(float waitSeconds,object arg = null)
        {
            this.Log("Close() waitSeconds:"+ waitSeconds);
            StartCoroutine(WaitClose(waitSeconds));
        }

        IEnumerator WaitClose(float waitSeconds, object arg = null)
        {
            yield return new WaitForSeconds(waitSeconds);
            Close(arg);
        }
    }
}