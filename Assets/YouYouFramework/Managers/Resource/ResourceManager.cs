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

        /// <summary>
        /// 可写区管理器
        /// </summary>
        public LocalAssetManager LocalAssetManager
        {
            get;
            private set;
        }

        public ResourceManager()
        {
            StreamingAssetsManager = new StreamingAssetsManager();
            LocalAssetManager = new LocalAssetManager();
        }

        #region 只读区 StreamingAssets文件夹
        /// <summary>
        /// 只读区版本号
        /// </summary>
        private string m_StreamingAssetsVersion;

        /// <summary>
        /// 只读区资源包信息
        /// </summary>
        private Dictionary<string, AssetBundleInfoEntity> m_StreamingAssetsVersionDic;

        /// <summary>
        /// 是否存在只读区资源包信息
        /// </summary>
        private bool m_IsExistsStreamingAssetBundleInfo = false;

        /// <summary>
        /// 初始化只读区资源信息
        /// </summary>
        public void InitStreamingAssetsBundleInfo()
        {
            GameEntry.Log(LogCategory.Resource, "InitStreamingAssetsBundleInfo");
            ReadStreamingAssetsBundle("VersionFile.bytes",(byte[] buffer)=>
            {
                if (buffer == null)
                {
                    InitCDNAssetBundleInfo();
                }
                else
                {
                    m_IsExistsStreamingAssetBundleInfo = true;
                    m_StreamingAssetsVersionDic = GetAssetBundleVersionList(buffer, ref m_StreamingAssetsVersion);
                    InitCDNAssetBundleInfo();
                }
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

        #region CDN网络站点
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
            GameEntry.Log(LogCategory.Resource, "InitCDNAssetBundleInfo");

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
                CheckVersionFileExistsInLocal();
            }
            else
            {
                GameEntry.Log(LogCategory.Resource, args.Value);
            }
        }

        #endregion

        #region 可写区 persistentDataPath文件夹
        /// <summary>
        /// 可写区版本号
        /// </summary>
        private string m_LocalAssetsVersion;

        /// <summary>
        /// 可写区资源包信息
        /// </summary>
        private Dictionary<string, AssetBundleInfoEntity> m_LocalAssetsVersionDic;

        /// <summary>
        /// 检查可写区版本文件是否存在
        /// </summary>
        private void CheckVersionFileExistsInLocal()
        {
            GameEntry.Log(LogCategory.Resource, "CheckVersionFileExistsInLocal");

            if (LocalAssetManager.GetVersionFileExists())
            {
                //可写区版本文件存在
                //加载可写区资源包信息
                InitLocalAssetBundleInfo();
            }
            else
            {
                //可写区版本文件不存在
                //判断只读区版本文件是否存在
                if (m_IsExistsStreamingAssetBundleInfo)
                {
                    //只读区版本文件存在
                    //将只读区版本文件初始化到可写区
                    InitVersionFileFormStreamingAssetsToLocal();
                }
                CheckVersionChange();
            }
        }

        /// <summary>
        /// 初始化可写区资源包信息
        /// </summary>
        private void InitLocalAssetBundleInfo()
        {
            GameEntry.Log(LogCategory.Resource, "InitLocalAssetBundleInfo");
            m_LocalAssetsVersionDic = LocalAssetManager.GetAssetBundleVersionList(ref m_LocalAssetsVersion);
            CheckVersionChange();
        }

        /// <summary>
        /// 将只读区版本文件初始化到可写区
        /// </summary>
        private void InitVersionFileFormStreamingAssetsToLocal()
        {
            GameEntry.Log(LogCategory.Resource, "InitVersionFileFormStreamingAssetsToLocal");

            m_LocalAssetsVersionDic = new Dictionary<string, AssetBundleInfoEntity>();
            var enumerator = m_StreamingAssetsVersionDic.GetEnumerator();
            while (enumerator.MoveNext())
            {
                AssetBundleInfoEntity entity = enumerator.Current.Value;
                m_LocalAssetsVersionDic[enumerator.Current.Key] = new AssetBundleInfoEntity()
                {
                    AssetBundleName = entity.AssetBundleName,
                    MD5 = entity.MD5,
                    Size = entity.Size,
                    IsFirstData = entity.IsFirstData,
                    IsEncrypt = entity.IsEncrypt
                };
            }

            //保存版本文件
            LocalAssetManager.SaveVersionFile(m_LocalAssetsVersionDic);

            //保存版本号
            m_LocalAssetsVersion = m_StreamingAssetsVersion;
            LocalAssetManager.SetResourceVersion(m_LocalAssetsVersion);
        }
        #endregion

        /// <summary>
        /// 检查更新
        /// </summary>
        private void CheckVersionChange()
        {
            GameEntry.Log(LogCategory.Resource, "CheckVersionChange");

            if (LocalAssetManager.GetVersionFileExists())
            {
                //判断只读区资源版本号和CDN资源版本号
                if (m_StreamingAssetsVersion.Equals(m_CDNVersion))
                {
                    GameEntry.Log(LogCategory.Resource, "只读区资源版本号和CDN资源版本号一致");
                    //一致，直接进入预加载流程
                    GameEntry.Procedure.ChangeState(ProcedureState.Preload);
                }
                else
                {
                    GameEntry.Log(LogCategory.Resource, "只读区版本号和CDN版本号不一致");
                    //TODO: 不一致 开始检查更新

                    //最后进入预加载流程
                }
            }
            else
            {
                //TODO: 下载初始资源
            }
        }


        public void Dispose()
        {
            m_StreamingAssetsVersionDic.Clear();
        }
    }
}
