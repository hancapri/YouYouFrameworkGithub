using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// AB包实体
/// </summary>
public class AssetBundleInfoEntity
{
    /// <summary>
    /// AB包名称
    /// </summary>
    public string AssetBundleName;
    /// <summary>
    /// AB的MD5值
    /// </summary>
    public string MD5;
    /// <summary>
    /// AB包大小
    /// </summary>
    public int Size;
    /// <summary>
    /// 是否为初始化资源
    /// </summary>
    public bool IsFirstData;
    /// <summary>
    /// 是否加密
    /// </summary>
    public bool IsEncrypt;
}
