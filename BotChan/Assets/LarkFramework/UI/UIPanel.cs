using System.Collections;
using UnityEngine;

namespace LarkFramework.UI
{

    public abstract partial class UIPanel : MonoBehaviour
    {

        public virtual void Open(object arg = null)
        {
            this.Log("Open() arg:{0}", arg);
        }

        public virtual void Close(object arg = null)
        {
            this.Log("Close()");
        }

        /// <summary>
        /// 当前UI是否打开
        /// </summary>
        public bool IsOpen
        {
            get
            {
                return this.gameObject.activeSelf;
            }
        }

        /// <summary>
        /// 当UI关闭时，会响应该函数
        /// 该函数在重写时，需支持可重复调用
        /// </summary>
        protected virtual void OnClose(object arg = null)
        {
            this.Log("OnClose()");
        }

        /// <summary>
        /// 当ui打开时，会响应这个函数
        /// </summary>
        protected virtual void OnOpen(object arg=null)
        {
            this.Log("OnOpen()");
        }
    }
}
