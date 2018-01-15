using LarkFramework.Module;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LarkFramework.Tick
{
    public class TickManager : ServiceModule<TickManager>
    {
        public const string LOG_TAG = "TickManager";

        public TickComponent m_TickComponent;

        public void Init()
        {
            m_TickComponent = TickComponent.Create();
        }
    }
}