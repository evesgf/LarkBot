using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LarkFramework.Module
{
    public class ModuleManager : ServiceModule<ModuleManager>
    {
        class MessageObject
        {
            public string target;
            public string msg;
            public object[] args;
        }

        private Dictionary<string, BusinessModule> m_mapMdules;

        private Dictionary<string, EventTable> m_mapPreListenEvents;

        private Dictionary<string, List<MessageObject>> m_mapCacheMessage;

        private string m_domain;

        public ModuleManager()
        {
            m_mapMdules = new Dictionary<string, BusinessModule>();
            m_mapPreListenEvents = new Dictionary<string, EventTable>();
            m_mapCacheMessage = new Dictionary<string, List<MessageObject>>();
        }

        public void Init(string domain = "LarkFramework.Module")
        {
            CheckSingleton();
            m_domain = domain;
        }

        public T CreateModlue<T>(object args = null) where T : BusinessModule
        {
            return (T)CreateModule(typeof(T).Name, args);
        }

        public BusinessModule CreateModule(string name, object args = null)
        {
            if (m_mapMdules.ContainsKey(name))
            {
                return m_mapMdules[name];
            }

            BusinessModule module = null;
            Type type = Type.GetType(m_domain + "." + name);
            if (type != null)
            {
                module = Activator.CreateInstance(type) as BusinessModule;
            }
            else
            {
                //module = new LuaModule(name);
                return null;
            }

            m_mapMdules.Add(name, module);

            //处理预监听的事件
            if (m_mapPreListenEvents.ContainsKey(name))
            {
                EventTable tblEvent = m_mapPreListenEvents[name];
                m_mapPreListenEvents.Remove(name);

                module.SetEventTable(tblEvent);
            }

            module.Create(args);

            //处理缓存的消息
            if (m_mapCacheMessage.ContainsKey(name))
            {
                List<MessageObject> list = m_mapCacheMessage[name];
                for (int i = 0; i < list.Count; i++)
                {
                    MessageObject msgObj = list[i];
                    module.HandleMessage(msgObj.msg, msgObj.args);
                }
                m_mapCacheMessage.Remove(name);
            }

            return module;
        }

        public void ReleaseModule(BusinessModule module)
        {
            if (module != null)
            {
                m_mapMdules.ContainsKey(module.Name);
                m_mapMdules.Remove(module.Name);
                module.Release();
            }
            else
            {

            }
        }

        public void ReleaseAll()
        {
            foreach (var item in m_mapPreListenEvents)
            {
                item.Value.Clear();
            }

            m_mapPreListenEvents.Clear();

            m_mapCacheMessage.Clear();

            foreach (var item in m_mapMdules)
            {
                item.Value.Release();
            }
            m_mapMdules.Clear();
        }

        //----------------------------------------------------

        public T GetModule<T>() where T : BusinessModule
        {
            return (T)GetModule(typeof(T).Name);
        }

        public BusinessModule GetModule(string name)
        {
            if (m_mapMdules.ContainsKey(name))
            {
                return m_mapMdules[name];
            }
            return null;
        }

        //-----------------------------------------------------

        public void SendMessage(string target, string msg, params object[] args)
        {
            BusinessModule module = GetModule(target);
            if (module != null)
            {
                module.HandleMessage(msg, args);
            }
            else
            {
                //模块还未创建，将消息缓存
                List<MessageObject> list = GetCacheMessageList(target);
                MessageObject msgObj = new MessageObject();
                list.Add(msgObj);

                msgObj.target = target;
                msgObj.msg = msg;
                msgObj.args = args;
            }
        }

        private List<MessageObject> GetCacheMessageList(string target)
        {
            List<MessageObject> list = null;
            if (!m_mapCacheMessage.ContainsKey(target))
            {
                list = new List<MessageObject>();
                m_mapCacheMessage.Add(target, list);
            }
            else
            {
                list = m_mapCacheMessage[target];
            }
            return list;
        }

        //----------------------------------------------------------

        public ModuleEvent Event(string target, string type)
        {
            ModuleEvent evt = null;
            BusinessModule module = GetModule(target);
            if (module != null)
            {
                evt = module.Event(type);
            }
            else
            {
                EventTable table = GetPreListenEventTable(target);
                evt = table.GetEvent(type);
            }

            return evt;
        }

        private EventTable GetPreListenEventTable(string target)
        {
            EventTable table = null;
            if (!m_mapPreListenEvents.ContainsKey(target))
            {
                table = new EventTable();
                m_mapPreListenEvents.Add(target, table);
            }
            else
            {
                table = m_mapPreListenEvents[target];
            }
            return table;
        }
    }
}
