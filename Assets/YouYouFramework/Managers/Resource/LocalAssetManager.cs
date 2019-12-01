using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace YouYouFramework
{
    /// <summary>
    /// 可写区管理器
    /// </summary>
    public class LocalAssetManager
    {
        public string LocalVersionFilePath
        {
            get { return string.Format("{0}/{1}",Application.persistentDataPath, ConstDefine.VersionFileName); }
        }

        /// <summary>
        /// 获取可写区版本文件是否存在
        /// </summary>
        /// <returns></returns>
        public bool GetVersionFileExists()
        {
            return File.Exists(LocalVersionFilePath);
        }

        /// <summary>
        /// 获取本地文件字节数组
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public byte[] GetFileBuffer(string path)
        {
            return IOUtil.GetFileBuffer(string.Format("{0}/{1}",Application.persistentDataPath, path));
        }

        /// <summary>
        /// 保存资源版本号
        /// </summary>
        /// <param name="version"></param>
        public void SetResourceVersion(string version)
        {
            PlayerPrefs.SetString(ConstDefine.ResourceVersion, version);
        }

        /// <summary>
        /// 保存版本文件
        /// </summary>
        /// <param name="m_LocalAssetsVersionDic"></param>
        public void SaveVersionFile(Dictionary<string, AssetBundleInfoEntity> dic)
        {
            string json = JsonMapper.ToJson(dic);
            IOUtil.CreateTextFile(LocalVersionFilePath, json);
        }

        /// <summary>
        /// 加载可写区资源包信息
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public Dictionary<string, AssetBundleInfoEntity> GetAssetBundleVersionList(ref string version)
        {
            version = PlayerPrefs.GetString(ConstDefine.ResourceVersion);
            string json = IOUtil.GetFileText(LocalVersionFilePath);
            return JsonMapper.ToObject<Dictionary<string, AssetBundleInfoEntity>>(json);
        }
    }
}
