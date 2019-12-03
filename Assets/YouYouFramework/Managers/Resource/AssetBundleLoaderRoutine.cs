using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// ab包加载器
    /// </summary>
    public class AssetBundleLoaderRoutine
    {
        /// <summary>
        /// 当前资源包信息
        /// </summary>
        private AssetBundleInfoEntity m_CurrAssetBundleInfo;

        /// <summary>
        /// 资源包创建请求
        /// </summary>
        private AssetBundleCreateRequest m_CurrAssetBundleCreateRequest;

        /// <summary>
        /// 资源包请求更新update进度
        /// </summary>
        public Action<float> OnAssetBundleCreateUpdate;

        /// <summary>
        /// 加载资源包完毕回调
        /// </summary>
        public Action<AssetBundle> OnLoadAssetBundleComplete;

        public void LoadAssetBundle(string assetBundlePath)
        {
            m_CurrAssetBundleInfo = GameEntry.Resource.ResourceManager.GetAssetBundleInfo(assetBundlePath);

            byte[] buffer = GameEntry.Resource.ResourceManager.LocalAssetManager.GetFileBuffer(assetBundlePath);
            if (buffer == null)
            {
                GameEntry.Resource.ResourceManager.StreamingAssetsManager.ReadAssetBundle(assetBundlePath, (byte[] buff) =>
                {
                    if (buff == null)
                    {
                        //TODO:如果只读区也没有,从CDN下载
                    }
                    else
                    {
                        LoadAssetBundleAsync(buff);
                    }
                });
            }
            else
            {
                LoadAssetBundleAsync(buffer);
            }
        }

        /// <summary>
        /// 异步加载资源包
        /// </summary>
        /// <param name="buff"></param>
        private void LoadAssetBundleAsync(byte[] buffer)
        {
            if (m_CurrAssetBundleInfo.IsEncrypt)
            {
                buffer = SecurityUtil.Xor(buffer);
            }

            m_CurrAssetBundleCreateRequest = AssetBundle.LoadFromMemoryAsync(buffer);
        }

        /// <summary>
        /// 重置
        /// </summary>
        public void Reset()
        {
            m_CurrAssetBundleCreateRequest = null;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void OnUpdate()
        {
            UpdateAssetBundleCreateRequest();
        }

        /// <summary>
        /// 更新资源包请求
        /// </summary>
        private void UpdateAssetBundleCreateRequest()
        {
            if (m_CurrAssetBundleCreateRequest != null)
            {
                if (m_CurrAssetBundleCreateRequest.isDone)
                {
                    AssetBundle assetBundle = m_CurrAssetBundleCreateRequest.assetBundle;
                    if (assetBundle != null)
                    {
                        GameEntry.Log(LogCategory.Resource, string.Format("资源包=>{0} 加载完毕", m_CurrAssetBundleInfo.AssetBundleName));
                        Reset();

                        if (OnLoadAssetBundleComplete != null)
                        {
                            OnLoadAssetBundleComplete(assetBundle);
                        }
                    }
                    else
                    {
                        GameEntry.Log(LogCategory.Resource, string.Format("资源包=>{0} 加载失败", m_CurrAssetBundleInfo.AssetBundleName));
                        Reset();

                        if (OnLoadAssetBundleComplete != null)
                        {
                            OnLoadAssetBundleComplete(null);
                        }
                    }
                }
                else
                {
                    //加载进度
                    if (OnAssetBundleCreateUpdate != null)
                    {
                        OnAssetBundleCreateUpdate(m_CurrAssetBundleCreateRequest.progress);
                    }
                }
            }
        }
    }
}
