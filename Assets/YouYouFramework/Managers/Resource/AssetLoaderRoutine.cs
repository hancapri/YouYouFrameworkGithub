using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 资源加载器
    /// </summary>
    public class AssetLoaderRoutine
    {
        /// <summary>
        /// 资源加载请求
        /// </summary>
        private AssetBundleRequest m_CurrAssetBundleRequest;

        /// <summary>
        /// 资源加载进度
        /// </summary>
        public Action<float> OnAssetUpdate;

        /// <summary>
        /// 加载资源完毕回调
        /// </summary>
        public Action<UnityEngine.Object> OnLoadAssetComplete;

        /// <summary>
        /// 当前加载的资源名称
        /// </summary>
        private string currAssetName = "";

        public void LoadAsset(string assetName, AssetBundle assetBundle)
        {
            currAssetName = assetName;
            m_CurrAssetBundleRequest = assetBundle.LoadAssetAsync(assetName);
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            currAssetName = "";
            m_CurrAssetBundleRequest = null;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void OnUpdate()
        {
            UpdateAssetBundleRequest();
        }

        /// <summary>
        /// 更新资源请求
        /// </summary>
        private void UpdateAssetBundleRequest()
        {
            if (m_CurrAssetBundleRequest != null)
            {
                if (m_CurrAssetBundleRequest.isDone)
                {
                    UnityEngine.Object obj = m_CurrAssetBundleRequest.asset;
                    if (obj != null)
                    {
                        GameEntry.Log(LogCategory.Resource, string.Format("资源=>{0} 加载完毕", obj.name));
                        Reset();

                        if (OnLoadAssetComplete != null)
                        {
                            OnLoadAssetComplete(obj);
                        }
                    }
                    else
                    {
                        GameEntry.LogError("资源包=>{0} 加载失败", currAssetName);
                        Reset();

                        if (OnLoadAssetComplete != null)
                        {
                            OnLoadAssetComplete(null);
                        }
                    }
                }
                else
                {
                    //加载进度
                    if (OnAssetUpdate != null)
                    {
                        OnAssetUpdate(m_CurrAssetBundleRequest.progress);
                    }
                }
            }
        }
    }
}
