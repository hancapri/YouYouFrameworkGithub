
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
/// GameLevel数据管理
/// </summary>
public partial class GameLevelDBModel : DataTableDBModelBase<GameLevelDBModel, GameLevelEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public override string DataTableName { get { return "GameLevel"; } }

    /// <summary>
    /// 加载列表
    /// </summary>
    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            GameLevelEntity entity = new GameLevelEntity();
            entity.Id = ms.ReadInt();
            entity.ChapterID = ms.ReadInt();
            entity.Name = ms.ReadUTF8String();
            entity.SceneName = ms.ReadUTF8String();
            entity.SmallMapImg = ms.ReadUTF8String();
            entity.isBoss = ms.ReadInt();
            entity.Ico = ms.ReadUTF8String();
            entity.PosInMap = ms.ReadUTF8String();
            entity.DlgPic = ms.ReadUTF8String();
            entity.CameraRotation = ms.ReadUTF8String();
            entity.Audio_BG = ms.ReadUTF8String();

            m_List.Add(entity);
            m_Dic[entity.Id] = entity;
        }
    }
}