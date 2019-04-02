using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    public class UIFormBase : MonoBehaviour
    {
        /// <summary>
        /// UI窗体编号
        /// </summary>
        public int UIFormId { get; private set; }

        /// <summary>
        /// UI分组Id
        /// </summary>
        public byte GroupId { get; private set; }

        /// <summary>
        /// 当前的画布
        /// </summary>
        public Canvas CurrCanvas { get; private set; }

        /// <summary>
        /// 关闭时间
        /// </summary>
        public float CloseTime { get; private set; }

        /// <summary>
        /// 禁用层级管理
        /// </summary>
        public bool DisableUILayer { get; private set; }

        /// <summary>
        /// 是否锁定
        /// </summary>
        public bool IsLook { get; private set; }

        /// <summary>
        /// 用户数据
        /// </summary>
        public object UserData { get; private set; }

        private void Awake()
        {
            CurrCanvas = GetComponent<Canvas>();
        }

        internal void Init(int uiFormId, byte groupId, bool disableUILayer, bool isLook, object userData)
        {
            UIFormId = uiFormId;
            GroupId = groupId;
            DisableUILayer = disableUILayer;
            IsLook = isLook;
            UserData = userData;
        }

        private void Start()
        {
            OnInit(UserData);
            Open(UserData, true);
        }

        internal void Open(object userData, bool isFormInit = false)
        {
            if (!isFormInit)
            {
                UserData = userData;
            }

            if (!DisableUILayer)
            {
                //进行层级管理,增加层级
                GameEntry.UI.SetSortingOrder(this,true);
            }

            OnOpen(userData);
        }

        /// <summary>
        /// 关闭UI
        /// </summary>
        public void Close()
        {
            GameEntry.UI.CloseUIForm(this);
        }

        public void ToClose()
        {
            if (!DisableUILayer)
            {
                //进行层级管理,减少层级
                GameEntry.UI.SetSortingOrder(this, false);
            }
            OnClose();

            CloseTime = Time.time;
            //先销毁，以后改成对象池
            GameEntry.UI.Enqueue(this);
        }

        private void OnDestroy()
        {
            OnBeforeDestroy();
        }


        protected virtual void OnInit(object userData) { }
        protected virtual void OnOpen(object userData) { }
        protected virtual void OnClose() { }
        protected virtual void OnBeforeDestroy() { }
    }
}
