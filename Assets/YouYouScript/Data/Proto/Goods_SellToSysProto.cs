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
/// 客户端发送出售物品给系统消息
/// </summary>
public struct Goods_SellToSysProto : IProto
{
    public ushort ProtoCode { get { return 16008; } }
    public string ProtoEnName { get { return "Goods_SellToSys"; } }

    public int roleBackpackId; //背包项编号
    public byte GoodsType; //物品类型
    public int GoodsId; //物品编号
    public int GoodsServerId; //服务服务器端编号
    public int SellCount; //出售数量

    public byte[] ToArray()
    {
        MMO_MemoryStream ms = GameEntry.Socket.SocketSendMS;
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);

        ms.WriteInt(roleBackpackId);
        ms.WriteByte(GoodsType);
        ms.WriteInt(GoodsId);
        ms.WriteInt(GoodsServerId);
        ms.WriteInt(SellCount);

        return ms.ToArray();
    }

    public static Goods_SellToSysProto GetProto(byte[] buffer)
    {
        Goods_SellToSysProto proto = new Goods_SellToSysProto();
        MMO_MemoryStream ms = GameEntry.Socket.SocketReceiveMS;
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;

        proto.roleBackpackId = ms.ReadInt();
        proto.GoodsType = (byte)ms.ReadByte();
        proto.GoodsId = ms.ReadInt();
        proto.GoodsServerId = ms.ReadInt();
        proto.SellCount = ms.ReadInt();

        return proto;
    }
}