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

        private void OnLoadOneDataTableComplete(VariableBase param)
        {
            string name = ((VarString)param).Value;
            Debug.Log("加载单一表完毕："+ name);
        }

        private void OnLoadDataTableComplete(VariableBase param)
        {
            Debug.Log("加载所有表完毕");
            List<ChapterEntity> lst = GameEntry.DataTable.DataTableManager.ChapterDBModel.GetList();
            foreach (var item in lst)
            {
                Debug.Log(item.ChapterName);
            }
        }
    }
}
