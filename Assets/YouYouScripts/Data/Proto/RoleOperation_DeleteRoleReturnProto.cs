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
/// 服务器返回删除角色消息
/// </summary>
public struct RoleOperation_DeleteRoleReturnProto : IProto
{
    public ushort ProtoCode { get { return 10006; } }
    public string ProtoEnName { get { return "RoleOperation_DeleteRoleReturn"; } }

    public bool IsSuccess; //是否成功
    public int MsgCode; //消息码

    public byte[] ToArray()
    {
        MMO_MemoryStream ms = GameEntry.Socket.CommonMemoryStream;
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);
        ms.WriteBool(IsSuccess);
        if(!IsSuccess)
        {
            ms.WriteInt(MsgCode);
        }
        return ms.ToArray();
    }

    public static RoleOperation_DeleteRoleReturnProto GetProto(byte[] buffer)
    {
        RoleOperation_DeleteRoleReturnProto proto = new RoleOperation_DeleteRoleReturnProto();
        MMO_MemoryStream ms = GameEntry.Socket.CommonMemoryStream;
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;
        proto.IsSuccess = ms.ReadBool();
        if(!proto.IsSuccess)
        {
            proto.MsgCode = ms.ReadInt();
        }
        return proto;
    }
}