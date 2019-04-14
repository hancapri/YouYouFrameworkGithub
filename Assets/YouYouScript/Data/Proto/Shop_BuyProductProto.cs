//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-04-14 21:59:59
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYouFramework;

/// <summary>
/// 客户端发送购买商城物品消息
/// </summary>
public struct Shop_BuyProductProto : IProto
{
    public ushort ProtoCode { get { return 16001; } }
    public string ProtoEnName { get { return "Shop_BuyProduct"; } }

    public int ProductId; //商品编号

    public byte[] ToArray()
    {
        MMO_MemoryStream ms = GameEntry.Socket.SocketSendMS;
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);

        ms.WriteInt(ProductId);

        return ms.ToArray();
    }

    public static Shop_BuyProductProto GetProto(byte[] buffer)
    {
        Shop_BuyProductProto proto = new Shop_BuyProductProto();
        MMO_MemoryStream ms = GameEntry.Socket.SocketReceiveMS;
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;

        proto.ProductId = ms.ReadInt();

        return proto;
    }
}