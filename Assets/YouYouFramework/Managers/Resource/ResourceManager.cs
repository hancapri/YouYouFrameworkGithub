using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 场景管理器
    /// </summary>
    public class ResourceManager : ManagerBase
    {
        #region  GetAssetBundleVersionList 根据字节数组获取资源包版本信息
        /// <summary>
        /// 根据字节数组获取资源包版本信息
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="version">版本信息</param>
        /// <returns></returns>
        public static Dictionary<string, AssetBundleInfoEntity> GetAssetBundleVersionList(byte[] buffer, ref string version)
        {
            buffer = ZlibHelper.DeCompressBytes(buffer);

            Dictionary<string, AssetBundleInfoEntity> dic = new Dictionary<string, AssetBundleInfoEntity>();

            MMO_MemoryStream ms = new MMO_MemoryStream(buffer);
            int len = ms.ReadInt();
            for (int i = 0; i < len; i++)
            {
                if (i == 0)
                {
                    version = ms.ReadUTF8String().Trim();
                }
                else
                {
                    AssetBundleInfoEntity entity = new AssetBundleInfoEntity();
                    entity.AssetBundleName = ms.ReadUTF8String();
                    entity.MD5 = ms.ReadUTF8String();
                    entity.Size = ms.ReadULong();
                    entity.IsFirstData = ms.ReadByte() == 1;
                    entity.IsEncrypt = ms.ReadByte() == 1;
                    dic[entity.AssetBundleName] = entity;
                }
            }
            return dic;
        }
        #endregion

        /// <summary>
        /// StreamingAssets管理器
        /// </summary>
        public StreamingAssetsManager StreamingAssetsManager
        {
            get;
            private set;
        }

        public ResourceManager()
        {
            StreamingAssetsManager = new StreamingAssetsManager();
        }

        #region 只读区
        /// <summary>
        /// 只读区版本号
        /// </summary>
        private string m_StreamingAssetsVersion;

        /// <summary>
        /// 只读区资源包信息
        /// </summary>
        private Dictionary<string, AssetBundleInfoEntity> m_StreamingAssetsVersionDic;

        /// <summary>
        /// 初始化只读区资源信息
        /// </summary>
        public void InitStreamingAssetsBundleInfo()
        {
            ReadStreamingAssetsBundle("VersionFile.bytes",(byte[] buffer)=>
            {
                m_StreamingAssetsVersionDic = GetAssetBundleVersionList(buffer,ref m_StreamingAssetsVersion);
                GameEntry.Log(LogCategory.Resource,"只读区版本号："+ m_StreamingAssetsVersion);
                GameEntry.Log(LogCategory.Resource, "只读区资源个数：" + m_StreamingAssetsVersionDic.Count);
                foreach (var item in m_StreamingAssetsVersionDic)
                {
                    GameEntry.Log(LogCategory.Resource, item.Value.AssetBundleName);
                }

                InitCDNAssetBundleInfo();
            });
        }

        /// <summary>
        /// 读取只读区资源包
        /// </summary>
        /// <param name="fileUrl"></param>
        /// <param name="onComplete"></param>
        internal void ReadStreamingAssetsBundle(string fileUrl, Action<byte[]> onComplete)
        {
            StreamingAssetsManager.ReadAssetBundle(fileUrl,onComplete);
        }
        #endregion

        #region CDN
        /// <summary>
        /// CDN资源版本号
        /// </summary>
        private string m_CDNVersion;

        /// <summary>
        /// CDN资源包信息
        /// </summary>
        private Dictionary<string, AssetBundleInfoEntity> m_CDNVersionDic;

        /// <summary>
        /// 初始化CDN资源包信息
        /// </summary>
        private void InitCDNAssetBundleInfo()
        {
            string url = string.Format("{0}VersionFile.bytes",GameEntry.Data.SysDataManager.CurrChannelConfig.RealSourceUrl);
            GameEntry.Log(LogCategory.Resource, "url:" +url);
            GameEntry.Http.SendData(url,OnInitCDNAssetBundleInfo,isGetData:true);
        }

        /// <summary>
        /// 初始化CDN资源包信息回调
        /// </summary>
        /// <param name="args"></param>
        private void OnInitCDNAssetBundleInfo(HttpCallBackArgs args)
        {
            if (!args.HasError)
            {
                m_CDNVersionDic = GetAssetBundleVersionList(args.Data, ref m_CDNVersion);
                GameEntry.Log(LogCategory.Resource, "cdn资源包总数" + m_CDNVersionDic.Count);
                foreach (var item in m_CDNVersionDic)
                {
                    GameEntry.Log(LogCategory.Resource,item.Value.AssetBundleName);
                }
            }
            else
            {
                GameEntry.Log(LogCategory.Resource, args.Value);
            }
        }

        #endregion

        public void Dispose()
        {
            m_StreamingAssetsVersionDic.Clear();
        }
    }
}
