using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using YouYouFramework;

/// <summary>
/// Localization数据管理
/// </summary>
public class LocalizationDBModel: DataTableDBModelBase<ChapterDBModel, DataTableEntityBase>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public override string DataTableName { get { return "Localization/"+GameEntry.Localization.CurrLanguage.ToString(); } }

    public Dictionary<string, string> LocalizationDic = new Dictionary<string, string>();

    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();
        for (int i = 0; i < rows; i++)
        {
            LocalizationDic[ms.ReadUTF8String()] = ms.ReadUTF8String();
        }
    }
}
