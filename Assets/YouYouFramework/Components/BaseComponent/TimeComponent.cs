using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 时间组件
    /// </summary>
    public class TimeComponent : YouYouBaseComponent, IUpdateComponent
    {
        /// <summary>
        /// 定时器管理器
        /// </summary>
        private TimeManager m_TimeManager;

        #region 定时器管理
        /// <summary>
        /// 注册定时器
        /// </summary>
        /// <param name="action"></param>
        internal void RegisterTimeAction(TimeAction action)
        {
            m_TimeManager.RegisterTimeAction(action);
        }

        /// <summary>
        /// 移除定时器
        /// </summary>
        /// <param name="action"></param>
        internal void RemoveTimeAction(TimeAction action)
        {
            m_TimeManager.RemoveTimeAction(action);
        }

        /// <summary>
        /// 创建定时器
        /// </summary>
        /// <returns></returns>
        internal TimeAction CreatTimeAction()
        {
            return m_TimeManager.CreatTimeAction();
        }
        #endregion 

        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);

            m_TimeManager = new TimeManager();
        }

        public void OnUpdate()
        {
            m_TimeManager.OnUpdate();
        }

        public override void Shutdown()
        {
            m_TimeManager.Dispose();
            GameEntry.RemoveUpdateComponent(this);
        }
    }
}
