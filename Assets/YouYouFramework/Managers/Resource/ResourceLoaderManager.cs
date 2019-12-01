using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 资源加载管理器
    /// </summary>
    public class ResourceLoaderManager : ManagerBase, IDisposable
    {
        /// <summary>
        /// 资源信息字典
        /// </summary>
        private Dictionary<AssetCategory, Dictionary<string, AssetEntity>> m_AssetInfoDic;

        public ResourceLoaderManager()
        {
            m_AssetInfoDic = new Dictionary<AssetCategory, Dictionary<string, AssetEntity>>();

            //游戏一开始时初始化分类字典
            var enumerator = Enum.GetValues(typeof(AssetCategory)).GetEnumerator();
            while (enumerator.MoveNext())
            {
                AssetCategory assetCategory = (AssetCategory)enumerator.Current;
                m_AssetInfoDic[assetCategory] = new Dictionary<string, AssetEntity>();
            }
        }

        /// <summary>
        /// 初始化资源信息
        /// </summary>
        public void InitAssetInfo()
        {
            byte[] buffer = GameEntry.Resource.ResourceManager.LocalAssetManager.GetFileBuffer(ConstDefine.AssetInfoName);
            if (buffer == null)
            {
                //如果可写区没有 那么就从只读区获取
                GameEntry.Resource.ResourceManager.StreamingAssetsManager.ReadAssetBundle(ConstDefine.AssetInfoName, (byte[] buff) =>
                 {
                     if (buff == null)
                     {
                         //如果只读区也没有，从CDN读取
                         string url = string.Format("{0}{1}", GameEntry.Data.SysDataManager.CurrChannelConfig.RealSourceUrl, ConstDefine.AssetInfoName);
                         GameEntry.Http.SendData(url, OnLoadAssetInfoFromCDN, isGetData: true);
                     }
                     else
                     {
                         InitAssetInfo(buff);
                     }
                 });
            }
            else
            {
                Debug.Log(3);
                InitAssetInfo(buffer);
            }
        }

        /// <summary>
        /// 从CDN加载资源信息
        /// </summary>
        /// <param name="args"></param>
        private void OnLoadAssetInfoFromCDN(HttpCallBackArgs args)
        {
            if (!args.HasError)
            {
                InitAssetInfo(args.Data);
            }
            else
            {
                GameEntry.Log(LogCategory.Resource, args.Value);
            }
        }

        /// <summary>
        /// 初始化资源信息，赋值字典
        /// </summary>
        /// <param name="buffer"></param>
        private void InitAssetInfo(byte[] buffer)
        {
            buffer = ZlibHelper.DeCompressBytes(buffer);

            MMO_MemoryStream ms = new MMO_MemoryStream(buffer);
            int len = ms.ReadInt();
            int depLen = 0;
            for (int i = 0; i < len; i++)
            {
                AssetEntity entity = new AssetEntity();
                entity.Category = (AssetCategory)ms.ReadByte();
                entity.AssetFullName = ms.ReadUTF8String();
                entity.AssetBundleName = ms.ReadUTF8String();
                depLen = ms.ReadInt();
                if (depLen > 0)
                {
                    entity.DependsAssetList = new List<AssetDependsEntity>();
                    for (int j = 0; j < depLen; j++)
                    {
                        AssetDependsEntity dep = new AssetDependsEntity();
                        dep.Category = (AssetCategory)ms.ReadByte();
                        dep.AssetFullName = ms.ReadUTF8String();
                        entity.DependsAssetList.Add(dep);
                    }
                }

                m_AssetInfoDic[entity.Category][entity.AssetFullName] = entity;
            }
        }

        public void Dispose()
        {
            
        }
    }
}
