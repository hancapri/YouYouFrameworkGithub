
//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2017-04-09 22:16:37
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Sprite数据管理
/// </summary>
public partial class SpriteDBModel : AbstractDBModel<SpriteDBModel, SpriteEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Sprite.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override SpriteEntity MakeEntity(GameDataTableParser parse)
    {
        SpriteEntity entity = new SpriteEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.SpriteType = parse.GetFieldValue("SpriteType").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.Level = parse.GetFieldValue("Level").ToInt();
        entity.IsBoss = parse.GetFieldValue("IsBoss").ToInt();
        entity.PrefabName = parse.GetFieldValue("PrefabName");
        entity.TextureName = parse.GetFieldValue("TextureName");
        entity.HeadPic = parse.GetFieldValue("HeadPic");
        entity.MoveSpeed = parse.GetFieldValue("MoveSpeed").ToFloat();
        entity.HP = parse.GetFieldValue("HP").ToInt();
        entity.MP = parse.GetFieldValue("MP").ToInt();
        entity.Attack = parse.GetFieldValue("Attack").ToInt();
        entity.Defense = parse.GetFieldValue("Defense").ToInt();
        entity.Hit = parse.GetFieldValue("Hit").ToInt();
        entity.Dodge = parse.GetFieldValue("Dodge").ToInt();
        entity.Cri = parse.GetFieldValue("Cri").ToInt();
        entity.Res = parse.GetFieldValue("Res").ToInt();
        entity.Fighting = parse.GetFieldValue("Fighting").ToInt();
        entity.ShowBloodBar = parse.GetFieldValue("ShowBloodBar").ToInt();
        entity.BloodBarLayerCount = parse.GetFieldValue("BloodBarLayerCount").ToInt();
        entity.UsedPhyAttack = parse.GetFieldValue("UsedPhyAttack");
        entity.UsedSkillList = parse.GetFieldValue("UsedSkillList");
        entity.CanArmor = parse.GetFieldValue("CanArmor").ToInt();
        entity.Armor_HP_Percentage = parse.GetFieldValue("Armor_HP_Percentage").ToInt();
        entity.Range_View = parse.GetFieldValue("Range_View").ToInt();
        entity.Attack_Interval = parse.GetFieldValue("Attack_Interval").ToFloat();
        entity.PhysicalAttackRate = parse.GetFieldValue("PhysicalAttackRate").ToInt();
        entity.DelaySec_Attack = parse.GetFieldValue("DelaySec_Attack").ToFloat();
        entity.RewardExp = parse.GetFieldValue("RewardExp").ToInt();
        return entity;
    }
}
