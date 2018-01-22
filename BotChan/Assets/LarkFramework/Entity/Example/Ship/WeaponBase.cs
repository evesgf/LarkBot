using LarkFramework.Entity;
using LarkFramework.Entity.Example;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LarkFramework.Example.Ship
{
    public class WeaponBase: EntityObject
    {
        private ShipBase m_ship;
        private Transform m_weaponRoot;

        public void Create(Transform weaponRoot)
        {
            m_weaponRoot = weaponRoot;

            ViewFactory.CreateView("Weapon01", "Weapon01", this, weaponRoot);
        }

        protected override void Release()
        {
            ViewFactory.ReleaseView(this);
        }

        public override Transform Root()
        {
            return m_weaponRoot;
        }
    }
}
