using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 事件组件
    /// </summary>
    public class EventComponent : YouYouBaseComponent
    {
        /// <summary>
        /// 事件管理器
        /// </summary>
        private EventManager m_EventManager;

        /// <summary>
        /// Socket事件，给外界调用
        /// </summary>
        public SocketEvent SocketEvent;
        /// <summary>
        /// 通用事件，给外界调用
        /// </summary>
        public CommonEvent CommonEvent;

        protected override void OnAwake()
        {
            base.OnAwake();
            m_EventManager = new EventManager();
            SocketEvent = m_EventManager.SocketEvent;
            CommonEvent = m_EventManager.CommonEvent;
        }

        /// <summary>
        /// 释放
        /// </summary>
        public override void Shutdown()
        {
            m_EventManager.Dispose();
        }
    }
}
