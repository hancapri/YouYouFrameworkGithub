
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2018-10-11 13:06:31
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// GameLevelRegion数据管理
/// </summary>
public partial class GameLevelRegionDBModel : AbstractDBModel<GameLevelRegionDBModel, GameLevelRegionEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "GameLevelRegion.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override GameLevelRegionEntity MakeEntity(GameDataTableParser parse)
    {
        GameLevelRegionEntity entity = new GameLevelRegionEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.GameLevelId = parse.GetFieldValue("GameLevelId").ToInt();
        entity.RegionId = parse.GetFieldValue("RegionId").ToInt();
        entity.InitSprite = parse.GetFieldValue("InitSprite");
        return entity;
    }
}
