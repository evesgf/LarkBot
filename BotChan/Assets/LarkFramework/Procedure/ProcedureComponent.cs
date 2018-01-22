using System;
using LarkFramework.Runtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LarkFramework.Utils;

namespace LarkFramework.Procedure
{
    /// <summary>
    /// 界面组件。
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("Lark Framework/Procedure Component")]
    public class ProcedureComponent : ComponentBase
    {
        public const string LOG_TAG = "ProcedureComponent";

        private ProcedureManager m_ProcedureManager = null;
        private ProcedureBase m_EntranceProcedure = null;

        [SerializeField]
        private string[] m_AvailableProcedureTypeNames = null;

        [SerializeField]
        private string m_EntranceProcedureTypeName = null;

        /// <summary>
        /// 获取当前流程。
        /// </summary>
        public ProcedureBase CurrentProcedure
        {
            get
            {
                return m_ProcedureManager.CurrentProcedure;
            }
        }

        /// <summary>
        /// 当前场景中寻找ProcedureComponent对象
        /// </summary>
        /// <returns></returns>
        public static GameObject FindProcedureComponent()
        {
            GameObject root = GameObject.Find("ProcedureComponent");
            if (root != null && root.GetComponent<ProcedureComponent>() != null)
            {
                return root;
            }

            Debuger.LogError(LOG_TAG, "FindProcedureComponent() ProcedureComponent Is Not Exist!!!");
            return root;
        }

        private IEnumerator Start()
        {
            m_ProcedureManager = ProcedureManager.Instance;

            //实例化流程
            ProcedureBase[] procedures = new ProcedureBase[m_AvailableProcedureTypeNames.Length];
            for (int i = 0; i < procedures.Length; i++)
            {
                Type procedureType = Utility.Assembly.GetTypeWithinLoadedAssemblies(m_AvailableProcedureTypeNames[i]);

                if (procedureType == null)
                {
                    Debuger.LogError("Can not find procedure type '{0}'.", m_AvailableProcedureTypeNames[i]);
                    yield break;
                }

                procedures[i] = (ProcedureBase)Activator.CreateInstance(procedureType);
                if (procedures[i] == null)
                {
                    Debuger.LogError("Can not create procedure instance '{0}'.", m_AvailableProcedureTypeNames[i]);
                    yield break;
                }

                if (m_EntranceProcedureTypeName == m_AvailableProcedureTypeNames[i])
                {
                    m_EntranceProcedure = procedures[i];
                }
            }

            if (m_EntranceProcedure == null)
            {
                Debuger.LogError("Entrance procedure is invalid.");
                yield break;
            }

            m_ProcedureManager.Init(procedures);

            yield return new WaitForEndOfFrame();

            m_ProcedureManager.StartProcedure(m_EntranceProcedure.GetType());
        }
    }
}
