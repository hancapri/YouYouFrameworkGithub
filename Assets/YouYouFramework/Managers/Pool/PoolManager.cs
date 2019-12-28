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
        /// 资源包池
        /// </summary>
        public ResourcePool AssetBundlePool
        {
            get;
            private set;
        }

        /// <summary>
        /// 分类资源池
        /// </summary>
        public Dictionary<AssetCategory, ResourcePool> AssetPool
        {
            get;
            private set;
        }

        public PoolManager()
        {
            ClassObjectPool = new ClassObjectPool();
            GameObjectPool = new GameObjectPool();
            AssetBundlePool = new ResourcePool("AssetBundlePool");
            AssetPool = new Dictionary<AssetCategory, ResourcePool>();

            var enumerator = Enum.GetValues(typeof(AssetCategory)).GetEnumerator();
            while (enumerator.MoveNext())
            {
                AssetCategory assetCategory = (AssetCategory)enumerator.Current;
                if (assetCategory == AssetCategory.None)
                {
                    continue;
                }
                AssetPool[assetCategory] = new ResourcePool(assetCategory.ToString());
            }
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

        /// <summary>
        /// 卸载分类资源池中的所有资源
        /// </summary>
        public void ReleaseAssetPool()
        {
            var enumerator = Enum.GetValues(typeof(AssetCategory)).GetEnumerator();
            while (enumerator.MoveNext())
            {
                AssetCategory assetCategory = (AssetCategory)enumerator.Current;
                if (assetCategory == AssetCategory.None)
                {
                    continue;
                }
                AssetPool[assetCategory].Release();
            }
        }

        public void Dispose()
        {
            ClassObjectPool.Dispose();
            GameObjectPool.Dispose();
        }
    }
}
