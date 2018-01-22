using LarkFramework.FSM;
using LarkFramework.Module;
using LarkFramework.Tick;
using UnityEngine;

namespace LarkFramework.Procedure.Example
{
    public class Procedure_Example : MonoBehaviour
    {
        void Start()
        {
            ModuleManager.Instance.Init("LarkFramework.Procedure.Example");

            TickManager.Instance.Init();

            FSMManager.Instance.Init();

            //ProcedureManager.Instance.Init(new ProcedureBase[]{ new ProcedureA(), new ProcedureB(),new ProcedureC()});

            //ProcedureManager.Instance.StartProcedure<ProcedureA>();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                ProcedureManager.Instance.ChangeProcedure<ProcedureA>();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                ProcedureManager.Instance.ChangeProcedure<ProcedureB>();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                ProcedureManager.Instance.ChangeProcedure<ProcedureC>();
            }
            Debug.Log(ProcedureManager.Instance.CurrentProcedure);
        }
    }

    public class ProcedureA : ProcedureBase
    {
        protected internal override void OnEnter(IFSM<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Debug.Log("ProcedureA OnEnter()");
        }

        protected internal override void OnInit(IFSM<ProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
            Debug.Log("ProcedureA OnInit()");
        }

        protected internal override void OnUpdate(IFSM<ProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            Debug.Log("ProcedureA OnUpdate()");
        }

        protected internal override void OnLeave(IFSM<ProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            Debug.Log("ProcedureA OnLeave()");
        }

        protected internal override void OnDestroy(IFSM<ProcedureManager> procedureOwner)
        {
            base.OnDestroy(procedureOwner);
            Debug.Log("ProcedureA OnDestroy()");
        }
    }

    public class ProcedureB : ProcedureBase
    {
        protected internal override void OnEnter(IFSM<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Debug.Log("ProcedureB OnEnter()");
        }

        protected internal override void OnInit(IFSM<ProcedureManager> procedureOwner)
        {
            base.OnInit(procedureOwner);
            Debug.Log("ProcedureB OnInit()");
        }

        protected internal override void OnUpdate(IFSM<ProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            Debug.Log("ProcedureB OnUpdate()");
        }

        protected internal override void OnLeave(IFSM<ProcedureManager> procedureOwner, bool isShutdown)
        {
            base.OnLeave(procedureOwner, isShutdown);
            Debug.Log("ProcedureB OnLeave()");
        }

        protected internal override void OnDestroy(IFSM<ProcedureManager> procedureOwner)
        {
            base.OnDestroy(procedureOwner);
            Debug.Log("ProcedureB OnDestroy()");
        }
    }

    public class ProcedureC : ProcedureBase
    {
        protected internal override void OnEnter(IFSM<ProcedureManager> procedureOwner)
        {
            base.OnEnter(procedureOwner);
            Debug.Log("ProcedureC OnEnter()");
        }

        protected internal override void OnUpdate(IFSM<ProcedureManager> procedureOwner, float elapseSeconds, float realElapseSeconds)
        {
            base.OnUpdate(procedureOwner, elapseSeconds, realElapseSeconds);
            Debug.Log("ProcedureC OnUpdate()");

            ProcedureManager.Instance.ChangeProcedure<ProcedureA>();
        }
    }
}
