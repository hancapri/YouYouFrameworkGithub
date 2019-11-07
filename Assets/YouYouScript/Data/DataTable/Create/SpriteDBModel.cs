
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-11-07 20:46:05
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYouFramework;

/// <summary>
/// Sprite数据管理
/// </summary>
public partial class SpriteDBModel : DataTableDBModelBase<SpriteDBModel, SpriteEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public override string DataTableName { get { return "Sprite"; } }

    /// <summary>
    /// 加载列表
    /// </summary>
    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            SpriteEntity entity = new SpriteEntity();
            entity.Id = ms.ReadInt();
            entity.SpriteType = ms.ReadInt();
            entity.Name = ms.ReadUTF8String();
            entity.Level = ms.ReadInt();
            entity.IsBoss = ms.ReadInt();
            entity.PrefabName = ms.ReadUTF8String();
            entity.TextureName = ms.ReadUTF8String();
            entity.HeadPic = ms.ReadUTF8String();
            entity.MoveSpeed = ms.ReadFloat();
            entity.HP = ms.ReadInt();
            entity.MP = ms.ReadInt();
            entity.Attack = ms.ReadInt();
            entity.Defense = ms.ReadInt();
            entity.Hit = ms.ReadInt();
            entity.Dodge = ms.ReadInt();
            entity.Cri = ms.ReadInt();
            entity.Res = ms.ReadInt();
            entity.Fighting = ms.ReadInt();
            entity.ShowBloodBar = ms.ReadInt();
            entity.BloodBarLayerCount = ms.ReadInt();
            entity.UsedPhyAttack = ms.ReadUTF8String();
            entity.UsedSkillList = ms.ReadUTF8String();
            entity.CanArmor = ms.ReadInt();
            entity.Armor_HP_Percentage = ms.ReadInt();
            entity.Range_View = ms.ReadInt();
            entity.Attack_Interval = ms.ReadFloat();
            entity.PhysicalAttackRate = ms.ReadInt();
            entity.DelaySec_Attack = ms.ReadFloat();
            entity.RewardExp = ms.ReadInt();

            m_List.Add(entity);
            m_Dic[entity.Id] = entity;
        }
    }
}