
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-03-11 23:19:14
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYou;

/// <summary>
/// Job数据管理
/// </summary>
public partial class JobDBModel : DataTableDBModelBase<JobDBModel, JobEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public override string DataTableName { get { return "Job"; } }

    /// <summary>
    /// 加载列表
    /// </summary>
    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            JobEntity entity = new JobEntity();
            entity.Id = ms.ReadInt();
            entity.Name = ms.ReadUTF8String();
            entity.HeadPic = ms.ReadUTF8String();
            entity.JobPic = ms.ReadUTF8String();
            entity.PrefabName = ms.ReadUTF8String();
            entity.Desc = ms.ReadUTF8String();
            entity.Attack = ms.ReadInt();
            entity.Defense = ms.ReadInt();
            entity.Hit = ms.ReadInt();
            entity.Dodge = ms.ReadInt();
            entity.Cri = ms.ReadInt();
            entity.Res = ms.ReadInt();
            entity.UsedPhyAttackIds = ms.ReadUTF8String();
            entity.UsedSkillIds = ms.ReadUTF8String();

            m_List.Add(entity);
            m_Dic[entity.Id] = entity;
        }
    }
}