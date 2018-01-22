using LarkFramework.Example.Ship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LarkFramework.Entity.Example
{
    public class Ship01: ViewObject
    {
        public Transform weaponRoot;

        [SerializeField]
        private Vector3 m_entityPosition;

        private ShipBase m_entity;

        private WeaponBase weapon;

        protected override void Create(EntityObject entity)
        {
            m_entity = entity as ShipBase;
            m_entityPosition = m_entity.Position();
            this.transform.localPosition = m_entityPosition;
            Debug.Log("Create:" + m_entity);

            weapon = EntityFactory.InstanceEntity<WeaponBase>();
            weapon.Create(weaponRoot);
        }

        protected override void Release()
        {
            m_entity = null;
        }
    }
}
