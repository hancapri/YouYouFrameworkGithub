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
            GameEntry.Log(LogCategory.Procedure, "OnEnable ProcedurePreload");
            GameEntry.Event.CommonEvent.AddEventListener(SysEventId.LoadOneDataTableComplete,OnLoadOneDataTableComplete);
            GameEntry.Event.CommonEvent.AddEventListener(SysEventId.LoadDataTableComplete, OnLoadDataTableComplete);
            GameEntry.DataTable.LoadDataTableAsync();
            GameEntry.Resource.InitAssetInfo();
            
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
            string name = (string)param;
            Debug.Log("加载单一表完毕："+ name);
            GameEntry.DataTable.DataTableManager.CurrLoadTableCount++;
            if (GameEntry.DataTable.DataTableManager.CurrLoadTableCount == GameEntry.DataTable.DataTableManager.TotalTableCount)
            {
                GameEntry.Event.CommonEvent.Dispatch(SysEventId.LoadDataTableComplete);
            }
        }

        private void OnLoadDataTableComplete(object param)
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
