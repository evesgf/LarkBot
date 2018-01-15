using LarkFramework.Module;
using LarkFramework.Tick;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LarkFramework.FSM
{
    public class FSMManager: ServiceModule<FSMManager>,IFSMManager
    {
        public const string LOG_TAG = "FSMManager";

        private readonly Dictionary<string, FSMBase> m_Fsms;
        private readonly List<FSMBase> m_TempFsms;

        public FSMManager()
        {
            m_Fsms = new Dictionary<string, FSMBase>();
            m_TempFsms = new List<FSMBase>();
        }

        /// <summary>
        /// 初始化操作
        /// </summary>
        public void Init()
        {
            CheckSingleton();

            m_Fsms.Clear();
            m_TempFsms.Clear();

            TickComponent.Instance.onUpdate += Update;
        }

        /// <summary>
        /// 获取有限状态机数量。
        /// </summary>
        public int Count
        {
            get
            {
                return m_Fsms.Count;
            }
        }

        /// <summary>
        /// 有限状态机管理器轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        internal void Update(float elapseSeconds, float realElapseSeconds)
        {
            m_TempFsms.Clear();
            if (m_Fsms.Count <= 0)
            {
                return;
            }

            foreach (KeyValuePair<string, FSMBase> fsm in m_Fsms)
            {
                m_TempFsms.Add(fsm.Value);
            }

            foreach (FSMBase fsm in m_TempFsms)
            {
                if (fsm.IsDestroyed)
                {
                    continue;
                }

                //fsm.Update(elapseSeconds, realElapseSeconds);
                fsm.Update(elapseSeconds,realElapseSeconds);
            }
        }

        /// <summary>
        /// 关闭并清理有限状态机管理器。
        /// </summary>
        internal void Shutdown()
        {
            foreach (KeyValuePair<string, FSMBase> fsm in m_Fsms)
            {
                fsm.Value.Shutdown();
            }

            m_Fsms.Clear();
            m_TempFsms.Clear();
        }

        /// <summary>
        /// 检查是否存在有限状态机。
        /// </summary>
        /// <typeparam name="T">有限状态机持有者类型。</typeparam>
        /// <returns>是否存在有限状态机。</returns>
        public bool HasFsm<T>() where T : class
        {
            return HasFsm<T>(string.Empty);
        }

        /// <summary>
        /// 检查是否存在有限状态机。
        /// </summary>
        /// <typeparam name="T">有限状态机持有者类型。</typeparam>
        /// <param name="name">有限状态机名称。</param>
        /// <returns>是否存在有限状态机。</returns>
        public bool HasFsm<T>(string name) where T : class
        {
            return m_Fsms.ContainsKey(name);
        }

        /// <summary>
        /// 获取有限状态机。
        /// </summary>
        /// <typeparam name="T">有限状态机持有者类型。</typeparam>
        /// <returns>要获取的有限状态机。</returns>
        public IFSM<T> GetFsm<T>() where T : class
        {
            return GetFsm<T>(string.Empty);
        }

        /// <summary>
        /// 获取有限状态机。
        /// </summary>
        /// <typeparam name="T">有限状态机持有者类型。</typeparam>
        /// <param name="name">有限状态机名称。</param>
        /// <returns>要获取的有限状态机。</returns>
        public IFSM<T> GetFsm<T>(string name) where T : class
        {
            FSMBase fsm = null;
            if (m_Fsms.TryGetValue(name, out fsm))
            {
                return (IFSM<T>)fsm;
            }

            return null;
        }

        /// <summary>
        /// 获取所有有限状态机。
        /// </summary>
        /// <returns>所有有限状态机。</returns>
        public FSMBase[] GetAllFsms()
        {       
            int index = 0;
            FSMBase[] fsms = new FSMBase[m_Fsms.Count];
            foreach (KeyValuePair<string, FSMBase> fsm in m_Fsms)
            {
                fsms[index++] = fsm.Value;
            }

            return fsms;
        }

        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <typeparam name="T">有限状态机持有者类型。</typeparam>
        /// <param name="owner">有限状态机持有者。</param>
        /// <param name="states">有限状态机状态集合。</param>
        /// <returns>要创建的有限状态机。</returns>
        public IFSM<T> CreateFsm<T>(T owner, params FSMState<T>[] states) where T : class
        {
            return CreateFsm(string.Empty, owner, states);
        }

        /// <summary>
        /// 创建有限状态机。
        /// </summary>
        /// <typeparam name="T">有限状态机持有者类型。</typeparam>
        /// <param name="name">有限状态机名称。</param>
        /// <param name="owner">有限状态机持有者。</param>
        /// <param name="states">有限状态机状态集合。</param>
        /// <returns>要创建的有限状态机。</returns>
        public IFSM<T> CreateFsm<T>(string name, T owner, params FSMState<T>[] states) where T : class
        {
            if (HasFsm<T>(name))
            {
                throw new Exception(string.Format("Already exist FSM '{0}'.", name));
            }

            FSM<T> fsm = new FSM<T>(name, owner, states);
            m_Fsms.Add(name, fsm);
            return fsm;
        }

        /// <summary>
        /// 销毁有限状态机。
        /// </summary>
        /// <typeparam name="T">有限状态机持有者类型。</typeparam>
        /// <returns>是否销毁有限状态机成功。</returns>
        public bool DestroyFsm<T>() where T : class
        {
            return DestroyFsm<T>(string.Empty);
        }

        /// <summary>
        /// 销毁有限状态机。
        /// </summary>
        /// <typeparam name="T">有限状态机持有者类型。</typeparam>
        /// <param name="name">要销毁的有限状态机名称。</param>
        /// <returns>是否销毁有限状态机成功。</returns>
        public bool DestroyFsm<T>(string name) where T : class
        {
            //TODO:非完全名称可能会出现重名
            //string fullName = Utility.Text.GetFullName<T>(name);
            string fullName = name;
            FSMBase fsm = null;
            if (m_Fsms.TryGetValue(fullName, out fsm))
            {
                fsm.Shutdown();
                return m_Fsms.Remove(fullName);
            }

            return false;
        }

        /// <summary>
        /// 销毁有限状态机。
        /// </summary>
        /// <typeparam name="T">有限状态机持有者类型。</typeparam>
        /// <param name="fsm">要销毁的有限状态机。</param>
        /// <returns>是否销毁有限状态机成功。</returns>
        public bool DestroyFsm<T>(IFSM<T> fsm) where T : class
        {
            if (fsm == null)
            {
                throw new Exception("FSM is invalid.");
            }

            return DestroyFsm<T>(fsm.Name);
        }
    }
}
