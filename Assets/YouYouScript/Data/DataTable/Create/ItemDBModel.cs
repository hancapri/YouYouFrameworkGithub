
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-11-06 23:02:27
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYouFramework;

/// <summary>
/// Item数据管理
/// </summary>
public partial class ItemDBModel : DataTableDBModelBase<ItemDBModel, ItemEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public override string DataTableName { get { return "Item"; } }

    /// <summary>
    /// 加载列表
    /// </summary>
    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            ItemEntity entity = new ItemEntity();
            entity.Id = ms.ReadInt();
            entity.Name = ms.ReadUTF8String();
            entity.Type = ms.ReadInt();
            entity.UsedLevel = ms.ReadInt();
            entity.UsedMethod = ms.ReadUTF8String();
            entity.SellMoney = ms.ReadInt();
            entity.Quality = ms.ReadInt();
            entity.Description = ms.ReadUTF8String();
            entity.UsedItems = ms.ReadUTF8String();
            entity.maxAmount = ms.ReadInt();
            entity.packSort = ms.ReadInt();

            m_List.Add(entity);
            m_Dic[entity.Id] = entity;
        }
    }
}