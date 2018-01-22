using LarkFramework.Entity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LarkFramework.Entity.Example
{
    public class Entity_Example : MonoBehaviour
    {
        public Transform mapRoot;
        private ShipBase ship;

        // Use this for initialization
        void Start()
        {
            Debuger.EnableLog = true;
            //初始化工厂
            EntityFactory.Init();
            ViewFactory.Init(mapRoot);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ship = EntityFactory.InstanceEntity<ShipBase>();
                ship.Create(new Vector3(3,3,3));
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                EntityFactory.ReleaseEntity(ship);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                EntityFactory.ClearReleasedObjects();
            }
        }
    }
}
