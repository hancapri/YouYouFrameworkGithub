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
/// 客户端发送登录区服消息
/// </summary>
public struct RoleOperation_LogOnGameServerProto : IProto
{
    public ushort ProtoCode { get { return 10001; } }
    public string ProtoEnName { get { return "RoleOperation_LogOnGameServer"; } }

    public int AccountId; //账户ID

    public byte[] ToArray()
    {
        MMO_MemoryStream ms = GameEntry.Socket.SocketSendMS;
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);

        ms.WriteInt(AccountId);

        return ms.ToArray();
    }

    public static RoleOperation_LogOnGameServerProto GetProto(byte[] buffer)
    {
        RoleOperation_LogOnGameServerProto proto = new RoleOperation_LogOnGameServerProto();
        MMO_MemoryStream ms = GameEntry.Socket.SocketReceiveMS;
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;

        proto.AccountId = ms.ReadInt();

        return proto;
    }
}