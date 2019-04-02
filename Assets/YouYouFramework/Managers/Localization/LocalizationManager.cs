using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 本地化（多语言）管理器
    /// </summary>
    public class LocalizationManager : ManagerBase
    {
        public string GetString(string key, params object[] args)
        {
            string value = null;
            if (GameEntry.DataTable.DataTableManager.LocalizationDBModel.LocalizationDic.TryGetValue(key,out value))
            {
                return string.Format(value,args);
            }
            return value;
        }
    }
}
