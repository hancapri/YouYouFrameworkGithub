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

        /// <summary>
        /// 资源池
        /// </summary>
        public ResourcePool AssetBundlePool
        {
            get;
            private set;
        }

        public PoolManager()
        {
            ClassObjectPool = new ClassObjectPool();
            GameObjectPool = new GameObjectPool();
            AssetBundlePool = new ResourcePool("AssetBundlePool");
        }

        /// <summary>
        /// 释放类对象池
        /// </summary>
        public void ClearClassObjectPool()
        {
            ClassObjectPool.ClearPool();
        }

        /// <summary>
        /// 释放资源包池
        /// </summary>
        public void ReleaseAssetBundlePool()
        {
            AssetBundlePool.Release();
        }

        public void Dispose()
        {
            ClassObjectPool.Dispose();
            GameObjectPool.Dispose();
        }
    }
}
