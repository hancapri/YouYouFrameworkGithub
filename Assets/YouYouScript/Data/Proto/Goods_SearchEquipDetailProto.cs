//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-11-06 23:26:30
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYouFramework;

/// <summary>
/// 客户端发送查询装备详情消息
/// </summary>
public struct Goods_SearchEquipDetailProto : IProto
{
    public ushort ProtoCode { get { return 16006; } }
    public string ProtoEnName { get { return "Goods_SearchEquipDetail"; } }

    public int GoodsServerId; //物品服务器端编号

    public byte[] ToArray()
    {
        MMO_MemoryStream ms = GameEntry.Socket.SocketSendMS;
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);

        ms.WriteInt(GoodsServerId);

        return ms.ToArray();
    }

    public static Goods_SearchEquipDetailProto GetProto(byte[] buffer)
    {
        Goods_SearchEquipDetailProto proto = new Goods_SearchEquipDetailProto();
        MMO_MemoryStream ms = GameEntry.Socket.SocketReceiveMS;
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;

        proto.GoodsServerId = ms.ReadInt();

        return proto;
    }
}