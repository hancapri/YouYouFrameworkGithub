using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;

namespace YouYouFramework
{
    /// <summary>
    /// 下载器
    /// </summary>
    public class DownloadRoutine
    {
        /// <summary>
        /// web请求
        /// </summary>
        private UnityWebRequest m_UnityWebRequest = null;

        /// <summary>
        /// 文件流
        /// </summary>
        private FileStream m_FileStream;

        /// <summary>
        /// 当前等待写入磁盘的大小
        /// </summary>
        private int m_CurrWaitFlushSize = 0;

        /// <summary>
        /// 上次写入的大小
        /// </summary>
        private int m_PrevWriteSize = 0;

        /// <summary>
        /// 总大小
        /// </summary>
        private ulong m_TotalSize;

        /// <summary>
        /// 当前下载的大小
        /// </summary>
        private ulong m_CurrDownloadedSize = 0;

        /// <summary>
        /// 起始位置
        /// </summary>
        private uint m_BeginPos = 0;

        /// <summary>
        /// 当前文件路径
        /// </summary>
        private string m_CurrFileUrl;

        /// <summary>
        /// 下载的本地文件路径
        /// </summary>
        private string m_DownloadLocalFilePath;

        /// <summary>
        /// 下载中委托
        /// </summary>
        private BaseAction<float> m_OnUpdate;

        /// <summary>
        /// 下载完毕委托
        /// </summary>
        private BaseAction<string> m_OnComplete;

        /// <summary>
        /// 开始下载
        /// </summary>
        public void BeginDownload(string url, BaseAction<float> onUpdate = null, BaseAction<string> onComplete = null)
        {
            m_OnUpdate = onUpdate;
            m_OnComplete = onComplete;

            m_CurrFileUrl = url;
            m_DownloadLocalFilePath = string.Format("{0}/{1}.temp",GameEntry.Resource.LocalFilePath,m_CurrFileUrl);
            Debug.Log("m_DownloadLocalFilePath:"+ m_DownloadLocalFilePath);
            if (File.Exists(m_DownloadLocalFilePath))
            {
                m_FileStream = File.OpenWrite(m_DownloadLocalFilePath);
                m_FileStream.Seek(0, SeekOrigin.End);

                m_BeginPos = (uint)m_FileStream.Length;

                Debug.LogError("继续下载beginpos = " + m_BeginPos);

                Download(string.Format("{0}/{1}", GameEntry.Data.SysDataManager.CurrChannelConfig.RealSourceUrl, m_CurrFileUrl), m_BeginPos);
            }
            else
            {
                string directory = Path.GetDirectoryName(m_DownloadLocalFilePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                m_FileStream = new FileStream(m_DownloadLocalFilePath,FileMode.Create,FileAccess.Write);

                Debug.LogError("从头下载");

                Download(string.Format("{0}/{1}", GameEntry.Data.SysDataManager.CurrChannelConfig.RealSourceUrl, m_CurrFileUrl));
            }
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="url"></param>
        private void Download(string url)
        {
            Debug.Log("下载:" + url);
            m_UnityWebRequest = UnityWebRequest.Get(url);
            m_UnityWebRequest.Send();
        }

        /// <summary>
        /// 下载
        /// </summary>
        /// <param name="url"></param>
        /// <param name="beginPos"></param>
        private void Download(string url, uint beginPos)
        {
            m_UnityWebRequest = UnityWebRequest.Get(url);
            m_UnityWebRequest.SetRequestHeader("Range", string.Format("bytes={0}-",beginPos.ToString()));
            m_UnityWebRequest.Send();
        }

        public void Reset()
        {
            if (m_UnityWebRequest != null)
            {
                m_UnityWebRequest.Dispose();
                m_UnityWebRequest = null;
            }
            if (m_FileStream != null)
            {
                m_FileStream.Close();
                m_FileStream.Dispose();
                m_FileStream = null;
            }

            m_PrevWriteSize = 0;
            m_TotalSize = 0;
            m_CurrDownloadedSize = 0;
            m_CurrWaitFlushSize = 0;
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void OnUpdate()
        {
            if (m_UnityWebRequest == null)
            {
                return;
            }

            if (m_TotalSize == 0)
            {
                m_TotalSize = 0;
                ulong.TryParse(m_UnityWebRequest.GetResponseHeader("Content-Length"),out m_TotalSize);
            }
            
            if (!m_UnityWebRequest.isDone)
            {
                if (m_CurrDownloadedSize < m_UnityWebRequest.downloadedBytes)
                {
                    m_CurrDownloadedSize = m_UnityWebRequest.downloadedBytes;
                    Debug.LogError(string.Format("下载进度{0}%",(int)(((m_CurrDownloadedSize/(float)m_TotalSize))*100)));

                    this.Save(m_UnityWebRequest.downloadHandler.data);

                    if (m_OnUpdate != null)
                    {
                        m_OnUpdate(m_CurrDownloadedSize / (float)m_TotalSize);
                    }
                }
                return;
            }

            if (m_UnityWebRequest.isNetworkError)
            {
                //下载失败
                //m_UnityWebRequest.error
                Debug.LogError("下载失败");
                Reset();
            }
            else
            {
                m_CurrDownloadedSize = m_UnityWebRequest.downloadedBytes;
                this.Save(m_UnityWebRequest.downloadHandler.data,true);

                if (m_OnUpdate != null)
                {
                    m_OnUpdate(m_CurrDownloadedSize/(float)m_TotalSize);
                }

                Reset();
                File.Move(m_DownloadLocalFilePath,m_DownloadLocalFilePath.Replace(".temp",""));
                m_DownloadLocalFilePath = null;

                if (m_OnComplete != null)
                {
                    m_OnComplete(m_CurrFileUrl);
                }
                m_CurrFileUrl = null;

                Debug.LogError("下载完毕");
            }
        }

        private void Save(byte[] buffer, bool downloadComplete = false)
        {
            if (buffer == null)
            {
                return;
            }

            int len = buffer.Length;
            int count = len - m_PrevWriteSize;
            m_FileStream.Write(buffer,m_PrevWriteSize,count);
            m_PrevWriteSize = len;

            m_CurrWaitFlushSize += count;
            if (m_CurrWaitFlushSize >= GameEntry.Download.FlushSize || downloadComplete)
            {
                Debug.LogError("写入磁盘");
                m_CurrWaitFlushSize = 0;
                m_FileStream.Flush();
            }
        }
    }
}
