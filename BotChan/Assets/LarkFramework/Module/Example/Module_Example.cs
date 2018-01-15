﻿using UnityEngine;

namespace LarkFramework.Module.Example
{

    public class Module_Example : MonoBehaviour
    {

        void Start()
        {
            Init();
        }

        public void Init()
        {
            Debuger.EnableLog = true;

            ModuleC.Instance.Init();
            ModuleManager.Instance.Init("LarkFramework.Module.Example");

            ModuleManager.Instance.CreateModule("ModuleA");
            ModuleManager.Instance.CreateModule("ModuleB");

            //业务层模块之间，通过Message进行通讯
            ModuleManager.Instance.SendMessage("ModuleB", "MessageFromA_1", 1, 2, 3);
            ModuleManager.Instance.SendMessage("ModuleB", "MessageFromA_2", "abc", 123);

            //业务层模块之间，通过Event进行通讯 
            ModuleManager.Instance.Event("ModuleB", "onModuleEventB").AddListener(OnModuleEventB);

            //业务层调用服务层，通过事件监听回调
            ModuleC.Instance.onEvent.AddListener(OnModuleEventC);
            ModuleC.Instance.DoSomething();

            //全局事件
            GloableEvent.onLogin.AddListener(OnLogin);
        }

        private void OnModuleEventC(object args)
        {
            this.Log("OnModuleEventC() args:{0}", args);
        }

        private void OnModuleEventB(object args)
        {
            this.Log("OnModuleEventB() args:{0}", args);
        }

        private void OnLogin(bool args)
        {
            this.Log("OnLogin() args:{0}", args);
        }
    }

    public class ModuleA : BusinessModule
    {
        public override void Create(object args = null)
        {
            base.Create(args);

            //业务层模块之间，通过message进行通讯
            ModuleManager.Instance.SendMessage("ModuleB", "MessageFromA_1", 1, 2, 3);
            ModuleManager.Instance.SendMessage("ModuleB", "MessageFromA_2", "abc", 123);

            //业务层模块之间，通过Event进行通讯
            ModuleManager.Instance.Event("ModuleB", "onModuleEventB").AddListener(OnModlueEventB);

            //业务层调用服务层，通过事件监听回调
            ModuleC.Instance.onEvent.AddListener(OnModlueEventC);
            ModuleC.Instance.DoSomething();

            //全局事件
            GloableEvent.onLogin.AddListener(OnLogin);
        }

        private void OnModlueEventB(object args)
        {
            this.Log("OnModuleEventB() args{0}", args);
        }
        private void OnModlueEventC(object args)
        {
            this.Log("OnModlueEventC() args{0}", args);
        }

        private void OnLogin(bool args)
        {
            this.Log("OnLogin() args:{0}", args);
        }
    }

    public class ModuleB : BusinessModule
    {
        public ModuleEvent onModlueEventB
        {
            get
            {
                return Event("onModuleEventB");
            }
        }

        public override void Create(object args = null)
        {
            base.Create(args);
            onModlueEventB.Invoke("aaaaaaaaaaaaaaaaa");
        }

        protected void MessageFromA_2(string args1, int args2)
        {
            this.Log("MessageFromA_2() args:{0},{1}", args1, args2);
        }

        protected override void OnModlueMessage(string msg, object[] args)
        {
            base.OnModlueMessage(msg, args);
            this.Log("OnModlueMessage() msg:{0},args:{1},{2},{3}", msg, args[0], args[1], args[2]);
        }
    }

    public class ModuleC : ServiceModule<ModuleC>
    {
        public ModuleEvent onEvent = new ModuleEvent();

        public void Init()
        {

        }

        public void DoSomething()
        {
            onEvent.Invoke(null);
        }
    }

}