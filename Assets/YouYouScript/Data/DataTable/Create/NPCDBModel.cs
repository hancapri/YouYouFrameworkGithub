
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-12-21 10:35:22
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYouFramework;

/// <summary>
/// NPC数据管理
/// </summary>
public partial class NPCDBModel : DataTableDBModelBase<NPCDBModel, NPCEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public override string DataTableName { get { return "NPC"; } }

    /// <summary>
    /// 加载列表
    /// </summary>
    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            NPCEntity entity = new NPCEntity();
            entity.Id = ms.ReadInt();
            entity.Name = ms.ReadUTF8String();
            entity.PrefabName = ms.ReadUTF8String();
            entity.HeadPic = ms.ReadUTF8String();
            entity.HalfBodyPic = ms.ReadUTF8String();
            entity.Talk = ms.ReadUTF8String();

            m_List.Add(entity);
            m_Dic[entity.Id] = entity;
        }
    }
}