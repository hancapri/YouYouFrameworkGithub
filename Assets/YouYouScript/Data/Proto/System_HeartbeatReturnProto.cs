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
/// 服务器返回心跳
/// </summary>
public struct System_HeartbeatReturnProto : IProto
{
    public ushort ProtoCode { get { return 14002; } }
    public string ProtoEnName { get { return "System_HeartbeatReturn"; } }

    public float LocalTime; //客户端发送的本地时间(毫秒)
    public long ServerTime; //服务器时间(毫秒)

    public byte[] ToArray()
    {
        MMO_MemoryStream ms = GameEntry.Socket.SocketSendMS;
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);

        ms.WriteFloat(LocalTime);
        ms.WriteLong(ServerTime);

        return ms.ToArray();
    }

    public static System_HeartbeatReturnProto GetProto(byte[] buffer)
    {
        System_HeartbeatReturnProto proto = new System_HeartbeatReturnProto();
        MMO_MemoryStream ms = GameEntry.Socket.SocketReceiveMS;
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;

        proto.LocalTime = ms.ReadFloat();
        proto.ServerTime = ms.ReadLong();

        return proto;
    }
}