using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 资源组件
    /// </summary>
    public class ResourceComponent : YouYouBaseComponent, IUpdateComponent
    {
        /// <summary>
        /// 本地文件路径
        /// </summary>
        public string LocalFilePath;

        /// <summary>
        /// 资源管理器
        /// </summary>
        public ResourceManager ResourceManager
        {
            get;
            private set;
        }      

        /// <summary>
        /// 资源加载管理器
        /// </summary>
        public ResourceLoaderManager ResourceLoaderManager
        {
            get;
            private set;
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            GameEntry.RegisterUpdateComponent(this);

            ResourceManager = new ResourceManager();
            ResourceLoaderManager = new ResourceLoaderManager();
#if DISABLE_ASSETBUNDLE
            LocalFilePath = Application.dataPath;
#else
            LocalFilePath = Application.persistentDataPath;
#endif
        }

        /// <summary>
        /// 初始化只读区资源包信息
        /// </summary>
        public void InitStreamingAssetsBundleInfo()
        {
            ResourceManager.InitStreamingAssetsBundleInfo();
        }

        /// <summary>
        /// 读取本地文件到byte数组
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public byte[] GetFileBuffer(string path)
        {
            byte[] buffer = null;

            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
            }
            return buffer;
        }

        /// <summary>
        /// 初始化所以资源的信息
        /// </summary>
        public void InitAssetInfo()
        {
            ResourceLoaderManager.InitAssetInfo();
        }

        /// <summary>
        /// 获取路径的最后名称
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetLastPathName(string path)
        {
            if (path.IndexOf("/") == -1)
            {
                return path;
            }
            return path.Substring(path.LastIndexOf("/")+1);
        }

        public void OnUpdate()
        {
            ResourceLoaderManager.OnUpdate();
        }

        public override void Shutdown()
        {
            ResourceManager.Dispose();
            ResourceLoaderManager.Dispose();

            GameEntry.RemoveUpdateComponent(this);
        }
    }
}
