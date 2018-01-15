using UnityEngine;
using UnityEngine.UI;

namespace LarkFramework.UI
{
    public partial class UIWindow:UIPanel
    {
        //-----------------------------------------------
        public delegate void CloseEvent(object arg=null);
        //-----------------------------------------------

        /// <summary>
        /// 关闭按钮，大部分窗口都会有关闭按钮
        /// </summary>
        [SerializeField]
        private Button m_btnClose;

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        public event CloseEvent onClose;

        /// <summary>
        /// 打开UI的参数
        /// </summary>
        protected object m_openArg;

        /// <summary>
        /// 该UI的当前实例是否曾经被打开过
        /// </summary>
        private bool m_isOpenedOnce;

        /// <summary>
        /// 当UIPage被激活时调用
        /// </summary>
        protected void OnEnable()
        {
            this.Log("OnEnable()");
            if (m_btnClose != null)
            {
                m_btnClose.onClick.AddListener(OnBtnClose);
            }

#if UNITY_EDITOR
            if (m_isOpenedOnce)
            {
                //如果UI曾经被打开过，
                //则可以通过UnityEditor来快速出发Open/Close操作
                //方便调试
                OnOpen(m_openArg);
            }
#endif
        }

        /// <summary>
        /// 当UI不可用时调用
        /// </summary>
        protected void OnDisable()
        {
            this.Log("OnDisable()");

#if UNITY_EDITOR
            if (m_isOpenedOnce)
            {
                //如果UI曾经被打开过，
                //则可以通过UnityEditor来快速出发Open/Close操作
                //方便调试
                OnClose();
                if (onClose != null)
                {
                    onClose();
                    onClose = null;
                }
            }
#endif

            if (m_btnClose != null)
            {
                m_btnClose.onClick.RemoveAllListeners();
            }
        }

        /// <summary>
        /// 当点击"关闭"时调用
        /// 但是并不是每一个Window都有返回按钮
        /// </summary>
        private void OnBtnClose()
        {
            this.Log("OnBtnClose()");
            Close();
        }

        /// <summary>
        /// 调用它打开UIPage
        /// </summary>
        /// <param name="arg"></param>
        public sealed override void Open(object arg = null)
        {
            this.Log("Open()");
            m_openArg = arg;
            m_isOpenedOnce = false;

            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }

            OnOpen(arg);
            m_isOpenedOnce = true;
        }

        public sealed override void Close(object arg = null)
        {
            this.Log("Close()");

            if (this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }

            OnClose(arg);
            if (onClose != null)
            {
                onClose(arg);
                onClose = null;
            }
        }
    }
}
