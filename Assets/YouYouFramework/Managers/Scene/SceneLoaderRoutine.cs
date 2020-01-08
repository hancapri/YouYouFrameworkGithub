using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace YouYouFramework
{
    /// <summary>
    /// 场景加载和卸载器
    /// </summary>
    public class SceneLoaderRoutine
    {
        private AsyncOperation m_CurrAsync = null;

        /// <summary>
        /// 进度更新
        /// </summary>
        private BaseAction<int, float> OnProgressUpdate;

        /// <summary>
        /// 加载场景完毕
        /// </summary>
        private BaseAction<SceneLoaderRoutine> OnLoadSceneComplete;

        /// <summary>
        /// 卸载场景完毕
        /// </summary>
        private BaseAction<SceneLoaderRoutine> OnUnLoadSceneComplete;

        /// <summary>
        /// 场景明细编码
        /// </summary>
        private int m_SceneDetailId;

        /// <summary>
        /// 场景加载（异步叠加）
        /// </summary>
        /// <param name="sceneDetailId"></param>
        /// <param name="sceneName"></param>
        /// <param name="onProgressUpdate"></param>
        /// <param name="onLoadSceneComplete"></param>
        public void LoadScene(int sceneDetailId, string sceneName, BaseAction<int, float> onProgressUpdate, BaseAction<SceneLoaderRoutine> onLoadSceneComplete)
        {
            Reset();

            m_SceneDetailId = sceneDetailId;
            OnProgressUpdate = onProgressUpdate;
            OnLoadSceneComplete = onLoadSceneComplete;

            GameEntry.Resource.ResourceLoaderManager.LoadAssetBundle(GameEntry.Resource.GetSceneAssetBundlePath(sceneName), onComplete: (AssetBundle ab) =>
            {
                m_CurrAsync = SceneManager.LoadSceneAsync(sceneName,LoadSceneMode.Additive);
                if (m_CurrAsync == null)
                {
                    if (OnLoadSceneComplete != null)
                    {
                        OnLoadSceneComplete(this);
                    }
                }
            });
        }

        /// <summary>
        /// 卸载场景（异步）
        /// </summary>
        /// <param name="sceneName"></param>
        /// <param name="onUnLoadSceneComplete"></param>
        public void UnLoadScene(string sceneName,BaseAction<SceneLoaderRoutine> onUnLoadSceneComplete)
        {
            Reset();

            OnUnLoadSceneComplete = onUnLoadSceneComplete;
            m_CurrAsync = SceneManager.UnloadSceneAsync(sceneName);
            if (m_CurrAsync == null)
            {
                if (OnUnLoadSceneComplete != null)
                {
                    OnUnLoadSceneComplete(this);
                }
            }
        }

        private void Reset()
        {
            m_CurrAsync = null;
            OnProgressUpdate = null;
            OnLoadSceneComplete = null;
            OnUnLoadSceneComplete = null;
        }

        /// <summary>
        /// 加载状态更新
        /// </summary>
        public void OnUpdate()
        {
            if (m_CurrAsync == null)
            {
                return;
            }

            if (!m_CurrAsync.isDone)
            {
                if (m_CurrAsync.progress >= 0.9)
                {
                    if (OnProgressUpdate != null)
                    {
                        OnProgressUpdate(m_SceneDetailId, m_CurrAsync.progress);
                    }

                    m_CurrAsync = null;

                    if (OnLoadSceneComplete != null)
                    {
                        OnLoadSceneComplete(this);
                    }
                }
                else
                {
                    if (OnProgressUpdate != null)
                    {
                        OnProgressUpdate(m_SceneDetailId, m_CurrAsync.progress);
                    }
                }
            }
            else
            {
                m_CurrAsync = null;
                if (OnLoadSceneComplete != null)
                {
                    OnLoadSceneComplete(this);
                }
            }
        }
    }
}