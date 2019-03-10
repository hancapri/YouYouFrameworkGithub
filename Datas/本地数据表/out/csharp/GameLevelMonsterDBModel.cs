
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2018-10-11 13:06:31
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// GameLevelMonster数据管理
/// </summary>
public partial class GameLevelMonsterDBModel : AbstractDBModel<GameLevelMonsterDBModel, GameLevelMonsterEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "GameLevelMonster.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override GameLevelMonsterEntity MakeEntity(GameDataTableParser parse)
    {
        GameLevelMonsterEntity entity = new GameLevelMonsterEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.GameLevelId = parse.GetFieldValue("GameLevelId").ToInt();
        entity.Grade = parse.GetFieldValue("Grade").ToInt();
        entity.RegionId = parse.GetFieldValue("RegionId").ToInt();
        entity.SpriteId = parse.GetFieldValue("SpriteId").ToInt();
        entity.SpriteCount = parse.GetFieldValue("SpriteCount").ToInt();
        entity.Exp = parse.GetFieldValue("Exp").ToInt();
        entity.Gold = parse.GetFieldValue("Gold").ToInt();
        entity.DropEquip = parse.GetFieldValue("DropEquip");
        entity.DropItem = parse.GetFieldValue("DropItem");
        entity.DropMaterial = parse.GetFieldValue("DropMaterial");
        return entity;
    }
}
