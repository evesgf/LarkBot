using System.Collections;
using System.Collections.Generic;
using LarkFramework.Download;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public Text txt_Name;
    public Text txt_Size;

    public Button btn_Pause;
    public Button btn_Stop;
    public Button btn_Remove;

    public Slider Slider;

    private long m_TotalSize;

    private LoadUpdateCallback m_LoadUpdateCallback;

    public void Init(long totalSize,string fileName, string url, string savePath, object userData = null,LoadSuccessCallback loadSuccessCallback = null, LoadFailureCallback loadFailureCallback = null, LoadUpdateCallback loadUpdateCallback = null)
    {
        txt_Name.text = fileName;
        m_TotalSize = totalSize;
        txt_Size.text = "0/" + totalSize;
        Slider.value = 0;

        loadUpdateCallback += ShowSlider;
        loadSuccessCallback += OK;
        loadFailureCallback += GG;

        DownloadManager.Instance.AddDownLoadTask(fileName, url, savePath, loadSuccessCallback, loadFailureCallback, loadUpdateCallback);
    }

    public void Pause()
    {

    }

    public void Stop()
    {

    }

    public void Remove()
    {

    }


    public void OK()
    {
        print(txt_Name.text+"搞定了啊");
    }

    public void GG()
    {
        print(txt_Name.text + "咋回事啊？");
    }

    public void ShowSlider(float processValue, long fileLoadSize = 0,long fileTotalSize = 0)
    {
        UpdateItem(processValue, fileLoadSize,fileTotalSize);
    }

    public void UpdateItem(float value,long loadSize, long totalSize)
    {
        Slider.value = value;
        txt_Size.text = (loadSize / 1024+ "K/"+ totalSize / 1024+"K");
    }
}
