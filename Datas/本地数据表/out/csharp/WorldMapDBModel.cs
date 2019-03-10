
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2018-10-11 13:06:32
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// WorldMap数据管理
/// </summary>
public partial class WorldMapDBModel : AbstractDBModel<WorldMapDBModel, WorldMapEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "WorldMap.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override WorldMapEntity MakeEntity(GameDataTableParser parse)
    {
        WorldMapEntity entity = new WorldMapEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.Name = parse.GetFieldValue("Name");
        entity.SceneName = parse.GetFieldValue("SceneName");
        entity.SmallMapImg = parse.GetFieldValue("SmallMapImg");
        entity.NPCList = parse.GetFieldValue("NPCList");
        entity.RoleBirthPos = parse.GetFieldValue("RoleBirthPos");
        entity.CameraRotation = parse.GetFieldValue("CameraRotation");
        entity.TransPos = parse.GetFieldValue("TransPos");
        entity.IsCity = parse.GetFieldValue("IsCity").ToInt();
        entity.IsShowInMap = parse.GetFieldValue("IsShowInMap").ToInt();
        entity.PosInMap = parse.GetFieldValue("PosInMap");
        entity.IcoInMap = parse.GetFieldValue("IcoInMap");
        entity.NearScene = parse.GetFieldValue("NearScene");
        entity.Audio_BG = parse.GetFieldValue("Audio_BG");
        return entity;
    }
}
