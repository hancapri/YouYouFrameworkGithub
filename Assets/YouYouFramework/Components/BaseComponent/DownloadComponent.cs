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

        [Header("下载器数量")]
        public int DownloadRoutineCount = 5;

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
        public void BeginDownloadSingle(string url, BaseAction<string,ulong,float> onUpdate = null, BaseAction<string> onComplete = null)
        {
            m_DownloadManager.BeginDownloadSingle(url, onUpdate, onComplete);
        }

        public void BeginDownloadMulit(LinkedList<string> lstUrl, BaseAction<int,int,ulong,ulong> onDownloadMulitUpdate = null, BaseAction onDownloadMulitComplete = null)
        {
            m_DownloadManager.BeginDownloadMulit(lstUrl, onDownloadMulitUpdate, onDownloadMulitComplete);
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
