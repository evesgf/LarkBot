using LarkFramework.Download;
using LarkFramework.Module;
using LarkFramework.Tick;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Download_Example : MonoBehaviour {

    public DownList[] downList;

    [System.Serializable]
    public class DownList
    {
        public string fileName;
        public string url;
        public string savePath;
    }

    public GameObject itemObj;
    public Transform itemRoot;

    // Use this for initialization
    void Start () {
        Init();
    }

    public void Init()
    {
        Debuger.EnableLog = true;

        ModuleManager.Instance.Init("LarkFramework.Module.Example");

        TickManager.Instance.Init();
        DownloadManager.Instance.Init();
    }

    public void DownAll()
    {
        foreach (var down in downList)
        {
            var item= Instantiate(itemObj, itemRoot).GetComponent<Item>();
            item.name = down.fileName;
            item.Init(0,down.fileName, down.url, down.savePath);
        }
    }

    public void PauseAll()
    {
        DownloadManager.Instance.ReStartAllDowanLoadTask();
    }

    public void RemoveAll()
    {

    }
}
