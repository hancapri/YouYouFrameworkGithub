
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2018-10-11 13:06:31
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// GameLevelGrade数据管理
/// </summary>
public partial class GameLevelGradeDBModel : AbstractDBModel<GameLevelGradeDBModel, GameLevelGradeEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    protected override string FileName { get { return "GameLevelGrade.data"; } }

    /// <summary>
    /// 创建实体
    /// </summary>
    /// <param name="parse"></param>
    /// <returns></returns>
    protected override GameLevelGradeEntity MakeEntity(GameDataTableParser parse)
    {
        GameLevelGradeEntity entity = new GameLevelGradeEntity();
        entity.Id = parse.GetFieldValue("Id").ToInt();
        entity.GameLevelId = parse.GetFieldValue("GameLevelId").ToInt();
        entity.Grade = parse.GetFieldValue("Grade").ToInt();
        entity.Desc = parse.GetFieldValue("Desc");
        entity.Type = parse.GetFieldValue("Type").ToInt();
        entity.Parameter = parse.GetFieldValue("Parameter");
        entity.ConditionDesc = parse.GetFieldValue("ConditionDesc");
        entity.Exp = parse.GetFieldValue("Exp").ToInt();
        entity.Gold = parse.GetFieldValue("Gold").ToInt();
        entity.CommendFighting = parse.GetFieldValue("CommendFighting").ToInt();
        entity.TimeLimit = parse.GetFieldValue("TimeLimit").ToFloat();
        entity.Star1 = parse.GetFieldValue("Star1").ToFloat();
        entity.Star2 = parse.GetFieldValue("Star2").ToFloat();
        entity.Equip = parse.GetFieldValue("Equip");
        entity.Item = parse.GetFieldValue("Item");
        entity.Material = parse.GetFieldValue("Material");
        return entity;
    }
}
