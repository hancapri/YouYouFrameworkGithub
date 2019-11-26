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
        public static Dictionary<string, AssetBundleInfoEntity> GetAssetBundleVersionList(byte[] buffer, ref string version)
        {
            Dictionary<string, AssetBundleInfoEntity> res = new Dictionary<string, AssetBundleInfoEntity>();
            return res;
        }
    }
}
