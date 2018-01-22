using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LarkFramework.FSM;
using LarkFramework.Procedure;

namespace Project
{
    public class ProcedureSplash: ProcedureBase
    {
        protected internal override void OnEnter(IFSM<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            //加载Splash动画
        }

        protected internal override void OnUpdate(IFSM<ProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

            //播放Splash动画

            //检查版本

            //切换场景
            ProcedureManager.Instance.ChangeProcedure<ProcedureChageScene>();
        }

        protected internal override void OnLeave(IFSM<ProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

        }
    }
}
