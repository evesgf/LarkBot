using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LarkFramework.UI;

public class SettingWindow : UIWindow
{

    protected override void OnOpen(object arg = null)
    {
        base.OnOpen(arg);

        GUIAniOpen();
    }
}
