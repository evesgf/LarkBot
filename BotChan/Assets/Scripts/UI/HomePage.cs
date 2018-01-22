using LarkFramework.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    public class HomePage : UIPage
    {
        public Button btn_Setting;

        protected override void OnOpen(object arg = null)
        {
            base.OnOpen(arg);

            GUIAniOpen();

            btn_Setting.onClick.AddListener(OnSetting);
        }

        private void OnSetting()
        {
            UIManager.Instance.OpenWindow(UIDef.SettingWindow);
        }
    }
}
