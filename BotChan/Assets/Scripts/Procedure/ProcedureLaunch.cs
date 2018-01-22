using System.Collections;
using System.Collections.Generic;
using LarkFramework.FSM;
using LarkFramework.Procedure;
using UnityEngine;

namespace Project
{
    public class ProcedureLaunch : ProcedureBase
    {
        protected internal override void OnEnter(IFSM<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            //构建信息

            //语言配置

            //资源配置

            //画质配置

            //声音配置
        }

        protected internal override void OnUpdate(IFSM<ProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            ProcedureManager.Instance.ChangeProcedure<ProcedureSplash>();
        }
    }
}
