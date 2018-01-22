using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>  
/// 改变Prefab  
/// 注：通过名字匹配搜索被替换目标  
/// （被选中物体的所有子物体.name包含newPrefab.name则替换）  
/// </summary> 
public class ReplaceSceneObjEditor : EditorWindow {

    [MenuItem("LarkFramework/Tools/Replace Scene Obj")]
    public static void Open()
    {
        EditorWindow.GetWindow(typeof(ReplaceSceneObjEditor));
    }

    public GameObject oldPrefab;
    static GameObject tooldPrefab;
    public GameObject newPrefab;
    static GameObject tonewPrefab;

    void OnGUI()
    {
        oldPrefab = (GameObject)EditorGUILayout.ObjectField(oldPrefab, typeof(GameObject), true, GUILayout.MinWidth(50f));
        tooldPrefab = oldPrefab;
        newPrefab = (GameObject)EditorGUILayout.ObjectField(newPrefab, typeof(GameObject), true, GUILayout.MinWidth(50f));
        tonewPrefab = newPrefab;
        if (isChange)
        {
            GUILayout.Button("正在替换...");
        }
        else
        {
            if (GUILayout.Button("根据资源路径替换"))
                Change();
        }
    }

    static bool isChange = false;

    public static void Change()
    {
        if (tonewPrefab == null)
            return;

        isChange = true;
        //List<GameObject> destroy = new List<GameObject>();
        //Object[] labels = Selection.GetFiltered(typeof(GameObject), SelectionMode.Deep);
        //foreach (Object item in labels)
        //{
        //    GameObject tempGO = (GameObject)item; // (GameObject)item;  
        //    //只要搜到的物体包含新Prefab的名字，就会被替换  
        //    if (tempGO.name.Contains(tonewPrefab.name))
        //    {
        //        GameObject newGO = (GameObject)Instantiate(tonewPrefab);
        //        newGO.transform.SetParent(tempGO.transform.parent);
        //        newGO.name = tempGO.name;
        //        newGO.transform.localPosition = tempGO.transform.localPosition;
        //        newGO.transform.localRotation = tempGO.transform.localRotation;
        //        newGO.transform.localScale = tempGO.transform.localScale;

        //        destroy.Add(tempGO);
        //    }
        //}
        //foreach (GameObject item in destroy)
        //{
        //    DestroyImmediate(item.gameObject);
        //}

        List<GameObject> destroy = new List<GameObject>();
        GameObject[] gos = (GameObject[])FindObjectsOfType(typeof(GameObject));

        Debug.Log(AssetDatabase.GetAssetPath(tooldPrefab));

        foreach (var go in gos)
        {
            if (PrefabUtility.GetPrefabType(go) == PrefabType.ModelPrefabInstance)
            {
                UnityEngine.Object parentObject = EditorUtility.GetPrefabParent(go);

                string path = AssetDatabase.GetAssetPath(parentObject);
                Debug.Log(path);
                if (path == AssetDatabase.GetAssetPath(tooldPrefab))
                {
                    GameObject newGO = PrefabUtility.InstantiatePrefab(tonewPrefab) as GameObject;
                    newGO.name = go.name;
                    newGO.transform.localPosition = go.transform.localPosition;
                    newGO.transform.localRotation = go.transform.localRotation;
                    newGO.transform.localScale = go.transform.localScale;

                    destroy.Add(go);

                    Debug.Log(go.name + ":" + parentObject.name);
                }
            }


        }

        foreach (GameObject item in destroy)
        {
            DestroyImmediate(item.gameObject);
        }

        isChange = false;
    }
}
