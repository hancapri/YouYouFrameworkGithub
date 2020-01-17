using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 下载组件
    /// </summary>
    public class DownloadComponent : YouYouBaseComponent, IUpdateComponent
    {
        [Header("写入磁盘的缓存大小")]
        public int FlushSize = 1024 * 1024;

        private DownloadManager m_DownloadManager;

        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);
            m_DownloadManager = new DownloadManager();
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="onUpdate"></param>
        /// <param name="onComplete"></param>
        public void BeginDownload(string url, BaseAction<float> onUpdate = null, BaseAction<string> onComplete = null)
        {
            m_DownloadManager.BeginDownload(url, onUpdate, onComplete);
        }


        public void OnUpdate()
        {
            m_DownloadManager.OnUpdate();
        }

        public override void Shutdown()
        {
            GameEntry.RemoveUpdateComponent(this);
        }
    }
}
