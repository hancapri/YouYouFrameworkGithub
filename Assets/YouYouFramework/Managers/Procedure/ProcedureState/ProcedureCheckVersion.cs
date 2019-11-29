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
            GameEntry.Log(LogCategory.Procedure, "OnEnable ProcedureCheckVersion");
            GameEntry.Resource.InitStreamingAssetsBundleInfo();
            //GameEntry.Procedure.ChangeState(ProcedureState.Preload);
        }

        public override void OnLeave()
        {
            base.OnLeave();
            GameEntry.Log(LogCategory.Procedure, "OnLeave ProcedureCheckVersion");
        }
    }
}
