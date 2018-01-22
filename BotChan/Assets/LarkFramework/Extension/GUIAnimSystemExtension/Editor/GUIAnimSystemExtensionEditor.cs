using LarkFramework.UI;
using UnityEditor;
using UnityEngine;

namespace LarkFramework.Extension
{
    [CustomEditor(typeof(UIPanel),true)]
    public class GUIAnimSystemExtensionEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Find GUI Anim Component"))
            {
                var _tag = (UIPanel)target;
                var objs = _tag.GetComponentsInChildren<GUIAnim>();
                _tag.guiAnims = objs;

                Debug.Log("Find（"+objs.Length+"）GUI Anim Component");
            }
        }

    }
}
