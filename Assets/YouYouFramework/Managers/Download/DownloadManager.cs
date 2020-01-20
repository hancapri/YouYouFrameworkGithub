using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 下载管理器
    /// </summary>
    public class DownloadManager : ManagerBase
    {
        /// <summary>
        /// 下载器链表
        /// </summary>
        private LinkedList<DownloadRoutine> m_DownloadRoutineList;

        /// <summary>
        /// 需要下载的文件链表
        /// </summary>
        private LinkedList<string> m_NeedDownloadList;

        /// <summary>
        /// 多文件下载中委托
        /// </summary>
        private BaseAction<int, int, ulong, ulong> m_OnDownloadMulitUpdate;

        /// <summary>
        /// 多文件下载完毕委托
        /// </summary>
        private BaseAction m_OnDownloadMulitComplete;

        /// <summary>
        /// 多文件需要下载的数量
        /// </summary>
        private int m_DownloadMulitNeedCount = 0;

        /// <summary>
        /// 多文件当前下载的数量
        /// </summary>
        private int m_DownloadMulitCurrCount = 0;

        /// <summary>
        /// 多文件需要下载的总大小（字节）
        /// </summary>
        private ulong m_DownloadMulitTotalSize = 0;

        /// <summary>
        /// 多文件当前下载的总大小（字节）
        /// </summary>
        private ulong m_DownloadMulitCurrSize = 0;

        /// <summary>
        /// 多文件当前下载的大小字典
        /// </summary>
        private Dictionary<string, ulong> m_DownloadMulitCurrSizeDic;

        public DownloadManager()
        {
            m_DownloadRoutineList = new LinkedList<DownloadRoutine>();
            m_NeedDownloadList = new LinkedList<string>();
            m_DownloadMulitCurrSizeDic = new Dictionary<string, ulong>();
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="onUpdate"></param>
        /// <param name="onComplete"></param>
        public void BeginDownloadSingle(string url,BaseAction<string,ulong,float> onUpdate = null, BaseAction<string> onComplete = null)
        {
            AssetBundleInfoEntity entity = GameEntry.Resource.ResourceManager.GetAssetBundleInfo(url);
            if (entity == null)
            {
                GameEntry.LogError("无效资源包=>"+url);
                return;
            }
            DownloadRoutine routine = GameEntry.Pool.DequeueClassObject<DownloadRoutine>();
            routine.BeginDownload(url, entity, onUpdate, onComplete:(string fileUrl,DownloadRoutine rou)=>
            {
                m_DownloadRoutineList.Remove(rou);
                GameEntry.Pool.EnqueueClassObject(routine);

                if (onComplete != null)
                {
                    onComplete(fileUrl);
                }
            });
            m_DownloadRoutineList.AddLast(routine);
        }

        /// <summary>
        /// 同时下载多个文件
        /// </summary>
        /// <param name="lstUrl"></param>
        /// <param name="onDownloadMulitUpdate"></param>
        /// <param name="onDownloadMulitComplete"></param>
        public void BeginDownloadMulit(LinkedList<string> lstUrl, BaseAction<int, int, ulong, ulong> onDownloadMulitUpdate = null, BaseAction onDownloadMulitComplete = null)
        {
            m_OnDownloadMulitUpdate = onDownloadMulitUpdate;
            m_OnDownloadMulitComplete = onDownloadMulitComplete;

            m_NeedDownloadList.Clear();
            m_DownloadMulitCurrSizeDic.Clear();

            m_DownloadMulitNeedCount = 0;
            m_DownloadMulitCurrCount = 0;

            m_DownloadMulitTotalSize = 0;
            m_DownloadMulitCurrSize = 0;

            for (LinkedListNode<string> item = lstUrl.First; item != null; item = item.Next)
            {
                string url = item.Value;
                AssetBundleInfoEntity entity = GameEntry.Resource.ResourceManager.GetAssetBundleInfo(url);
                if (entity != null)
                {
                    m_DownloadMulitTotalSize += entity.Size;
                    m_DownloadMulitNeedCount++;
                    m_NeedDownloadList.AddLast(url);
                    m_DownloadMulitCurrSizeDic[url] = 0;
                }
                else
                {
                    GameEntry.LogError("无效资源包=>"+url);
                }
            }

            int routineCount = Mathf.Min(GameEntry.Download.DownloadRoutineCount, m_NeedDownloadList.Count);

            for (int i = 0; i < routineCount; i++)
            {
                DownloadRoutine routine = GameEntry.Pool.DequeueClassObject<DownloadRoutine>();
                string url = m_NeedDownloadList.First.Value;
                m_NeedDownloadList.RemoveFirst();

                AssetBundleInfoEntity entity = GameEntry.Resource.ResourceManager.GetAssetBundleInfo(url);
                routine.BeginDownload(url, entity, OnDownloadMulitUpdate, OnDownloadMulitComplete);
                m_DownloadRoutineList.AddLast(routine);
            }
        }

        private void OnDownloadMulitUpdate(string url, ulong currDownloadSize, float progress)
        {
            m_DownloadMulitCurrSizeDic[url] = currDownloadSize;
            ulong currSize = 0;
            var enumerator = m_DownloadMulitCurrSizeDic.GetEnumerator();
            while (enumerator.MoveNext())
            {
                currSize += enumerator.Current.Value;
            }

            m_DownloadMulitCurrSize = currSize;
            if (m_DownloadMulitCurrSize > m_DownloadMulitTotalSize)
            {
                m_DownloadMulitCurrSize = m_DownloadMulitTotalSize;
            }

            if (m_OnDownloadMulitUpdate != null)
            {
                m_OnDownloadMulitUpdate(m_DownloadMulitCurrCount,m_DownloadMulitNeedCount,m_DownloadMulitCurrSize, m_DownloadMulitTotalSize);
            }
        }

        private void OnDownloadMulitComplete(string  fileUrl, DownloadRoutine routine)
        {
            Debug.LogError("下载完毕="+ fileUrl);
            if (m_NeedDownloadList.Count > 0)
            {
                string url = m_NeedDownloadList.First.Value;
                m_NeedDownloadList.RemoveFirst();

                AssetBundleInfoEntity entity = GameEntry.Resource.ResourceManager.GetAssetBundleInfo(url);
                routine.BeginDownload(url, entity, OnDownloadMulitUpdate, OnDownloadMulitComplete);
            }
            else
            {
                m_DownloadRoutineList.Remove(routine);
                Debug.LogError("下载器回池");
                GameEntry.Pool.EnqueueClassObject(routine);
            }

            m_DownloadMulitCurrCount++;

            if (m_OnDownloadMulitUpdate != null)
            {
                m_OnDownloadMulitUpdate(m_DownloadMulitCurrCount, m_DownloadMulitNeedCount, m_DownloadMulitCurrSize, m_DownloadMulitTotalSize);
            }

            if (m_DownloadMulitCurrCount == m_DownloadMulitNeedCount)
            {
                m_DownloadMulitCurrSize = m_DownloadMulitTotalSize;
                if (m_OnDownloadMulitUpdate != null)
                {
                    m_OnDownloadMulitUpdate(m_DownloadMulitCurrCount, m_DownloadMulitNeedCount, m_DownloadMulitCurrSize, m_DownloadMulitTotalSize);
                }

                if (m_OnDownloadMulitComplete != null)
                {
                    m_OnDownloadMulitComplete();
                }
            }
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void OnUpdate()
        {
            var curr = m_DownloadRoutineList.First;
            while (curr != null)
            {
                curr.Value.OnUpdate();
                curr = curr.Next;
            }
        }
    }
}
