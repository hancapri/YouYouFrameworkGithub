
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-04-14 21:59:44
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYouFramework;

/// <summary>
/// GameLevelRegion数据管理
/// </summary>
public partial class GameLevelRegionDBModel : DataTableDBModelBase<GameLevelRegionDBModel, GameLevelRegionEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public override string DataTableName { get { return "GameLevelRegion"; } }

    /// <summary>
    /// 加载列表
    /// </summary>
    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            GameLevelRegionEntity entity = new GameLevelRegionEntity();
            entity.Id = ms.ReadInt();
            entity.GameLevelId = ms.ReadInt();
            entity.RegionId = ms.ReadInt();
            entity.InitSprite = ms.ReadUTF8String();

            m_List.Add(entity);
            m_Dic[entity.Id] = entity;
        }
    }
}