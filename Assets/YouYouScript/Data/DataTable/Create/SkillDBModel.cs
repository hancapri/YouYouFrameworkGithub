
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-12-21 10:35:23
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYouFramework;

/// <summary>
/// Skill数据管理
/// </summary>
public partial class SkillDBModel : DataTableDBModelBase<SkillDBModel, SkillEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public override string DataTableName { get { return "Skill"; } }

    /// <summary>
    /// 加载列表
    /// </summary>
    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            SkillEntity entity = new SkillEntity();
            entity.Id = ms.ReadInt();
            entity.SkillName = ms.ReadUTF8String();
            entity.SkillDesc = ms.ReadUTF8String();
            entity.SkillPic = ms.ReadUTF8String();
            entity.LevelLimit = ms.ReadInt();
            entity.IsPhyAttack = ms.ReadInt();
            entity.AttackTargetCount = ms.ReadInt();
            entity.AttackRange = ms.ReadFloat();
            entity.AreaAttackRadius = ms.ReadFloat();
            entity.ShowHurtEffectDelaySecond = ms.ReadFloat();
            entity.RedScreen = ms.ReadInt();
            entity.AttackState = ms.ReadInt();
            entity.AbnormalState = ms.ReadInt();
            entity.BuffInfoID = ms.ReadInt();
            entity.BuffTargetFilter = ms.ReadInt();
            entity.BuffIsPercentage = ms.ReadInt();

            m_List.Add(entity);
            m_Dic[entity.Id] = entity;
        }
    }
}