//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-03-17 21:52:10
//备    注：
//===================================================
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// AssetBundle实体
/// </summary>
public class AssetBundleEntity
{
    /// <summary>
    /// 用于打包时候选定 唯一Key
    /// </summary>
    public string Key;

    /// <summary>
    /// 名称
    /// </summary>
    public string Name;

    /// <summary>
    /// 标记
    /// </summary>
    public string Tag;

    /// <summary>
    /// 是否文件夹
    /// </summary>
    public bool IsFolder;

    /// <summary>
    /// 是否初始资源
    /// </summary>
    public bool IsFirstData;

    /// <summary>
    /// 是否加密
    /// </summary>
    public bool IsEncrypt;

    /// <summary>
    /// 是否被选中
    /// </summary>
    public bool IsChecked;

    private List<string> m_PathList = new List<string>();

    /// <summary>
    /// 路径集合
    /// </summary>
    public List<string> PathList
    {
        get { return m_PathList; }
    }
}