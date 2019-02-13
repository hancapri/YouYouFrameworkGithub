using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 时间管理器
    /// </summary>
    public class TimeManager : ManagerBase
    {
        /// <summary>
        /// 定时器链表
        /// </summary>
        private LinkedList<TimeAction> m_TimeActionList;

        #region 定时器链表管理
        /// <summary>
        /// 注册定时器
        /// </summary>
        /// <param name="action"></param>
        public void RegisterTimeAction(TimeAction action)
        {
            m_TimeActionList.AddLast(action);
        }

        /// <summary>
        /// 移除定时器
        /// </summary>
        /// <param name="action"></param>
        public void RemoveTimeAction(TimeAction action)
        {
            m_TimeActionList.Remove(action);
        }
        #endregion

        public TimeManager()
        {
            m_TimeActionList = new LinkedList<TimeAction>();
        }

        public void OnUpdate()
        {
            for (LinkedListNode<TimeAction> curr = m_TimeActionList.First; curr != null; curr = curr.Next)
            {
                curr.Value.OnUpdate();
            }
        }
    }
}
