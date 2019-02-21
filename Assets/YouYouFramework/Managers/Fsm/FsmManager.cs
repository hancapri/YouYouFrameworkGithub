using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace YouYouFramework
{
    /// <summary>
    /// 状态机管理器
    /// </summary>
    public class FsmManager : ManagerBase, IDisposable
    {
        private Dictionary<int, FsmBase> m_FsmDic;

        public FsmManager()
        {
            m_FsmDic = new Dictionary<int, FsmBase>();
        }

        /// <summary>
        /// 创建状态机
        /// </summary>
        /// <typeparam name="T">拥有者类型</typeparam>
        /// <param name="fsmId">状态机编号</param>
        /// <param name="owner">拥有者</param>
        /// <param name="states">状态机数组</param>
        /// <returns></returns>
        public Fsm<T> Create<T>(int fsmId, T owner, FsmState<T>[] states) where T : class
        {
            Fsm<T> fsm = new Fsm<T>(fsmId, owner, states);
            m_FsmDic[fsmId] = fsm;
            return fsm;
        }

        /// <summary>
        /// 销毁状态机
        /// </summary>
        /// <param name="fsmId"></param>
        public void DestroyFsm(int fsmId)
        {
            FsmBase fsm = null;
            m_FsmDic.TryGetValue(fsmId, out fsm);
            if (fsm != null)
            {
                fsm.ShutDown();
                m_FsmDic.Remove(fsmId);
            }
        }

        public void Dispose()
        {
            foreach (var fsm in m_FsmDic)
            {
                fsm.Value.ShutDown();
            }
            m_FsmDic.Clear();
        }
    }
}
