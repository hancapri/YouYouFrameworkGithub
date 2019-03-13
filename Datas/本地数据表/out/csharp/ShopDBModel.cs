
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-03-13 22:23:43
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYouFramework;

/// <summary>
/// Shop数据管理
/// </summary>
public partial class ShopDBModel : DataTableDBModelBase<ShopDBModel, ShopEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public override string DataTableName { get { return "Shop"; } }

    /// <summary>
    /// 加载列表
    /// </summary>
    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            ShopEntity entity = new ShopEntity();
            entity.Id = ms.ReadInt();
            entity.ShopCategoryId = ms.ReadInt();
            entity.GoodsType = ms.ReadInt();
            entity.GoodsId = ms.ReadInt();
            entity.OldPrice = ms.ReadInt();
            entity.Price = ms.ReadInt();
            entity.SellStatus = ms.ReadInt();

            m_List.Add(entity);
            m_Dic[entity.Id] = entity;
        }
    }
}