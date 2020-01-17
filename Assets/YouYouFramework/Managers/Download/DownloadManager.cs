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

        public DownloadManager()
        {
            m_DownloadRoutineList = new LinkedList<DownloadRoutine>();
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="url"></param>
        /// <param name="onUpdate"></param>
        /// <param name="onComplete"></param>
        public void BeginDownload(string url,BaseAction<float> onUpdate = null, BaseAction<string> onComplete = null)
        {
            DownloadRoutine routine = GameEntry.Pool.DequeueClassObject<DownloadRoutine>();
            routine.BeginDownload(url, onUpdate, onComplete:(string fileUrl)=>
            {
                m_DownloadRoutineList.Remove(routine);

                if (onComplete != null)
                {
                    onComplete(fileUrl);
                }
            });
            m_DownloadRoutineList.AddLast(routine);
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
