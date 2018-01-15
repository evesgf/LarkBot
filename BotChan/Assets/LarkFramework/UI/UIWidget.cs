namespace LarkFramework.UI
{
    public partial class UIWidget:UIPanel
    {
        /// <summary>
        /// 打开UI的参数
        /// </summary>
        protected object m_openArg;

        /// <summary>
        /// 调用它打开UIWindow
        /// </summary>
        /// <param name="arg"></param>
        public override void Open(object arg = null)
        {
            this.Log("Open() arg:{0}", arg);
            m_openArg = arg;
            if (!this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(true);
            }

            OnOpen(arg);
        }


        /// <summary>
        /// 调用它以关闭UIWindow
        /// </summary>
        public override void Close(object arg=null)
        {
            this.Log("Close()");
            if (this.gameObject.activeSelf)
            {
                this.gameObject.SetActive(false);
            }

            OnClose();
        }
    }
}
