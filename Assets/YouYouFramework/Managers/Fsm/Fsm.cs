using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 状态机
    /// </summary>
    public class Fsm<T> : FsmBase where T : class
    {
        /// <summary>
        /// 当前状态
        /// </summary>
        private FsmState<T> m_CurrState;

        /// <summary>
        /// 状态字典
        /// </summary>
        private Dictionary<byte, FsmState<T>> m_StateDic;

        /// <summary>
        /// 参数字典
        /// </summary>
        public Dictionary<string, object> m_ParamDic;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fsmId">状态机编号</param>
        /// <param name="owner">拥有者</param>
        /// <param name="states">状态数组</param>
        public Fsm(int fsmId, T owner, FsmState<T>[] states) : base(fsmId, owner.GetType())
        {
            m_StateDic = new Dictionary<byte, FsmState<T>>();
            m_ParamDic = new Dictionary<string, object>();

            //把状态加入字典
            int len = states.Length;
            for (int i = 0; i < len; i++)
            {
                FsmState<T> state = states[i];
                state.CurrFsm = this;
                m_StateDic[(byte)i] = state;
            }

            //设置默认状态
            CurrStateType = 0;
            m_CurrState = m_StateDic[CurrStateType];
        }

        /// <summary>
        /// 获取某个状态
        /// </summary>
        /// <param name="stateType"></param>
        /// <returns></returns>
        public FsmState<T> GetState(byte stateType)
        {
            FsmState<T> state = null;
            m_StateDic.TryGetValue(stateType, out state);
            return state;
        }

        /// <summary>
        /// 执行当前状态的Update
        /// </summary>
        public void OnUpdate()
        {
            if (m_CurrState != null)
            {
                m_CurrState.OnUpdate();
            }
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="newState"></param>
        public void ChangeState(byte newState)
        {
            //如果新状态就是当前状态，不切换
            if (CurrStateType == newState)
            {
                return;
            }
            if (m_CurrState != null)
            {
                m_CurrState.OnLeave();
            }
            CurrStateType = newState;
            m_CurrState = m_StateDic[CurrStateType];
            //进入新状态
            if (m_CurrState != null)
            {
                m_CurrState.OnEnable();
            }

        }

        /// <summary>
        /// 关闭状态机
        /// </summary>
        public override void ShutDown()
        {
            if (m_CurrState != null)
            {
                m_CurrState.OnLeave();
            }
            foreach (var state in m_StateDic)
            {
                state.Value.OnDestroy();
            }
            m_StateDic.Clear();
        }
    }
}
