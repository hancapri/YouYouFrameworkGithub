
//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-03-11 23:19:14
//备    注：此代码为工具生成 请勿手工修改
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYou;

/// <summary>
/// RechargeShop数据管理
/// </summary>
public partial class RechargeShopDBModel : DataTableDBModelBase<RechargeShopDBModel, RechargeShopEntity>
{
    /// <summary>
    /// 文件名称
    /// </summary>
    public override string DataTableName { get { return "RechargeShop"; } }

    /// <summary>
    /// 加载列表
    /// </summary>
    protected override void LoadList(MMO_MemoryStream ms)
    {
        int rows = ms.ReadInt();
        int columns = ms.ReadInt();

        for (int i = 0; i < rows; i++)
        {
            RechargeShopEntity entity = new RechargeShopEntity();
            entity.Id = ms.ReadInt();
            entity.Type = ms.ReadInt();
            entity.Price = ms.ReadInt();
            entity.Name = ms.ReadUTF8String();
            entity.SalesDesc = ms.ReadUTF8String();
            entity.ProductDesc = ms.ReadUTF8String();
            entity.Virtual = ms.ReadInt();
            entity.Icon = ms.ReadUTF8String();

            m_List.Add(entity);
            m_Dic[entity.Id] = entity;
        }
    }
}