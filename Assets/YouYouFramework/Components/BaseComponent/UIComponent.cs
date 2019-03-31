using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace YouYouFramework
{
    /// <summary>
    /// UI组件
    /// </summary>
    public class UIComponent : YouYouBaseComponent, IUpdateComponent
    {
        [Header("标准分辨率宽度")]
        [SerializeField]
        private int m_StandardWidth = 1280;

        [Header("标准分辨率高度")]
        [SerializeField]
        private int m_StandardHeight = 720;

        [Header("UI摄像机")]
        public Camera UICamera;

        [Header("根画布的缩放")]
        [SerializeField]
        private CanvasScaler m_UIRootCanvasScaler;
        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);
        }

        /// <summary>
        /// 自动缩放
        /// </summary>
        public void AutoCanvasScaler()
        {
            float standard = m_StandardWidth / (float)m_StandardHeight;
            float currScreen = Screen.width / (float)Screen.height;
            if (currScreen > standard)
            {
                m_UIRootCanvasScaler.matchWidthOrHeight = 0;
            }
            else
            {
                m_UIRootCanvasScaler.matchWidthOrHeight = standard - currScreen;
            }
        }

        /// <summary>
        /// 设置缩放为1
        /// </summary>
        public void SetCanvasScaler()
        {
            m_UIRootCanvasScaler.matchWidthOrHeight = 1;
        }

        public void OnUpdate()
        {
            //Debug.Log("UI组件的Update方法");
        }

        public override void Shutdown()
        {

        }
    }
}
