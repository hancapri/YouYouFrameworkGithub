
//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-12-18 15:20:37
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// NPC数据管理
/// </summary>
public partial class NPCDBModel : AbstractDBModel<NPCDBModel, NPCEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "NPC.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override NPCEntity MakeEntity(GameDataTableParser parse)
    {
        NPCEntity entity = new NPCEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.PrefabName = parse.GetFieldValue("PrefabName");
        entity.HeadPic = parse.GetFieldValue("HeadPic");
        entity.HalfBodyPic = parse.GetFieldValue("HalfBodyPic");
        entity.Talk = parse.GetFieldValue("Talk");
        return entity;
    }
}
