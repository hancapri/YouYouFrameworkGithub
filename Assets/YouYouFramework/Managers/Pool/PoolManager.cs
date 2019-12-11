using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace YouYouFramework
{
    /// <summary>
    /// 对象池管理器
    /// </summary>
    public class PoolManager : ManagerBase, IDisposable
    {
        /// <summary>
        /// 类对象池
        /// </summary>
        public ClassObjectPool ClassObjectPool
        {
            get;
            private set;
        }

        /// <summary>
        /// 游戏问题对象池
        /// </summary>
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
