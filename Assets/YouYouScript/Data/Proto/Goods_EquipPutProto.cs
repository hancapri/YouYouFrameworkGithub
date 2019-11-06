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
/// 客户端发送穿戴消息
/// </summary>
public struct Goods_EquipPutProto : IProto
{
    public ushort ProtoCode { get { return 16012; } }
    public string ProtoEnName { get { return "Goods_EquipPut"; } }

    public byte Type; //0=穿上 1=脱下
    public int GoodsId; //装备编号
    public int GoodsServerId; //装备服务器端编号

    public byte[] ToArray()
    {
        MMO_MemoryStream ms = GameEntry.Socket.SocketSendMS;
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);

        ms.WriteByte(Type);
        ms.WriteInt(GoodsId);
        ms.WriteInt(GoodsServerId);

        return ms.ToArray();
    }

    public static Goods_EquipPutProto GetProto(byte[] buffer)
    {
        Goods_EquipPutProto proto = new Goods_EquipPutProto();
        MMO_MemoryStream ms = GameEntry.Socket.SocketReceiveMS;
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;

        proto.Type = (byte)ms.ReadByte();
        proto.GoodsId = ms.ReadInt();
        proto.GoodsServerId = ms.ReadInt();

        return proto;
    }
}