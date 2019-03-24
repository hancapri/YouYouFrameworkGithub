//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-03-24 13:47:02
//备    注：
//===================================================
using System.Collections;

/// <summary>
/// 协议编号定义
/// </summary>
public class ProtoCodeDef
{
    /// <summary>
    /// 客户端发送本地时间
    /// </summary>
    public const ushort System_SendLocalTime = 14001;

    /// <summary>
    /// 服务器返回服务器时间
    /// </summary>
    public const ushort System_ServerTimeReturn = 14002;

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
    /// 服务器返回角色学会的技能
    /// </summary>
    public const ushort RoleData_SkillReturn = 11001;

    /// <summary>
    /// 服务器返回角色充值信息
    /// </summary>
    public const ushort RoleData_RechargeReturn = 11002;

    /// <summary>
    /// 服务器返回充值产品信息
    /// </summary>
    public const ushort RoleData_RechargeProductReturn = 11003;

    /// <summary>
    /// 服务器返回元宝更新消息
    /// </summary>
    public const ushort RoleData_MondeyChangeReturn = 11004;

    /// <summary>
    /// 服务器返回金币更新消息
    /// </summary>
    public const ushort RoleData_GoldChangeReturn = 11005;

    /// <summary>
    /// 客户端发送进入游戏关卡消息
    /// </summary>
    public const ushort GameLevel_Enter = 12001;

    /// <summary>
    /// 服务器返回进入关卡消息
    /// </summary>
    public const ushort GameLevel_EnterReturn = 12002;

    /// <summary>
    /// 客户端发送战斗胜利消息
    /// </summary>
    public const ushort GameLevel_Victory = 12003;

    /// <summary>
    /// 服务器返回战斗胜利消息
    /// </summary>
    public const ushort GameLevel_VictoryReturn = 12004;

    /// <summary>
    /// 客户端发送战斗失败消息
    /// </summary>
    public const ushort GameLevel_Fail = 12005;

    /// <summary>
    /// 服务器返回战斗失败消息
    /// </summary>
    public const ushort GameLevel_FailReturn = 12006;

    /// <summary>
    /// 客户端发送复活消息
    /// </summary>
    public const ushort GameLevel_Resurgence = 12007;

    /// <summary>
    /// 服务器返回复活消息
    /// </summary>
    public const ushort GameLevel_ResurgenceReturn = 12008;

    /// <summary>
    /// 客户端发送进入世界地图场景消息
    /// </summary>
    public const ushort WorldMap_RoleEnter = 13001;

    /// <summary>
    /// 服务器返回进入世界地图场景消息
    /// </summary>
    public const ushort WorldMap_RoleEnterReturn = 13002;

    /// <summary>
    /// 客户端发送自身坐标
    /// </summary>
    public const ushort WorldMap_Pos = 13003;

    /// <summary>
    /// 客户端发送角色已经进入世界地图场景消息
    /// </summary>
    public const ushort WorldMap_RoleAlreadyEnter = 13004;

    /// <summary>
    /// 服务器广播其他角色进入场景消息
    /// </summary>
    public const ushort WorldMap_OtherRoleEnter = 13005;

    /// <summary>
    /// 服务器广播其他角色离开场景消息
    /// </summary>
    public const ushort WorldMap_OtherRoleLeave = 13006;

    /// <summary>
    /// 服务器广播当前场景角色
    /// </summary>
    public const ushort WorldMap_InitRole = 13007;

    /// <summary>
    /// 客户端发送当前角色移动消息
    /// </summary>
    public const ushort WorldMap_CurrRoleMove = 13008;

    /// <summary>
    /// 服务器广播其他角色移动消息
    /// </summary>
    public const ushort WorldMap_OtherRoleMove = 13009;

    /// <summary>
    /// 客户端发送角色使用技能消息
    /// </summary>
    public const ushort WorldMap_CurrRoleUseSkill = 13010;

    /// <summary>
    /// 服务器广播其他角色使用技能消息
    /// </summary>
    public const ushort WorldMap_OtherRoleUseSkill = 13011;

    /// <summary>
    /// 服务器广播其他角色死亡消息
    /// </summary>
    public const ushort WorldMap_OtherRoleDie = 13012;

    /// <summary>
    /// 客户端发送角色更新信息消息
    /// </summary>
    public const ushort WorldMap_CurrRoleUpdateInfo = 13013;

    /// <summary>
    /// 服务器广播角色更新信息消息
    /// </summary>
    public const ushort WorldMap_OtherRoleUpdateInfo = 13014;

    /// <summary>
    /// 客户端发送角色复活消息
    /// </summary>
    public const ushort WorldMap_CurrRoleResurgence = 13015;

    /// <summary>
    /// 服务器广播角色复活消息
    /// </summary>
    public const ushort WorldMap_OtherRoleResurgence = 13016;

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
