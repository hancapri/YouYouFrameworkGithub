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
/// 客户端发送心跳
/// </summary>
public struct System_HeartbeatProto : IProto
{
    public ushort ProtoCode { get { return 14001; } }
    public string ProtoEnName { get { return "System_Heartbeat"; } }

    public float LocalTime; //本地时间(毫秒)

    public byte[] ToArray()
    {
        MMO_MemoryStream ms = GameEntry.Socket.SocketSendMS;
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);

        ms.WriteFloat(LocalTime);

        return ms.ToArray();
    }

    public static System_HeartbeatProto GetProto(byte[] buffer)
    {
        System_HeartbeatProto proto = new System_HeartbeatProto();
        MMO_MemoryStream ms = GameEntry.Socket.SocketReceiveMS;
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;

        proto.LocalTime = ms.ReadFloat();

        return proto;
    }
}