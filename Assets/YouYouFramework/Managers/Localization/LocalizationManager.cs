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
        private Dictionary<string, string> m_LocalizationDic;
        public LocalizationManager()
        {
            m_LocalizationDic = GameEntry.DataTable.DataTableManager.LocalizationDBModel.LocalizationDic;
        }
        public string GetString(string key, params object[] args)
        {
            string value = null;
            if (m_LocalizationDic.TryGetValue(key,out value))
            {
                return string.Format(value,args);
            }
            return value;
        }
    }
}
