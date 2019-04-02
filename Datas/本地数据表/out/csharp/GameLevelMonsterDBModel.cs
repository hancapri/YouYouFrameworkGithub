
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-04-02 14:51:31
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYouFramework;

/// <summary>
/// GameLevelMonster数据管理
/// </summary>
public partial class GameLevelMonsterDBModel : DataTableDBModelBase<GameLevelMonsterDBModel, GameLevelMonsterEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public override string DataTableName { get { return "GameLevelMonster"; } }

    /// <summary>
    /// 加载列表
    /// </summary>
    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            GameLevelMonsterEntity entity = new GameLevelMonsterEntity();
            entity.Id = ms.ReadInt();
            entity.GameLevelId = ms.ReadInt();
            entity.Grade = ms.ReadInt();
            entity.RegionId = ms.ReadInt();
            entity.SpriteId = ms.ReadInt();
            entity.SpriteCount = ms.ReadInt();
            entity.Exp = ms.ReadInt();
            entity.Gold = ms.ReadInt();
            entity.DropEquip = ms.ReadUTF8String();
            entity.DropItem = ms.ReadUTF8String();
            entity.DropMaterial = ms.ReadUTF8String();

            m_List.Add(entity);
            m_Dic[entity.Id] = entity;
        }
    }
}