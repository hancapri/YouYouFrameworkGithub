
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-04-14 21:59:45
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYouFramework;

/// <summary>
/// SkillLevel数据管理
/// </summary>
public partial class SkillLevelDBModel : DataTableDBModelBase<SkillLevelDBModel, SkillLevelEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public override string DataTableName { get { return "SkillLevel"; } }

    /// <summary>
    /// 加载列表
    /// </summary>
    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            SkillLevelEntity entity = new SkillLevelEntity();
            entity.Id = ms.ReadInt();
            entity.SkillId = ms.ReadInt();
            entity.Level = ms.ReadInt();
            entity.HurtValueRate = ms.ReadInt();
            entity.SpendMP = ms.ReadInt();
            entity.StateTime = ms.ReadFloat();
            entity.AbnormalRatio = ms.ReadFloat();
            entity.AStateTimes = ms.ReadInt();
            entity.AStatexiaohao = ms.ReadInt();
            entity.SkillCDTime = ms.ReadFloat();
            entity.BuffChance = ms.ReadFloat();
            entity.BuffDuration = ms.ReadFloat();
            entity.BuffValue = ms.ReadInt();
            entity.NeedCharacterLevel = ms.ReadInt();
            entity.SpendGold = ms.ReadInt();

            m_List.Add(entity);
            m_Dic[entity.Id] = entity;
        }
    }
}