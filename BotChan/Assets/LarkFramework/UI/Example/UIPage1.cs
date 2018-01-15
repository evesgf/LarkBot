namespace LarkFramework.UI.Example
{
    public class UIPage1 : UIPage
    {
        protected override void OnOpen(object arg = null)
        {
            base.OnOpen(arg);
        }

        public void OnBtnOpenPage2()
        {
            UIManager.Instance.OpenPage("UIPage2");
        }
    }
}

