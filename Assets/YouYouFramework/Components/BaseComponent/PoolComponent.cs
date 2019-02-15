using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 对象池组件
    /// </summary>
    public class PoolComponent : YouYouBaseComponent
    {
        private PoolManager m_PoolManager;

        public ClassObjectPool ClassObjectPool;

        protected override void OnAwake()
        {
            base.OnAwake();
            m_PoolManager = new PoolManager();
            ClassObjectPool = m_PoolManager.ClassObjectPool;
        }

        public override void Shutdown()
        {
            m_PoolManager.Dispose();
        }
    }
}
