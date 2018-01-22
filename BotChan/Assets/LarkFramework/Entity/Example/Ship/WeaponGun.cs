using LarkFramework.Entity;
using LarkFramework.Entity.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LarkFramework.Example.Ship
{
    public class WeaponGun: ViewObject
    {
        [SerializeField]
        private Transform m_weaponRoot;

        private WeaponBase m_entity;

        protected override void Create(EntityObject entity)
        {
            m_entity = entity as WeaponBase;

            this.transform.parent = m_entity.Root();
            this.transform.localPosition = Vector3.zero;
            //this.transform.localRotation = m_entity.Root().localRotation;

            Debug.Log("Create:" + m_entity);
        }

        protected override void Release()
        {
            m_entity = null;
        }
    }
}
