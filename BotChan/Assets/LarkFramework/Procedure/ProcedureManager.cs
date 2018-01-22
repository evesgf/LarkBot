using LarkFramework.FSM;
using LarkFramework.Module;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace LarkFramework.Procedure
{
    public class ProcedureManager : ServiceModule<ProcedureManager>
    {
        public const string LOG_TAG = "ProcedureManager";

        /// <summary>
        /// 流程状态机
        /// </summary>
        private IFSM<ProcedureManager> m_ProcedureFSM;

        public ProcedureManager()
        {
            m_ProcedureFSM = null;
        }

        /// <summary>
        /// 初始化流程管理器。
        /// </summary>
        /// <param name="fsmManager">有限状态机管理器。</param>
        /// <param name="procedures">流程管理器包含的流程。</param>
        public void Init(ProcedureBase[] procedures)
        {
            m_ProcedureFSM = FSMManager.Instance.CreateFsm("ProcedureFSM", this, procedures);
        }

        /// <summary>
        /// 获取当前流程。
        /// </summary>
        public ProcedureBase CurrentProcedure
        {
            get
            {
                if (m_ProcedureFSM == null)
                {
                    throw new Exception("You must initialize procedure first.");
                }

                return (ProcedureBase)m_ProcedureFSM.CurrentState;
            }
        }

        /// <summary>
        /// 获取当前流程持续时间。
        /// </summary>
        public float CurrentProcedureTime
        {
            get
            {
                if (m_ProcedureFSM == null)
                {
                    throw new Exception("You must initialize procedure first.");
                }

                return m_ProcedureFSM.CurrentStateTime;
            }
        }

        /// <summary>
        /// 流程管理器轮询。
        /// </summary>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        internal void Update(float elapseSeconds, float realElapseSeconds)
        {

        }

        /// <summary>
        /// 开始流程。
        /// </summary>
        /// <typeparam name="T">要开始的流程类型。</typeparam>
        public void StartProcedure<T>() where T : ProcedureBase
        {
            if (m_ProcedureFSM == null)
            {
                throw new Exception("You must initialize procedure first.");
            }

            m_ProcedureFSM.Start<T>();
        }

        /// <summary>
        /// 开始流程。
        /// </summary>
        /// <param name="procedureType">要开始的流程类型。</param>
        public void StartProcedure(Type procedureType)
        {
            if (m_ProcedureFSM == null)
            {
                throw new Exception("You must initialize procedure first.");
            }

            m_ProcedureFSM.Start(procedureType);
        }

        /// <summary>
        /// 是否存在流程。
        /// </summary>
        /// <typeparam name="T">要检查的流程类型。</typeparam>
        /// <returns>是否存在流程。</returns>
        public bool HasProcedure<T>() where T : ProcedureBase
        {
            if (m_ProcedureFSM == null)
            {
                throw new Exception("You must initialize procedure first.");
            }

            return m_ProcedureFSM.HasState<T>();
        }

        /// <summary>
        /// 是否存在流程。
        /// </summary>
        /// <param name="procedureType">要检查的流程类型。</param>
        /// <returns>是否存在流程。</returns>
        public bool HasProcedure(Type procedureType)
        {
            if (m_ProcedureFSM == null)
            {
                throw new Exception("You must initialize procedure first.");
            }

            return m_ProcedureFSM.HasState(procedureType);
        }

        /// <summary>
        /// 获取流程。
        /// </summary>
        /// <typeparam name="T">要获取的流程类型。</typeparam>
        /// <returns>要获取的流程。</returns>
        public ProcedureBase GetProcedure<T>() where T : ProcedureBase
        {
            if (m_ProcedureFSM == null)
            {
                throw new Exception("You must initialize procedure first.");
            }

            return m_ProcedureFSM.GetState<T>();
        }

        /// <summary>
        /// 获取流程。
        /// </summary>
        /// <param name="procedureType">要获取的流程类型。</param>
        /// <returns>要获取的流程。</returns>
        public ProcedureBase GetProcedure(Type procedureType)
        {
            if (m_ProcedureFSM == null)
            {
                throw new Exception("You must initialize procedure first.");
            }

            return (ProcedureBase)m_ProcedureFSM.GetState(procedureType);
        }

        /// <summary>
        /// 切换当前过程状态。
        /// </summary>
        /// <typeparam name="TProcedure">要切换到的有限状态机状态类型。</typeparam>
        public void ChangeProcedure<TProcedure>() where TProcedure : ProcedureBase
        {
            m_ProcedureFSM.ChangeState<TProcedure>();
        }
    }
}
