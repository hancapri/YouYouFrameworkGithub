using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 启动流程
    /// </summary>
    public class ProcedureLaunch : ProcedureBase
    {
        public override void OnEnable()
        {
            base.OnEnable();
            Debug.Log("OnEnable ProcedureLaunch");

            //string url = GameEntry.Http.RealWebUrl + "/api/accountlaunch";
            //GameEntry.Http.SendData(url, WebGetCallBack);

            //Dictionary<string, object> dic = GameEntry.Pool.DequeueClassObject<Dictionary<string,object>>();
            //dic["ChannelId"] = 0;
            //dic["InnerVersion"] = 1001;
            //GameEntry.Http.SendData(url, WebPostCallBack, true, dic);
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
            if (Input.GetKeyDown(KeyCode.L))
            {
                GameEntry.Procedure.ChangeState(ProcedureState.CheckVersion);
            }
        }

        private void WebGetCallBack(HttpCallBackArgs args)
        {
            Debug.Log("haserror = " + args.HasError);
            Debug.Log("value = " + args.Value);
        }

        private void WebPostCallBack(HttpCallBackArgs args)
        {
            GameEntry.Procedure.ChangeState(ProcedureState.CheckVersion);
        }
    }
}
