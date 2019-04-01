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

        [Header("根画布")]
        [SerializeField]
        private Canvas m_UIRootCanvas;

        [Header("根画布的缩放")]
        [SerializeField]
        private CanvasScaler m_UIRootCanvasScaler;

        [Header("UI分组")]
        [SerializeField]
        private UIGroup[] m_Groups;

        private Dictionary<byte, UIGroup> m_UIGroupDic;

        private float m_Standard = 0;
        private float m_Curr = 0;
        protected override void OnAwake()
        {
            base.OnAwake();
            m_UIGroupDic = new Dictionary<byte, UIGroup>();
            GameEntry.RegisterUpdateComponent(this);
            m_Standard = m_StandardWidth / (float)m_StandardHeight;
            m_Curr = Screen.width / (float)Screen.height;

            NormalFormCanvasScaler();

            int len = m_Groups.Length;
            for (int i = 0; i < len; i++)
            {
                UIGroup group = m_Groups[i];
                m_UIGroupDic[group.Id] = group;
            }
        }

        #region UI适配
        /// <summary>
        /// LoadingForm适配缩放
        /// </summary>
        public void LoadingFormCanvasScaler()
        {
            if (m_Curr > m_Standard)
            {
                m_UIRootCanvasScaler.matchWidthOrHeight = 0;
            }
            else
            {
                m_UIRootCanvasScaler.matchWidthOrHeight = m_Standard - m_Curr;
            }
        }

        /// <summary>
        /// FullForm适配缩放
        /// </summary>
        public void FullFormCanvasScaler()
        {
            m_UIRootCanvasScaler.matchWidthOrHeight = 1;
        }

        /// <summary>
        /// NormalForm适配缩放
        /// </summary>
        public void NormalFormCanvasScaler()
        {
            if (m_Curr >= m_Standard)
            {
                m_UIRootCanvasScaler.matchWidthOrHeight = 1;
            }
            else
            {
                m_UIRootCanvasScaler.matchWidthOrHeight = 0;
            }
        }
        #endregion

        /// <summary>
        /// 根据UI分组编号获取UI分组
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public UIGroup GetUIGroup(byte id)
        {
            UIGroup group = null;
            m_UIGroupDic.TryGetValue(id,out group);
            return group;
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
