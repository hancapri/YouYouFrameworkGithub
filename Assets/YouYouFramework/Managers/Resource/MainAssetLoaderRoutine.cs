using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 主资源加载器（包含依赖资源）
    /// </summary>
    public class MainAssetLoaderRoutine
    {
        /// <summary>
        /// 当前的资源信息
        /// </summary>
        private AssetEntity m_CurrAssetEntity;

        /// <summary>
        /// 需要加载的依赖项资源数量
        /// </summary>
        private int m_NeedLoadAssetDependCount = 0;

        /// <summary>
        /// 当前已经加载的依赖资源数量
        /// </summary>
        private int m_CurrLoadAssetDependCount = 0;

        /// <summary>
        /// 主资源加载完毕回调
        /// </summary>
        private BaseAction<ResourceEntity> m_OnComplete;

        /// <summary>
        /// 加载主资源
        /// </summary>
        /// <param name="assetCategory"></param>
        /// <param name="assetFullName"></param>
        /// <param name="onComplete"></param>
        public void Load(AssetCategory assetCategory, string assetFullName, BaseAction<ResourceEntity> onComplete = null)
        {
            m_OnComplete = onComplete;
            m_CurrAssetEntity = GameEntry.Resource.ResourceLoaderManager.GetAssetEntity(assetCategory,assetFullName);
            LoadDependsAsset();
        }

        /// <summary>
        /// 加载主资源
        /// </summary>
        private void LoadMainAsset()
        {
            //1.先从分类资源池中（AssetPool）中取资源
            ResourceEntity assetEntity = GameEntry.Pool.PoolManager.AssetPool[m_CurrAssetEntity.Category].Spawn(m_CurrAssetEntity.AssetFullName);
            if (assetEntity != null)
            {
                Debug.LogError("从资源池中加载"+assetEntity.ResourceName);
                if (m_OnComplete != null)
                {
                    m_OnComplete(assetEntity);
                }
                return;
            }

            //2.没有再从ab包中取
            //加载ab包
            GameEntry.Resource.ResourceLoaderManager.LoadAssetBundle(m_CurrAssetEntity.AssetBundleName,onComplete:(AssetBundle assetBundle)=>
            {
                //加载资源
                GameEntry.Resource.ResourceLoaderManager.LoadAsset(m_CurrAssetEntity.AssetFullName, assetBundle, onComplete: (UnityEngine.Object obj) =>
                {
                    assetEntity = GameEntry.Pool.DequeueClassObject<ResourceEntity>();
                    assetEntity.Category = m_CurrAssetEntity.Category;
                    assetEntity.IsAssetBundle = false;
                    assetEntity.ResourceName = m_CurrAssetEntity.AssetFullName;
                    assetEntity.Target = obj;
                    GameEntry.Pool.PoolManager.AssetPool[m_CurrAssetEntity.Category].Register(assetEntity);

                    if (m_OnComplete != null)
                    {
                        m_OnComplete(assetEntity);
                    }
                    Reset();
                });
            });
        }

        /// <summary>
        /// 加载依赖资源
        /// </summary>
        private void LoadDependsAsset()
        {
            List<AssetDependsEntity> lst = m_CurrAssetEntity.DependsAssetList;
            if (lst != null)
            {
                int len = lst.Count;
                m_NeedLoadAssetDependCount = len;
                for (int i = 0; i < len; i++)
                {
                    AssetDependsEntity entity = lst[i];
                    MainAssetLoaderRoutine routine = GameEntry.Pool.DequeueClassObject<MainAssetLoaderRoutine>();
                    routine.Load(entity.Category, entity.AssetFullName, OnLoadDependsAssetComplete);
                }
            }
            else
            {
                LoadMainAsset();
            }
        }

        /// <summary>
        /// 加载某个依赖资源完毕
        /// </summary>
        private void OnLoadDependsAssetComplete(ResourceEntity entity)
        {
            m_CurrLoadAssetDependCount++;

            if (m_NeedLoadAssetDependCount == m_CurrLoadAssetDependCount)
            {
                LoadMainAsset();
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        private void Reset()
        {
            m_OnComplete = null;
            m_CurrAssetEntity = null;
            m_NeedLoadAssetDependCount = 0;
            m_CurrLoadAssetDependCount = 0;
            GameEntry.Pool.EnqueueClassObject(this);
        }
    }
}
