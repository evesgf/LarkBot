using LarkFramework.Module;
using LarkFramework.Tick;
using UnityEngine;

namespace LarkFramework.FSM.Example
{
    public class FSM_Example : MonoBehaviour
    {
        private IFSM<FSM_Example> fsm;

        // Use this for initialization
        void Start()
        {
            ModuleManager.Instance.Init("LarkFramework.FSM.Example");

            TickManager.Instance.Init();

            FSMManager.Instance.Init();

            fsm = FSMManager.Instance.CreateFsm("Testfsm", this, new FSMA(), new FSMB());

            fsm.Start<FSMA>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                fsm.ChangeState<FSMA>();
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                fsm.ChangeState<FSMB>();
            }
            Debug.Log(fsm.Name + "_" + fsm.CurrentState);
        }

        public class FSMA : FSMState<FSM_Example>
        {
            protected internal override void OnInit(IFSM<FSM_Example> fsm)
            {
                base.OnInit(fsm);
                Debug.Log("FSMA OnInit");
            }

            protected internal override void OnEnter(IFSM<FSM_Example> fsm)
            {
                base.OnEnter(fsm);
                Debug.Log("FSMA OnEnter");

            }

            protected internal override void OnUpdate(IFSM<FSM_Example> fsm, float elapseSeconds, float realElapseSeconds)
            {
                base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
                Debug.Log("FSMA OnUpdate");
            }

            protected internal override void OnLeave(IFSM<FSM_Example> fsm, bool isShutdown)
            {
                base.OnLeave(fsm, isShutdown);
                Debug.Log("FSMA OnLeave");
            }

            protected internal override void OnDestroy(IFSM<FSM_Example> fsm)
            {
                base.OnDestroy(fsm);
                Debug.Log("FSMA OnDestroy");
            }
        }

        public class FSMB : FSMState<FSM_Example>
        {
            protected internal override void OnInit(IFSM<FSM_Example> fsm)
            {
                base.OnInit(fsm);
                Debug.Log("FSMB OnInit");
            }

            protected internal override void OnEnter(IFSM<FSM_Example> fsm)
            {
                base.OnEnter(fsm);
                Debug.Log("FSMB OnEnter");
            }

            protected internal override void OnUpdate(IFSM<FSM_Example> fsm, float elapseSeconds, float realElapseSeconds)
            {
                base.OnUpdate(fsm, elapseSeconds, realElapseSeconds);
                Debug.Log("FSMB OnUpdate");
            }

            protected internal override void OnLeave(IFSM<FSM_Example> fsm, bool isShutdown)
            {
                base.OnLeave(fsm, isShutdown);
                Debug.Log("FSMB OnLeave");
            }

            protected internal override void OnDestroy(IFSM<FSM_Example> fsm)
            {
                base.OnDestroy(fsm);
                Debug.Log("FSMB OnDestroy");
            }
        }
    }
}
