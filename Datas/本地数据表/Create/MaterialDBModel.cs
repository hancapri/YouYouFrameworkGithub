
//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2018-01-28 16:16:00
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Material数据管理
/// </summary>
public partial class MaterialDBModel : AbstractDBModel<MaterialDBModel, MaterialEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Material.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override MaterialEntity MakeEntity(GameDataTableParser parse)
    {
        MaterialEntity entity = new MaterialEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.Quality = parse.GetFieldValue("Quality").ToInt();
        entity.Description = parse.GetFieldValue("Description");
        entity.Type = parse.GetFieldValue("Type").ToInt();
        entity.FixedType = parse.GetFieldValue("FixedType").ToInt();
        entity.FixedAddValue = parse.GetFieldValue("FixedAddValue").ToInt();
        entity.maxAmount = parse.GetFieldValue("maxAmount").ToInt();
        entity.packSort = parse.GetFieldValue("packSort").ToInt();
        entity.CompositionProps = parse.GetFieldValue("CompositionProps");
        entity.CompositionMaterialID = parse.GetFieldValue("CompositionMaterialID").ToInt();
        entity.CompositionGold = parse.GetFieldValue("CompositionGold");
        entity.SellMoney = parse.GetFieldValue("SellMoney").ToInt();
        return entity;
    }
}
