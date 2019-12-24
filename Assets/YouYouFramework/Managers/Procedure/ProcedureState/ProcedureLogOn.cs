using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 登录流程
    /// </summary>
    public class ProcedureLogOn : ProcedureBase
    {
        public override void OnEnable()
        {
            base.OnEnable();
            GameEntry.Log(LogCategory.Procedure, "OnEnable ProcedureLogOn");

            GameEntry.UI.OpenUIForm(UIFormId.UI_Task);

        }

        public override void OnLeave()
        {
            GameEntry.Log(LogCategory.Procedure, "OnLeave ProcedureLogOn");
        }

    }
}
