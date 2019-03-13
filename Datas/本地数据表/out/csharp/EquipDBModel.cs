
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-03-12 15:13:50
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYouFramework;

/// <summary>
/// Equip数据管理
/// </summary>
public partial class EquipDBModel : DataTableDBModelBase<EquipDBModel, EquipEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string DataTableName { get { return "Equip"; } }

    /// <summary>
    /// 加载列表
    /// </summary>
    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            EquipEntity entity = new EquipEntity();
            entity.Id = ms.ReadInt();
            entity.Name = ms.ReadUTF8String();
            entity.UsedLevel = ms.ReadInt();
            entity.Quality = ms.ReadInt();
            entity.Star = ms.ReadInt();
            entity.Description = ms.ReadUTF8String();
            entity.Type = ms.ReadInt();
            entity.SellMoney = ms.ReadInt();
            entity.BackAttrOneType = ms.ReadInt();
            entity.BackAttrOneValue = ms.ReadInt();
            entity.BackAttrTwoType = ms.ReadInt();
            entity.BackAttrTwoValue = ms.ReadInt();
            entity.Attack = ms.ReadInt();
            entity.Defense = ms.ReadInt();
            entity.Hit = ms.ReadInt();
            entity.Dodge = ms.ReadInt();
            entity.Cri = ms.ReadInt();
            entity.Res = ms.ReadInt();
            entity.HP = ms.ReadInt();
            entity.MP = ms.ReadInt();
            entity.maxHole = ms.ReadInt();
            entity.embedProps = ms.ReadUTF8String();
            entity.StrengthenItem = ms.ReadInt();
            entity.StrengthenLvMax = ms.ReadInt();
            entity.StrengthenValue = ms.ReadUTF8String();
            entity.StrengthenItemNumber = ms.ReadUTF8String();
            entity.StrengthenGold = ms.ReadUTF8String();
            entity.StrengthenRatio = ms.ReadUTF8String();

            m_List.Add(entity);
            m_Dic[entity.Id] = entity;
        }
    }
}