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
        internal void OpenUIForm(int uiFormId, object userData = null,BaseAction<UIFormBase> onOpen = null)
        {
            if (IsExists(uiFormId)) return;

            Sys_UIFormEntity entity = GameEntry.DataTable.DataTableManager.Sys_UIFormDBModel.Get(uiFormId);
            if (entity == null)
            {
                Debug.Log("对应的UI窗体数据不存在，id:" + uiFormId);
                return;
            }

            UIFormBase formBase = GameEntry.UI.Dequeue(uiFormId);

            if (formBase == null)
            {
                //TODO:异步加载UI需要时间 过滤正在加载的UI

                string assetPath = string.Empty;
                switch (GameEntry.Localization.CurrLanguage)
                {
                    case LocalizationComponent.LanguageEnum.Chinese:
                        assetPath = entity.AssetPath_Chinese;
                        break;
                    case LocalizationComponent.LanguageEnum.English:
                        assetPath = entity.AssetPath_English;
                        break;
                }
                LoadUIAsset(assetPath,(ResourceEntity resourceEntity) =>
                {
                    GameObject UIObj = Object.Instantiate((Object)resourceEntity.Target) as GameObject;

                    //把克隆出来的资源加入实例对象资源池
                    GameEntry.Pool.RegisterInstanceResource(UIObj.GetInstanceID(),resourceEntity);

                    UIObj.transform.SetParent(GameEntry.UI.GetUIGroup(entity.UIGroupId).Group);
                    UIObj.transform.localPosition = Vector3.zero;
                    UIObj.transform.localScale = Vector3.one;

                    formBase = UIObj.GetComponent<UIFormBase>();
                    formBase.Init(uiFormId, entity.UIGroupId, entity.DisableUILayer == 1, entity.IsLock == 1, userData);
                    m_OpenUIFormList.AddLast(formBase);

                    if (onOpen != null)
                    {
                        onOpen(formBase);
                    }
                });
            }
            else
            {
                formBase.gameObject.SetActive(true);
                formBase.Open(userData);
                m_OpenUIFormList.AddLast(formBase);

                if (onOpen != null)
                {
                    onOpen(formBase);
                }
            }
        }

        /// <summary>
        /// 加载UI资源
        /// </summary>
        /// <param name="assetPath"></param>
        /// <param name="onComplete"></param>
        private void LoadUIAsset(string assetPath,BaseAction<ResourceEntity> onComplete)
        {
#if DISABLE_ASSETBUNDLE && UNITY_EDITOR
            string path = string.Format("Assets/Download/UI/UIPrefab/{0}.prefab", assetPath);
            //加载镜像
            Object obj = UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(path);
            if (onComplete != null)
            {
                onComplete(obj);
            }
#else
            GameEntry.Resource.ResourceLoaderManager.LoadMainAsset(AssetCategory.UIPrefab, string.Format("Assets/Download/UI/UIPrefab/{0}.prefab", assetPath),(ResourceEntity entity)=>
            {
                if (onComplete != null)
                {
                    onComplete(entity);
                }
            });
#endif
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
