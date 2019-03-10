
//===================================================
//作    者：边涯  http://www.u3dol.com  QQ群：87481002
//创建时间：2016-12-18 15:20:41
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Message数据管理
/// </summary>
public partial class MessageDBModel : AbstractDBModel<MessageDBModel, MessageEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "Message.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override MessageEntity MakeEntity(GameDataTableParser parse)
    {
        MessageEntity entity = new MessageEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Msg = parse.GetFieldValue("Msg");
        entity.Module = parse.GetFieldValue("Module");
        entity.Description = parse.GetFieldValue("Description");
        return entity;
    }
}
