using UnityEngine;
using System.Collections;

namespace LarkFramework.Test
{
    public class Test_Rec : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {
            NotificationCenter.DefaultCenter().AddObserver(this, "ApplyDamage");
        }

        void ApplyDamage(Notification note)
        {
            Debug.Log("从:" + note.sender + ",接收一个信息内容:" + (string)note.data + ", 通知名称为:" + note.name);
        }
    }
}
