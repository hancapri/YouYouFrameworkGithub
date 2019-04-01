using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 检查更新流程
    /// </summary>
    public class ProcedureCheckVersion : ProcedureBase
    {
        public override void OnEnable()
        {
            base.OnEnable();
            Debug.Log("OnEnable ProcedureCheckVersion");
            GameEntry.Procedure.ChangeState(ProcedureState.Preload);
        }
    }
}
