using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// UI管理器
    /// </summary>
    public class UIManager : ManagerBase
    {
        /// <summary>
        /// 打开UI窗体
        /// </summary>
        /// <param name="uiFormId"></param>
        /// <param name="userData"></param>
        internal void OpenUIForm(int uiFormId, object userData = null)
        {
            Sys_UIFormEntity entity = GameEntry.DataTable.DataTableManager.Sys_UIFormDBModel.Get(uiFormId);
            if (entity == null)
            {
                Debug.Log("对应的UI窗体数据不存在，id:" + uiFormId);
                return;
            }

#if DISABLE_ASSETBUNDLE && UNITY_EDITOR

            UIFormBase formBase = null;//以后从对象池中获取

            string path = string.Format("Assets/Download/UI/UIPrefab/{0}.prefab", entity.AssetPath_Chinese);
            //加载镜像
            Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(path);
            GameObject UIObj = Object.Instantiate(obj) as GameObject;
            UIObj.transform.SetParent(GameEntry.UI.GetUIGroup(entity.UIGroupId).Group);
            UIObj.transform.localPosition = Vector3.zero;
            UIObj.transform.localScale = Vector3.one;

            formBase = UIObj.GetComponent<UIFormBase>();
            formBase.Init(uiFormId, entity.UIGroupId, entity.DisableUILayer == 1, entity.IsLock == 1, userData);
            formBase.Open(userData);
#endif
        }

        /// <summary>
        /// 关闭UI窗体
        /// </summary>
        /// <param name="formBase"></param>
        internal void CloseUIForm(UIFormBase formBase)
        {
            formBase.ToClose();
        }
    }
}
