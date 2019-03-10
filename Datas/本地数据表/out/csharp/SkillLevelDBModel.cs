
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2018-10-11 13:06:32
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// SkillLevel数据管理
/// </summary>
public partial class SkillLevelDBModel : AbstractDBModel<SkillLevelDBModel, SkillLevelEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "SkillLevel.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override SkillLevelEntity MakeEntity(GameDataTableParser parse)
    {
        SkillLevelEntity entity = new SkillLevelEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.SkillId = parse.GetFieldValue("SkillId").ToInt();
        entity.Level = parse.GetFieldValue("Level").ToInt();
        entity.HurtValueRate = parse.GetFieldValue("HurtValueRate").ToInt();
        entity.SpendMP = parse.GetFieldValue("SpendMP").ToInt();
        entity.StateTime = parse.GetFieldValue("StateTime").ToFloat();
        entity.AbnormalRatio = parse.GetFieldValue("AbnormalRatio").ToFloat();
        entity.AStateTimes = parse.GetFieldValue("AStateTimes").ToInt();
        entity.AStatexiaohao = parse.GetFieldValue("AStatexiaohao").ToInt();
        entity.SkillCDTime = parse.GetFieldValue("SkillCDTime").ToFloat();
        entity.BuffChance = parse.GetFieldValue("BuffChance").ToFloat();
        entity.BuffDuration = parse.GetFieldValue("BuffDuration").ToFloat();
        entity.BuffValue = parse.GetFieldValue("BuffValue").ToInt();
        entity.NeedCharacterLevel = parse.GetFieldValue("NeedCharacterLevel").ToInt();
        entity.SpendGold = parse.GetFieldValue("SpendGold").ToInt();
        return entity;
    }
}
