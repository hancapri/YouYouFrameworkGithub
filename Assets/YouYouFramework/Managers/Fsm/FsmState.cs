using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 单个状态
    /// </summary>
    public abstract class FsmState<T> where T : class
    {
        /// <summary>
        /// 所属状态机
        /// </summary>
        public Fsm<T> CurrFsm;

        /// <summary>
        /// 进入状态
        /// </summary>
        public virtual void OnEnable() { }

        /// <summary>
        /// 执行状态
        /// </summary>
        public virtual void OnUpdate() { }

        /// <summary>
        /// 离开状态
        /// </summary>
        public virtual void OnLeave() { }

        /// <summary>
        /// 状态机销毁时调用
        /// </summary>
        public virtual void OnDestroy() { }

    }
}
