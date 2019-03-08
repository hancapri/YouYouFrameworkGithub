using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 流程组件
    /// </summary>
    public class ProcedureComponent : YouYouBaseComponent, IUpdateComponent
    {
        /// <summary>
        /// 流程管理器
        /// </summary>
        private ProcedureManager m_ProcedureManager;

        /// <summary>
        /// 当前流程状态枚举值
        /// </summary>
        public ProcedureState CurrProcedureState
        {
            get { return m_ProcedureManager.CurrProcedureState; }
        }

        /// <summary>
        /// 当前流程实体
        /// </summary>
        public FsmState<ProcedureManager> CurrProcedure
        {
            get { return m_ProcedureManager.CurrProcedure; }
        }


        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);
            m_ProcedureManager = new ProcedureManager();
        }

        protected override void OnStart()
        {
            base.OnStart();
            //要在Start的时候进行初始化
            m_ProcedureManager.Init();
        }

        /// <summary>
        /// 设置参数字典值
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetData<TData>(string key, TData value)
        {
            m_ProcedureManager.CurrFsm.SetData(key, value);
        }

        /// <summary>
        /// 获取参数字典里面的值
        /// </summary>
        /// <typeparam name="TData"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TData GetData<TData>(string key)
        {
            return m_ProcedureManager.CurrFsm.GetData<TData>(key);
        }

        /// <summary>
        /// 切换状态
        /// </summary>
        /// <param name="state"></param>
        public void ChangeState(ProcedureState state)
        {
            m_ProcedureManager.ChangeState(state);
        }

        public void OnUpdate()
        {
            m_ProcedureManager.OnUpdate();
        }

        public override void Shutdown()
        {
            m_ProcedureManager.Dispose();
        }
    }
}
