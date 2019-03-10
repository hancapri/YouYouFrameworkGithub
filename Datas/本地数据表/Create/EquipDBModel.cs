
//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2018-01-28 16:15:58
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Equip数据管理
/// </summary>
public partial class EquipDBModel : AbstractDBModel<EquipDBModel, EquipEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Equip.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override EquipEntity MakeEntity(GameDataTableParser parse)
    {
        EquipEntity entity = new EquipEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.UsedLevel = parse.GetFieldValue("UsedLevel").ToInt();
        entity.Quality = parse.GetFieldValue("Quality").ToInt();
        entity.Star = parse.GetFieldValue("Star").ToInt();
        entity.Description = parse.GetFieldValue("Description");
        entity.Type = parse.GetFieldValue("Type").ToInt();
        entity.SellMoney = parse.GetFieldValue("SellMoney").ToInt();
        entity.BackAttrOneType = parse.GetFieldValue("BackAttrOneType").ToInt();
        entity.BackAttrOneValue = parse.GetFieldValue("BackAttrOneValue").ToInt();
        entity.BackAttrTwoType = parse.GetFieldValue("BackAttrTwoType").ToInt();
        entity.BackAttrTwoValue = parse.GetFieldValue("BackAttrTwoValue").ToInt();
        entity.Attack = parse.GetFieldValue("Attack").ToInt();
        entity.Defense = parse.GetFieldValue("Defense").ToInt();
        entity.Hit = parse.GetFieldValue("Hit").ToInt();
        entity.Dodge = parse.GetFieldValue("Dodge").ToInt();
        entity.Cri = parse.GetFieldValue("Cri").ToInt();
        entity.Res = parse.GetFieldValue("Res").ToInt();
        entity.HP = parse.GetFieldValue("HP").ToInt();
        entity.MP = parse.GetFieldValue("MP").ToInt();
        entity.maxHole = parse.GetFieldValue("maxHole").ToInt();
        entity.embedProps = parse.GetFieldValue("embedProps");
        entity.StrengthenItem = parse.GetFieldValue("StrengthenItem").ToInt();
        entity.StrengthenLvMax = parse.GetFieldValue("StrengthenLvMax").ToInt();
        entity.StrengthenValue = parse.GetFieldValue("StrengthenValue");
        entity.StrengthenItemNumber = parse.GetFieldValue("StrengthenItemNumber");
        entity.StrengthenGold = parse.GetFieldValue("StrengthenGold");
        entity.StrengthenRatio = parse.GetFieldValue("StrengthenRatio");
        return entity;
    }
}
