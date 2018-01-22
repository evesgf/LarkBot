using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// 单纯的只是画个LOGO~
/// </summary>
[InitializeOnLoad]
public class LarkFrameworkIconEditor
{
    // 层级窗口项回调
    private static readonly EditorApplication.HierarchyWindowItemCallback hiearchyItemCallback;

    private static Texture2D hierarchyEventIcon;

    private static Texture2D HierarchyEventIcon
    {
        get
        {
            if (LarkFrameworkIconEditor.hierarchyEventIcon == null)
            {
                LarkFrameworkIconEditor.hierarchyEventIcon = (Texture2D)Resources.Load("LarkFramework_Logo");
            }
            return LarkFrameworkIconEditor.hierarchyEventIcon;
        }
    }

    /// <summary>
    /// 静态构造
    /// </summary>
    static LarkFrameworkIconEditor()
    {
        LarkFrameworkIconEditor.hiearchyItemCallback = new EditorApplication.HierarchyWindowItemCallback(LarkFrameworkIconEditor.DrawHierarchyIcon);
        EditorApplication.hierarchyWindowItemOnGUI = (EditorApplication.HierarchyWindowItemCallback)Delegate.Combine(
            EditorApplication.hierarchyWindowItemOnGUI,
            LarkFrameworkIconEditor.hiearchyItemCallback);
    }

    private static void DrawHierarchyIcon(int instanceID, Rect selectionRect)
    {
        GameObject gameObject = EditorUtility.InstanceIDToObject(instanceID) as GameObject;

        if (gameObject != null)
        {
            if (!gameObject.name.Equals("GameEntry"))
                return;

            // 设置icon的位置与尺寸（Hierarchy窗口的左上角是起点）
            //Rect rect = new Rect(0, 0, 16, 16);
            //Rect rect = new Rect(selectionRect.x + selectionRect.width - 16f, selectionRect.y, 16f, 16f);
            Rect rect = new Rect(0, selectionRect.y, 16f, 16f);
            // 画icon
            GUI.DrawTexture(rect, LarkFrameworkIconEditor.HierarchyEventIcon);
        }
    }
}
