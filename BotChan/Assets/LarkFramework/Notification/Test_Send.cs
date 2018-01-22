using UnityEngine;
using System.Collections;

namespace LarkFramework.Test
{
    public class Test_Send : MonoBehaviour
    {
        public int RecNum;
        public GameObject Rec;
        bool isFirst = true;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (isFirst)
            {
                //Do someting..
                CreateRec();

                isFirst = false;
            }

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                NotificationCenter.DefaultCenter().PostNotification(this, "ApplyDamage", "Hello World");
            }
        }

        void CreateRec()
        {
            for (int i = 0; i < RecNum; i++)
            {
                Instantiate(Rec, transform.position, transform.rotation);
            }
        }
    }
}
