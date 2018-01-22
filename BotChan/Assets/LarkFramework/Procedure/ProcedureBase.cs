using LarkFramework.FSM;
using LarkFramework.Tick;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LarkFramework.Procedure
{
    /// <summary>
    /// 流程基类
    /// </summary>
    public abstract class ProcedureBase : FSMState<ProcedureManager>
    {
        /// <summary>
        /// 状态初始化时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        protected internal override void OnInit(IFSM<ProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
        }

        /// <summary>
        /// 进入状态时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        protected internal override void OnEnter(IFSM<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Debuger.Log("Enter '{0}' procedure.", GetType().FullName);
        }

        /// <summary>
        /// 状态轮询时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        /// <param name="elapseSeconds">逻辑流逝时间，以秒为单位。</param>
        /// <param name="realElapseSeconds">真实流逝时间，以秒为单位。</param>
        protected internal override void OnUpdate(IFSM<ProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
        }

        /// <summary>
        /// 离开状态时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        /// <param name="isShutdown">是否是关闭状态机时触发。</param>
        protected internal override void OnLeave(IFSM<ProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            Debuger.Log("Leave '{0}' procedure.", GetType().FullName);
        }

        /// <summary>
        /// 状态销毁时调用。
        /// </summary>
        /// <param name="procedureOwner">流程持有者。</param>
        protected internal override void OnDestroy(IFSM<ProcedureManager> procedureOwner)
        {
            base.OnDestroy(procedureOwner);
        }
    }
}
