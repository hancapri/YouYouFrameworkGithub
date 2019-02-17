using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace YouYouFramework
{
    /// <summary>
    /// 流程管理器
    /// </summary>
    public class PoolManager : ManagerBase, IDisposable
    {
        public ClassObjectPool ClassObjectPool
        {
            get;
            private set;
        }
        public GameObjectPool GameObjectPool
        {
            get;
            private set;
        }
        public PoolManager()
        {
            ClassObjectPool = new ClassObjectPool();
            GameObjectPool = new GameObjectPool();
        }

        /// <summary>
        /// 释放类对象池
        /// </summary>
        public void ClearClassObjectPool()
        {
            ClassObjectPool.ClearPool();
        }

        public void Dispose()
        {
            ClassObjectPool.Dispose();
        }
    }
}
