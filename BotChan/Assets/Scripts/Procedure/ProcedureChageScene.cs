using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LarkFramework.FSM;
using LarkFramework.Procedure;

namespace Project
{
    public class ProcedureChageScene: ProcedureBase
    {
        protected internal override void OnEnter(IFSM<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);

            //停止所有声音

            //隐藏所有实体

            //卸载所有场景

            //还原游戏速度

        }

        protected internal override void OnUpdate(IFSM<ProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);


        }

        protected internal override void OnLeave(IFSM<ProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);

        }

        private void OnLoadSceneSuccess()
        {

        }

        private void OnLoadSceneFailure()
        {

        }
    }
}
