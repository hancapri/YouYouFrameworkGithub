using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 定时器
    /// </summary>
    public class TimeAction : ManagerBase
    {
        /// <summary>
        /// 是否运行中
        /// </summary>
        public bool IsRunning
        {
            get;
            private set;
        }

        /// <summary>
        /// 当前运行了多长时间
        /// </summary>
        private float m_CurrRunTime;

        /// <summary>
        /// 当前运行了多少次
        /// </summary>
        private int m_CurrLoop;

        /// <summary>
        /// 延迟时间
        /// </summary>
        private float m_DelayTime;

        /// <summary>
        /// 每次间隔秒数
        /// </summary>
        private float m_Interval;

        /// <summary>
        /// 循环次数(-1表示无限循环，0表示循环一次)
        /// </summary>
        private int m_Loop;

        /// <summary>
        /// 开始运行委托
        /// </summary>
        private Action m_OnStartAction;

        /// <summary>
        /// 运行中的委托
        /// </summary>
        private Action<int> m_OnUpdateAction;

        /// <summary>
        /// 结束运行委托
        /// </summary>
        private Action m_OnCompleteAction;

        /// <summary>
        /// 初始化定时器
        /// </summary>
        /// <param name="delayTime">延迟时间</param>
        /// <param name="interval">间隔秒数</param>
        /// <param name="loop">循环次数</param>
        /// <param name="onStartAction">开始回调</param>
        /// <param name="onUpdateAction">运行中回调</param>
        /// <param name="onCompleteAction">结束回调</param>
        /// <returns></returns>
        public TimeAction Init(float delayTime, float interval, int loop, Action onStartAction, Action<int> onUpdateAction, Action onCompleteAction)
        {
            m_DelayTime = delayTime;
            m_Interval = interval;
            m_Loop = loop;
            m_OnStartAction = onStartAction;
            m_OnUpdateAction = onUpdateAction;
            m_OnCompleteAction = onCompleteAction;
            m_CurrLoop = 0;
            return this;
        }

        /// <summary>
        /// 定时器启动
        /// </summary>
        public void Run()
        {
            //1.把自己加入时间管理器链表中
            GameEntry.Time.RegisterTimeAction(this);
            //2.设置当前运行的时间
            m_CurrRunTime = Time.time;
        }

        /// <summary>
        /// 定时器暂停
        /// </summary>
        public void Pause()
        {
            IsRunning = false;
        }

        /// <summary>
        /// 定时器结束
        /// </summary>
        public void Stop()
        {
            if (m_OnCompleteAction != null)
            {
                m_OnCompleteAction();
            }
            IsRunning = false;
            GameEntry.Time.RemoveTimeAction(this);
        }

        /// <summary>
        /// 定时器每帧更新
        /// </summary>
        public void OnUpdate()
        {
            //先处理延迟，过了延迟时间，第一次开始执行
            if (!IsRunning && Time.time > m_CurrRunTime + m_DelayTime)
            {
                IsRunning = true;

                m_CurrRunTime = Time.time;

                if (m_OnStartAction != null)
                {
                    m_OnStartAction();
                }
            }

            if (!IsRunning) return;

            //间隔m_Interval时间循环执行
            if (Time.time > m_CurrRunTime)
            {
                m_CurrRunTime = Time.time + m_Interval;

                if (m_OnUpdateAction != null)
                {
                    m_OnUpdateAction(m_Loop - m_CurrLoop);
                }

                if (m_Loop > -1)
                {
                    m_CurrLoop++;
                    if (m_CurrLoop >= m_Loop)
                    {
                        Stop();
                    }
                }

            }
        }
    }
}

