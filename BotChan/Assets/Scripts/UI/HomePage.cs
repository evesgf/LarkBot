using LarkFramework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class HomePage : UIPage
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
