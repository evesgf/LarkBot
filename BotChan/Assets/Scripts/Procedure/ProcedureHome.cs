using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LarkFramework.FSM;
using LarkFramework.Procedure;
using LarkFramework.UI;

namespace Project
{
    public class ProcedureHome: ProcedureBase
    {
        protected internal override void OnEnter(IFSM<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            //加载UI
            UIManager.Instance.OpenPage(UIDef.MainPage);
        }

        protected internal override void OnUpdate(IFSM<ProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);

        }

        protected internal override void OnLeave(IFSM<ProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

            //关闭UI
        }
    }
}
