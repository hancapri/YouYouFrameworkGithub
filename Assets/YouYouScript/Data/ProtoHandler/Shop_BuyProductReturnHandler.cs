//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 服务器返回购买商城物品消息（工具只生成一次）
/// </summary>
public sealed class Shop_BuyProductReturnHandler
{
    public static void OnShop_BuyProductReturn(byte[] buffer)
    {
        Shop_BuyProductReturnProto proto = Shop_BuyProductReturnProto.GetProto(buffer);
#if DEBUG_LOG_PROTO
        Debug.Log("<color=#00eaff>接收消息:</color><color=#00ff9c>" + proto.ProtoEnName + " " + proto.ProtoCode + "</color>");
        Debug.Log("<color=#c5e1dc>==>>" + JsonUtility.ToJson(proto) + "</color>");
#endif
    }
}