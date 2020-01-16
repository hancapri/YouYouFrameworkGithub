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

        private UIManager m_UIManager;
        private UILayer m_UILayer;
        private UIPool m_UIPool;

        [Header("释放间隔（秒）")]
        [SerializeField]
        private float m_ClearInterval = 120f;

        /// <summary>
        /// 下次运行时间
        /// </summary>
        private float m_NextRunTime = 0f;

        /// <summary>
        /// UI回池后过期时间
        /// </summary>
        public float UIExpire = 120f;

        /// <summary>
        /// UI对象池的最大数量
        /// </summary>
        public int UIPoolMaxCount = 5;

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
            m_UIManager = new UIManager();
            m_UILayer = new UILayer();
            m_UILayer.Init(m_Groups);
            m_UIPool = new UIPool();
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

        /// <summary>
        /// 打开UI窗体
        /// </summary>
        /// <param name="uiFormId"></param>
        /// <param name="userData"></param>
        public void OpenUIForm(int uiFormId, object userData = null,BaseAction<UIFormBase> onOpen = null)
        {
            m_UIPool.CheckByOpenUI();
            m_UIManager.OpenUIForm(uiFormId, userData, onOpen);
        }

        /// <summary>
        /// 关闭UI窗体(根据UIFormBase)
        /// </summary>
        /// <param name="formBase"></param>
        public void CloseUIForm(UIFormBase formBase)
        {
            m_UIManager.CloseUIForm(formBase);
        }

        /// <summary>
        /// 关闭UI窗体(根据UIFormId)
        /// </summary>
        /// <param name="formBase"></param>
        public void CloseUIForm(int uiformId)
        {
            m_UIManager.CloseUIForm(uiformId);
        }

        /// <summary>
        /// 设置层级
        /// </summary>
        /// <param name="formBase"></param>
        /// <param name="isAdd"></param>
        public void SetSortingOrder(UIFormBase formBase, bool isAdd)
        {
            m_UILayer.SetSortingOrder(formBase, isAdd);
        }

        /// <summary>
        /// UI出池
        /// </summary>
        /// <param name="uiFormId"></param>
        /// <returns></returns>
        public UIFormBase Dequeue(int uiFormId)
        {
            return m_UIPool.Dequeue(uiFormId);
        }

        /// <summary>
        /// UI回池
        /// </summary>
        /// <param name="form"></param>
        public void Enqueue(UIFormBase form)
        {
            m_UIPool.Enqueue(form);
        }

        public void OnUpdate()
        {
            if (Time.time > m_NextRunTime+ m_ClearInterval)
            {
                m_NextRunTime = Time.time;
                //释放UI对象池
                m_UIPool.CheckClear();
            }
        }

        public override void Shutdown()
        {

        }
    }
}
