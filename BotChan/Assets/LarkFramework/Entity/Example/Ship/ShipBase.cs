using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace LarkFramework.Entity.Example
{
    public class ShipBase : EntityObject
    {
        private Vector3 m_pos;

        public void Create(Vector3 pos)
        {
            m_pos = pos;

            ViewFactory.CreateView("ship01", "ship01", this);
        }

        protected override void Release()
        {
            ViewFactory.ReleaseView(this);
        }

        public override Vector3 Position()
        {
            return m_pos;
        }
    }
}
