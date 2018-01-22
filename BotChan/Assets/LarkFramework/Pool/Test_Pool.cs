using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LarkFramework.Test
{
    public class Test_Pool : MonoBehaviour
    {
        private ObjectPool<List<Vector3>> m_PoolOfListOfVector3 = new ObjectPool<List<Vector3>>(
            //5为假设最大数量
            5, 
           (list) =>
           {
               list.Clear();
           }, 
           (list) =>
           {
               //初始化容量为10
               list.Capacity = 10;
           }
        );

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                List<Vector3> listVector3= m_PoolOfListOfVector3.New();

                for (int i = 0; i < 20; i++)
                {
                    listVector3.Add(new Vector3(i, i, i));
                }

                Debug.Log(listVector3.Count);

                m_PoolOfListOfVector3.Store(listVector3);

                Debug.Log(listVector3.Count);
            }
        }
    }
}
