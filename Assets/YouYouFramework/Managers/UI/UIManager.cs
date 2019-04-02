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
        /// 已经打开的UI链表
        /// </summary>
        private LinkedList<UIFormBase> m_OpenUIFormList;

        public UIManager()
        {
            m_OpenUIFormList = new LinkedList<UIFormBase>();
        }

        /// <summary>
        /// 打开UI窗体
        /// </summary>
        /// <param name="uiFormId"></param>
        /// <param name="userData"></param>
        internal void OpenUIForm(int uiFormId, object userData = null)
        {
            if (IsExists(uiFormId)) return;

            Sys_UIFormEntity entity = GameEntry.DataTable.DataTableManager.Sys_UIFormDBModel.Get(uiFormId);
            if (entity == null)
            {
                Debug.Log("对应的UI窗体数据不存在，id:" + uiFormId);
                return;
            }

#if DISABLE_ASSETBUNDLE && UNITY_EDITOR
            UIFormBase formBase = GameEntry.UI.Dequeue(uiFormId);

            if (formBase == null)
            {
                string path = string.Format("Assets/Download/UI/UIPrefab/{0}.prefab", entity.AssetPath_Chinese);
                //加载镜像
                Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(path);
                GameObject UIObj = Object.Instantiate(obj) as GameObject;
                UIObj.transform.SetParent(GameEntry.UI.GetUIGroup(entity.UIGroupId).Group);
                UIObj.transform.localPosition = Vector3.zero;
                UIObj.transform.localScale = Vector3.one;

                formBase = UIObj.GetComponent<UIFormBase>();
                formBase.Init(uiFormId, entity.UIGroupId, entity.DisableUILayer == 1, entity.IsLock == 1, userData);
            }
            else
            {
                formBase.gameObject.SetActive(true);
                formBase.Open(userData);
            }
#endif
            m_OpenUIFormList.AddLast(formBase);
        }

        /// <summary>
        /// 检测UI是否已经打开
        /// </summary>
        /// <param name="uiformId"></param>
        /// <returns></returns>
        public bool IsExists(int uiformId)
        {
            for (LinkedListNode<UIFormBase> curr = m_OpenUIFormList.First; curr != null; curr = curr.Next)
            {
                if (curr.Value.UIFormId == uiformId)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 关闭UI窗体(根据UIFormBase)
        /// </summary>
        /// <param name="formBase"></param>
        internal void CloseUIForm(UIFormBase formBase)
        {
            m_OpenUIFormList.Remove(formBase);
            formBase.ToClose();
        }

        /// <summary>
        /// 关闭UI窗体(根据UIFormId)
        /// </summary>
        /// <param name="formBase"></param>
        internal void CloseUIForm(int uiformId)
        {
            for (LinkedListNode<UIFormBase> curr = m_OpenUIFormList.First; curr != null; curr = curr.Next)
            {
                if (curr.Value.UIFormId == uiformId)
                {
                    CloseUIForm(curr.Value);
                    break;
                }
            }
        }
    }
}
