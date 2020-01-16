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
            GameEntry.Event.CommonEvent.AddEventListener(SysEventId.LoadLuaDataTableComplete, OnLoadLuaDataTableComplete);
#if DISABLE_ASSETBUNDLE
            GameEntry.Lua.Init();
#endif
            GameEntry.Resource.InitAssetInfo();
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
            GameEntry.Event.CommonEvent.RemoveEventListener(SysEventId.LoadLuaDataTableComplete, OnLoadLuaDataTableComplete);
        }

        private void OnLoadOneDataTableComplete(object param)
        {
            GameEntry.DataTable.DataTableManager.CurrLoadTableCount++;
            if (GameEntry.DataTable.DataTableManager.CurrLoadTableCount == GameEntry.DataTable.DataTableManager.TotalTableCount)
            {
                GameEntry.Event.CommonEvent.Dispatch(SysEventId.LoadDataTableComplete);
            }
        }

        private void OnLoadDataTableComplete(object param)
        {
            GameEntry.Log(LogCategory.Normal, "加载c#表格完毕");

#if !DISABLE_ASSETBUNDLE
            //执行Lua初始化
            GameEntry.Lua.Init();
#endif
        }

        private void OnLoadLuaDataTableComplete(object param)
        {
            GameEntry.Log(LogCategory.Normal, "加载Lua表格完毕");
            GameEntry.Procedure.ChangeState(ProcedureState.LogOn);
        }
    }
}
