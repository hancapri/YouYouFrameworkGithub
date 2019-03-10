
//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2018-01-28 16:15:56
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Item数据管理
/// </summary>
public partial class ItemDBModel : AbstractDBModel<ItemDBModel, ItemEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Item.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override ItemEntity MakeEntity(GameDataTableParser parse)
    {
        ItemEntity entity = new ItemEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.Type = parse.GetFieldValue("Type").ToInt();
        entity.UsedLevel = parse.GetFieldValue("UsedLevel").ToInt();
        entity.UsedMethod = parse.GetFieldValue("UsedMethod");
        entity.SellMoney = parse.GetFieldValue("SellMoney").ToInt();
        entity.Quality = parse.GetFieldValue("Quality").ToInt();
        entity.Description = parse.GetFieldValue("Description");
        entity.UsedItems = parse.GetFieldValue("UsedItems");
        entity.maxAmount = parse.GetFieldValue("maxAmount").ToInt();
        entity.packSort = parse.GetFieldValue("packSort").ToInt();
        return entity;
    }
}
