//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：
//备    注：
//===================================================
using YouYouFramework;

/// <summary>
/// Socket协议监听（工具生成）
/// </summary>
public sealed class SocketProtoListener
{
    /// <summary>
    /// 添加协议监听
    /// </summary>
    public static void AddProtoListener()
    {
        GameEntry.Event.SocketEvent.AddEventListener(ProtoCodeDef.System_ServerTimeReturn, System_ServerTimeReturnHandler.OnSystem_ServerTimeReturn);
        GameEntry.Event.SocketEvent.AddEventListener(ProtoCodeDef.System_GameServerConfigReturn, System_GameServerConfigReturnHandler.OnSystem_GameServerConfigReturn);
        GameEntry.Event.SocketEvent.AddEventListener(ProtoCodeDef.RoleOperation_LogOnGameServerReturn, RoleOperation_LogOnGameServerReturnHandler.OnRoleOperation_LogOnGameServerReturn);
        GameEntry.Event.SocketEvent.AddEventListener(ProtoCodeDef.RoleOperation_CreateRoleReturn, RoleOperation_CreateRoleReturnHandler.OnRoleOperation_CreateRoleReturn);
        GameEntry.Event.SocketEvent.AddEventListener(ProtoCodeDef.RoleOperation_DeleteRoleReturn, RoleOperation_DeleteRoleReturnHandler.OnRoleOperation_DeleteRoleReturn);
        GameEntry.Event.SocketEvent.AddEventListener(ProtoCodeDef.RoleOperation_EnterGameReturn, RoleOperation_EnterGameReturnHandler.OnRoleOperation_EnterGameReturn);
        GameEntry.Event.SocketEvent.AddEventListener(ProtoCodeDef.RoleOperation_SelectRoleInfoReturn, RoleOperation_SelectRoleInfoReturnHandler.OnRoleOperation_SelectRoleInfoReturn);
    }

    /// <summary>
    /// 移除协议监听
    /// </summary>
    public static void RemoveProtoListener()
    {
        GameEntry.Event.SocketEvent.RemoveEventListener(ProtoCodeDef.System_ServerTimeReturn, System_ServerTimeReturnHandler.OnSystem_ServerTimeReturn);
        GameEntry.Event.SocketEvent.RemoveEventListener(ProtoCodeDef.System_GameServerConfigReturn, System_GameServerConfigReturnHandler.OnSystem_GameServerConfigReturn);
        GameEntry.Event.SocketEvent.RemoveEventListener(ProtoCodeDef.RoleOperation_LogOnGameServerReturn, RoleOperation_LogOnGameServerReturnHandler.OnRoleOperation_LogOnGameServerReturn);
        GameEntry.Event.SocketEvent.RemoveEventListener(ProtoCodeDef.RoleOperation_CreateRoleReturn, RoleOperation_CreateRoleReturnHandler.OnRoleOperation_CreateRoleReturn);
        GameEntry.Event.SocketEvent.RemoveEventListener(ProtoCodeDef.RoleOperation_DeleteRoleReturn, RoleOperation_DeleteRoleReturnHandler.OnRoleOperation_DeleteRoleReturn);
        GameEntry.Event.SocketEvent.RemoveEventListener(ProtoCodeDef.RoleOperation_EnterGameReturn, RoleOperation_EnterGameReturnHandler.OnRoleOperation_EnterGameReturn);
        GameEntry.Event.SocketEvent.RemoveEventListener(ProtoCodeDef.RoleOperation_SelectRoleInfoReturn, RoleOperation_SelectRoleInfoReturnHandler.OnRoleOperation_SelectRoleInfoReturn);
    }
}