using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 预加载流程
    /// </summary>
    public class ProcedurePreload : ProcedureBase
    {
        public override void OnEnable()
        {
            base.OnEnable();
            GameEntry.Event.CommonEvent.AddEventListener(SysEventId.LoadOneDataTableComplete,OnLoadOneDataTableComplete);
            GameEntry.Event.CommonEvent.AddEventListener(SysEventId.LoadDataTableComplete, OnLoadDataTableComplete);
            GameEntry.DataTable.LoadDataTableAsync();
            
        }

        public override void OnUpdate()
        {
            base.OnUpdate();
        }

        public override void OnLeave()
        {
            base.OnLeave();
            GameEntry.Event.CommonEvent.RemoveEventListener(SysEventId.LoadOneDataTableComplete, OnLoadOneDataTableComplete);
            GameEntry.Event.CommonEvent.RemoveEventListener(SysEventId.LoadDataTableComplete, OnLoadDataTableComplete);
        }

        private void OnLoadOneDataTableComplete(object param)
        {
            string name = ((VarString)param).Value;
            Debug.Log("加载单一表完毕："+ name);
        }

        private void OnLoadDataTableComplete(object param)
        {
            Debug.Log("加载所有表完毕");
            List<Sys_UIFormEntity> lst = GameEntry.DataTable.DataTableManager.Sys_UIFormDBModel.GetList();
            foreach (var item in lst)
            {
                Debug.Log(item.Desc);
                Debug.Log(item.AssetPath_Chinese);
            }
        }
    }
}
