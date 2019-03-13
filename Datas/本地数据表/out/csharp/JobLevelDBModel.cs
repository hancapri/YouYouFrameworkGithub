
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-03-13 22:23:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYouFramework;

/// <summary>
/// JobLevel数据管理
/// </summary>
public partial class JobLevelDBModel : DataTableDBModelBase<JobLevelDBModel, JobLevelEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public override string DataTableName { get { return "JobLevel"; } }

    /// <summary>
    /// 加载列表
    /// </summary>
    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            JobLevelEntity entity = new JobLevelEntity();
            entity.Id = ms.ReadInt();
            entity.Level = ms.ReadInt();
            entity.NeedExp = ms.ReadInt();
            entity.Energy = ms.ReadInt();
            entity.HP = ms.ReadInt();
            entity.MP = ms.ReadInt();
            entity.Attack = ms.ReadInt();
            entity.Defense = ms.ReadInt();
            entity.Hit = ms.ReadInt();
            entity.Dodge = ms.ReadInt();
            entity.Cri = ms.ReadInt();
            entity.Res = ms.ReadInt();

            m_List.Add(entity);
            m_Dic[entity.Id] = entity;
        }
    }
}