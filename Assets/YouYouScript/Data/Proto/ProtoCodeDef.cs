//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-04-14 21:59:59
//备    注：
//===================================================
using System.Collections;

/// <summary>
/// 协议编号定义
/// </summary>
public class ProtoCodeDef
{
    /// <summary>
    /// 客户端发送心跳
    /// </summary>
    public const ushort System_Heartbeat = 14001;

    /// <summary>
    /// 服务器返回心跳
    /// </summary>
    public const ushort System_HeartbeatReturn = 14002;

    /// <summary>
    /// 服务器返回配置列表
    /// </summary>
    public const ushort System_GameServerConfigReturn = 14003;

    /// <summary>
    /// 客户端发送登录区服消息
    /// </summary>
    public const ushort RoleOperation_LogOnGameServer = 10001;

    /// <summary>
    /// 服务器返回登录信息
    /// </summary>
    public const ushort RoleOperation_LogOnGameServerReturn = 10002;

    /// <summary>
    /// 客户端发送创建角色消息
    /// </summary>
    public const ushort RoleOperation_CreateRole = 10003;

    /// <summary>
    /// 服务器返回创建角色消息
    /// </summary>
    public const ushort RoleOperation_CreateRoleReturn = 10004;

    /// <summary>
    /// 客户端发送删除角色消息
    /// </summary>
    public const ushort RoleOperation_DeleteRole = 10005;

    /// <summary>
    /// 服务器返回删除角色消息
    /// </summary>
    public const ushort RoleOperation_DeleteRoleReturn = 10006;

    /// <summary>
    /// 客户端发送进入游戏消息
    /// </summary>
    public const ushort RoleOperation_EnterGame = 10007;

    /// <summary>
    /// 服务器返回进入游戏消息
    /// </summary>
    public const ushort RoleOperation_EnterGameReturn = 10008;

    /// <summary>
    /// 客户端查询角色信息
    /// </summary>
    public const ushort RoleOperation_SelectRoleInfo = 10009;

    /// <summary>
    /// 服务器返回角色信息
    /// </summary>
    public const ushort RoleOperation_SelectRoleInfoReturn = 10010;

    /// <summary>
    /// 客户端发送查询任务消息
    /// </summary>
    public const ushort Task_SearchTask = 15001;

    /// <summary>
    /// 服务器返回任务列表消息
    /// </summary>
    public const ushort Task_SearchTaskReturn = 15002;

    /// <summary>
    /// 客户端发送购买商城物品消息
    /// </summary>
    public const ushort Shop_BuyProduct = 16001;

    /// <summary>
    /// 服务器返回购买商城物品消息
    /// </summary>
    public const ushort Shop_BuyProductReturn = 16002;

    /// <summary>
    /// 服务器返回背包物品更新消息
    /// </summary>
    public const ushort Backpack_GoodsChangeReturn = 16003;

    /// <summary>
    /// 客户端发送查询背包项消息
    /// </summary>
    public const ushort Backpack_Search = 16004;

    /// <summary>
    /// 服务器返回查询背包项消息
    /// </summary>
    public const ushort Backpack_SearchReturn = 16005;

    /// <summary>
    /// 客户端发送查询装备详情消息
    /// </summary>
    public const ushort Goods_SearchEquipDetail = 16006;

    /// <summary>
    /// 服务器返回查询装备详情消息
    /// </summary>
    public const ushort Goods_SearchEquipDetailReturn = 16007;

    /// <summary>
    /// 客户端发送出售物品给系统消息
    /// </summary>
    public const ushort Goods_SellToSys = 16008;

    /// <summary>
    /// 服务器返回出售物品给系统消息
    /// </summary>
    public const ushort Goods_SellToSysReturn = 16009;

    /// <summary>
    /// 客户端发送使用道具消息
    /// </summary>
    public const ushort Goods_UseItem = 16010;

    /// <summary>
    /// 服务器返回使用道具消息
    /// </summary>
    public const ushort Goods_UseItemReturn = 16011;

    /// <summary>
    /// 客户端发送穿戴消息
    /// </summary>
    public const ushort Goods_EquipPut = 16012;

    /// <summary>
    /// 服务器返回穿戴消息
    /// </summary>
    public const ushort Goods_EquipPutReturn = 16013;

}
