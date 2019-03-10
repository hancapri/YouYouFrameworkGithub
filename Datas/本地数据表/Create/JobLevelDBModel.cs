
//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2017-11-08 16:37:40
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// JobLevel数据管理
/// </summary>
public partial class JobLevelDBModel : AbstractDBModel<JobLevelDBModel, JobLevelEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "JobLevel.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override JobLevelEntity MakeEntity(GameDataTableParser parse)
    {
        JobLevelEntity entity = new JobLevelEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Level = parse.GetFieldValue("Level").ToInt();
        entity.NeedExp = parse.GetFieldValue("NeedExp").ToInt();
        entity.Energy = parse.GetFieldValue("Energy").ToInt();
        entity.HP = parse.GetFieldValue("HP").ToInt();
        entity.MP = parse.GetFieldValue("MP").ToInt();
        entity.Attack = parse.GetFieldValue("Attack").ToInt();
        entity.Defense = parse.GetFieldValue("Defense").ToInt();
        entity.Hit = parse.GetFieldValue("Hit").ToInt();
        entity.Dodge = parse.GetFieldValue("Dodge").ToInt();
        entity.Cri = parse.GetFieldValue("Cri").ToInt();
        entity.Res = parse.GetFieldValue("Res").ToInt();
        return entity;
    }
}
