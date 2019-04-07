//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-04-07 11:57:43
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYouFramework;

/// <summary>
/// 服务器返回服务器时间
/// </summary>
public struct System_ServerTimeReturnProto : IProto
{
    public ushort ProtoCode { get { return 14002; } }
    public string ProtoEnName { get { return "System_ServerTimeReturn"; } }

    public float LocalTime; //客户端发送的本地时间(毫秒)
    public long ServerTime; //服务器时间(毫秒)

    public byte[] ToArray()
    {
        MMO_MemoryStream ms = GameEntry.Socket.CommonMemoryStream;
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);
        ms.WriteFloat(LocalTime);
        ms.WriteLong(ServerTime);
        return ms.ToArray();
    }

    public static System_ServerTimeReturnProto GetProto(byte[] buffer)
    {
        System_ServerTimeReturnProto proto = new System_ServerTimeReturnProto();
        MMO_MemoryStream ms = GameEntry.Socket.CommonMemoryStream;
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;
        proto.LocalTime = ms.ReadFloat();
        proto.ServerTime = ms.ReadLong();
        return proto;
    }
}