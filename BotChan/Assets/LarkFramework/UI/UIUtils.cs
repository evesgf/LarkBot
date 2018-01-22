using LarkFramework.Utils;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LarkFramework.UI
{
    /// <summary>
    /// 为UI操作提供基础封装，使UI操作更方便
    /// </summary>
    public static class UIUtils
    {
        /// <summary>
        /// 设置一个UI元素是否可见
        /// </summary>
        /// <param name="ui"></param>
        /// <param name="value"></param>
        public static void SetActive(UIBehaviour ui, bool value)
        {
            if (ui != null && ui.gameObject != null)
            {
                GameObjectUtils.SetActiveRecursively(ui.gameObject, value);
            }
        }

        internal static void SetButtonText(Button button, string v)
        {
            //TODO:临时方法
            var text = button.GetComponentInChildren<Text>();
            text.text = v;
        }

        internal static void SetChildText(UIBehaviour ctlTitle, string title)
        {
            //TODO:临时方法
            var text = ctlTitle.GetComponentInChildren<Text>();
            text.text = title;
        }
    }
}
