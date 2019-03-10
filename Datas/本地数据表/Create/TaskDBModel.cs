
//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2017-06-24 19:11:06
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Task数据管理
/// </summary>
public partial class TaskDBModel : AbstractDBModel<TaskDBModel, TaskEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Task.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override TaskEntity MakeEntity(GameDataTableParser parse)
    {
        TaskEntity entity = new TaskEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.Status = parse.GetFieldValue("Status").ToInt();
        entity.Content = parse.GetFieldValue("Content");
        return entity;
    }
}
