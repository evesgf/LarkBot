using LarkFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LarkFramework.Tick
{
    [DisallowMultipleComponent]
    [AddComponentMenu("LarkFramework/TickComponent")]
    public class TickComponent : SingletonMono<TickComponent>
    {
        #region 全局生命周期回调
        public delegate void LifeCircleCallback();
        public delegate void LifeUpdateCircleCallback(float elapseSeconds, float realElapseSeconds);

        public LifeUpdateCircleCallback onUpdate = null;
        public LifeUpdateCircleCallback onFixedUpdate = null;
        public LifeUpdateCircleCallback onLatedUpdate = null;
        public LifeCircleCallback onGUI = null;
        public LifeCircleCallback onDestroy = null;
        public LifeCircleCallback onApplicationQuit = null;

        void Update()
        {
            if (this.onUpdate != null)
                this.onUpdate(Time.deltaTime,Time.realtimeSinceStartup);
        }

        void FixedUpdate()
        {
            if (this.onFixedUpdate != null)
                this.onFixedUpdate(Time.deltaTime, Time.realtimeSinceStartup);

        }

        void LatedUpdate()
        {
            if (this.onLatedUpdate != null)
                this.onLatedUpdate(Time.deltaTime, Time.realtimeSinceStartup);
        }

        void OnGUI()
        {
            if (this.onGUI != null)
                this.onGUI();
        }

        protected void OnDestroy()
        {
            if (this.onDestroy != null)
                this.onDestroy();
        }

        void OnApplicationQuit()
        {
            if (this.onApplicationQuit != null)
                this.onApplicationQuit();
        }
        #endregion
    }
}
