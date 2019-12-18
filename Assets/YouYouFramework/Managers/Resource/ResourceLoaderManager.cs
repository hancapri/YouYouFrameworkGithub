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

        /// <summary>
        /// 资源包加载器链表
        /// </summary>
        private LinkedList<AssetBundleLoaderRoutine> m_AssetBundleLoaderList;

        /// <summary>
        /// 资源加载器链表
        /// </summary>
        private LinkedList<AssetLoaderRoutine> m_AssetLoaderList;

        public ResourceLoaderManager()
        {
            m_AssetInfoDic = new Dictionary<AssetCategory, Dictionary<string, AssetEntity>>();
            m_AssetBundleLoaderList = new LinkedList<AssetBundleLoaderRoutine>();
            m_AssetLoaderList = new LinkedList<AssetLoaderRoutine>();
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

        #region InitAssetInfo 初始化资源信息
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
        #endregion


        /// <summary>
        /// 加载ab包
        /// </summary>
        /// <param name="assetBundlePath"></param>
        /// <param name="onUpdate"></param>
        /// <param name="onComplete"></param>
        public void LoadAssetBundle(string assetBundlePath, Action<float> onUpdate = null, Action<AssetBundle> onComplete = null)
        {
            //1.判断资源是否在资源池中
            ResourceEntity assetBundleEntity = GameEntry.Pool.PoolManager.AssetBundlePool.Spawn(assetBundlePath);
            if (assetBundleEntity != null)
            {
                //说明ab包在资源池中存在
                AssetBundle assetbundle = assetBundleEntity.Target as AssetBundle;
                Debug.LogError("从资源池中加载AssetBundle");
                if (onComplete != null)
                {
                    onComplete(assetbundle);
                }
                return;
            }

            //2.没有再加载
            AssetBundleLoaderRoutine routine = GameEntry.Pool.DequeueClassObject<AssetBundleLoaderRoutine>();
            if (routine == null)
            {
                routine = new AssetBundleLoaderRoutine();
            }

            m_AssetBundleLoaderList.AddLast(routine);

            routine.LoadAssetBundle(assetBundlePath);
            routine.OnAssetBundleCreateUpdate = (float progress) =>
            {
                if (onUpdate != null)
                {
                    onUpdate(progress);
                }
            };

            routine.OnLoadAssetBundleComplete = (AssetBundle assetBundle) =>
            {
                //将ab包注册进资源池
                assetBundleEntity = GameEntry.Pool.DequeueClassObject<ResourceEntity>();
                assetBundleEntity.ResourceName = assetBundlePath;
                assetBundleEntity.IsAssetBundle = true;
                assetBundleEntity.Target = assetBundle;
                GameEntry.Pool.PoolManager.AssetBundlePool.Register(assetBundleEntity);

                Debug.LogError("把加载的ab包加入资源池");


                if (onComplete != null)
                {
                    onComplete(assetBundle);
                }

                //结束回池
                m_AssetBundleLoaderList.Remove(routine);
                GameEntry.Pool.EnqueueClassObject(routine);
            };
        }

        /// <summary>
        /// 加载资源
        /// </summary>
        /// <param name="assetName"></param>
        /// <param name="assetBundle"></param>
        /// <param name="onUpdate"></param>
        /// <param name="onComplete"></param>
        public void LoadAsset(string assetName, AssetBundle assetBundle, Action<float> onUpdate = null, Action<UnityEngine.Object> onComplete = null)
        {
            AssetLoaderRoutine routine = GameEntry.Pool.DequeueClassObject<AssetLoaderRoutine>();
            if (routine == null)
            {
                routine = new AssetLoaderRoutine();
            }

            //加入链表
            m_AssetLoaderList.AddLast(routine);

            routine.LoadAsset(assetName, assetBundle);
            routine.OnAssetUpdate = (float progress) =>
            {
                if (onUpdate != null)
                {
                    onUpdate(progress);
                }
            };
            routine.OnLoadAssetComplete = (UnityEngine.Object obj) =>
            {
                if (onComplete != null)
                {
                    onComplete(obj);
                }

                //结束循环 回池
                m_AssetLoaderList.Remove(routine);
                GameEntry.Pool.EnqueueClassObject(routine);
            };
        }

        public void OnUpdate()
        {
            for (LinkedListNode<AssetBundleLoaderRoutine> curr = m_AssetBundleLoaderList.First; curr != null; curr = curr.Next)
            {
                curr.Value.OnUpdate();
            }

            for (LinkedListNode<AssetLoaderRoutine> curr = m_AssetLoaderList.First; curr != null; curr = curr.Next)
            {
                curr.Value.OnUpdate();
            }
        }

        public void Dispose()
        {
            
        }
    }
}
