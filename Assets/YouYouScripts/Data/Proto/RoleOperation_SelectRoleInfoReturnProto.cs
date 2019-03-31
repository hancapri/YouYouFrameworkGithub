//===================================================
//作    者：边涯  http://www.u3dol.com
//创建时间：2019-03-31 12:57:22
//备    注：
//===================================================
using System.Collections;
using System.Collections.Generic;
using System;
using YouYouFramework;

/// <summary>
/// 服务器返回角色信息
/// </summary>
public struct RoleOperation_SelectRoleInfoReturnProto : IProto
{
    public ushort ProtoCode { get { return 10010; } }
    public string ProtoEnName { get { return "RoleOperation_SelectRoleInfoReturn"; } }

    public bool IsSuccess; //是否成功
    public int MsgCode; //消息码
    public int RoldId; //角色编号
    public string RoleNickName; //角色昵称
    public byte JobId; //职业编号
    public int Level; //等级
    public int TotalRechargeMoney; //总充值金额
    public int Money; //元宝
    public int Gold; //金币
    public int Exp; //经验
    public int MaxHP; //最大HP
    public int MaxMP; //最大MP
    public int CurrHP; //当前HP
    public int CurrMP; //当前MP
    public int Attack; //攻击力
    public int Defense; //防御
    public int Hit; //命中
    public int Dodge; //闪避
    public int Cri; //暴击
    public int Res; //抗性
    public int Fighting; //综合战斗力
    public int LastInWorldMapId; //最后进入的世界地图编号
    public string LastInWorldMapPos; //最后进入的世界地图坐标
    public int Equip_Weapon; //穿戴武器
    public int Equip_Pants; //穿戴护腿
    public int Equip_Clothes; //穿戴衣服
    public int Equip_Belt; //穿戴腰带
    public int Equip_Cuff; //穿戴护腕
    public int Equip_Necklace; //穿戴项链
    public int Equip_Shoe; //穿戴鞋
    public int Equip_Ring; //穿戴戒指
    public int Equip_WeaponTableId; //穿戴武器
    public int Equip_PantsTableId; //穿戴护腿
    public int Equip_ClothesTableId; //穿戴衣服
    public int Equip_BeltTableId; //穿戴腰带
    public int Equip_CuffTableId; //穿戴护腕
    public int Equip_NecklaceTableId; //穿戴项链
    public int Equip_ShoeTableId; //穿戴鞋
    public int Equip_RingTableId; //穿戴戒指

    public byte[] ToArray()
    {
        MMO_MemoryStream ms = GameEntry.Socket.CommonMemoryStream;
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);
        ms.WriteBool(IsSuccess);
        if(IsSuccess)
        {
            ms.WriteInt(RoldId);
            ms.WriteUTF8String(RoleNickName);
            ms.WriteByte(JobId);
            ms.WriteInt(Level);
            ms.WriteInt(TotalRechargeMoney);
            ms.WriteInt(Money);
            ms.WriteInt(Gold);
            ms.WriteInt(Exp);
            ms.WriteInt(MaxHP);
            ms.WriteInt(MaxMP);
            ms.WriteInt(CurrHP);
            ms.WriteInt(CurrMP);
            ms.WriteInt(Attack);
            ms.WriteInt(Defense);
            ms.WriteInt(Hit);
            ms.WriteInt(Dodge);
            ms.WriteInt(Cri);
            ms.WriteInt(Res);
            ms.WriteInt(Fighting);
            ms.WriteInt(LastInWorldMapId);
            ms.WriteUTF8String(LastInWorldMapPos);
            ms.WriteInt(Equip_Weapon);
            ms.WriteInt(Equip_Pants);
            ms.WriteInt(Equip_Clothes);
            ms.WriteInt(Equip_Belt);
            ms.WriteInt(Equip_Cuff);
            ms.WriteInt(Equip_Necklace);
            ms.WriteInt(Equip_Shoe);
            ms.WriteInt(Equip_Ring);
            ms.WriteInt(Equip_WeaponTableId);
            ms.WriteInt(Equip_PantsTableId);
            ms.WriteInt(Equip_ClothesTableId);
            ms.WriteInt(Equip_BeltTableId);
            ms.WriteInt(Equip_CuffTableId);
            ms.WriteInt(Equip_NecklaceTableId);
            ms.WriteInt(Equip_ShoeTableId);
            ms.WriteInt(Equip_RingTableId);
        }
        else
        {
            ms.WriteInt(MsgCode);
        }
        return ms.ToArray();
    }

    public static RoleOperation_SelectRoleInfoReturnProto GetProto(byte[] buffer)
    {
        RoleOperation_SelectRoleInfoReturnProto proto = new RoleOperation_SelectRoleInfoReturnProto();
        MMO_MemoryStream ms = GameEntry.Socket.CommonMemoryStream;
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;
        proto.IsSuccess = ms.ReadBool();
        if(proto.IsSuccess)
        {
            proto.RoldId = ms.ReadInt();
            proto.RoleNickName = ms.ReadUTF8String();
            proto.JobId = (byte)ms.ReadByte();
            proto.Level = ms.ReadInt();
            proto.TotalRechargeMoney = ms.ReadInt();
            proto.Money = ms.ReadInt();
            proto.Gold = ms.ReadInt();
            proto.Exp = ms.ReadInt();
            proto.MaxHP = ms.ReadInt();
            proto.MaxMP = ms.ReadInt();
            proto.CurrHP = ms.ReadInt();
            proto.CurrMP = ms.ReadInt();
            proto.Attack = ms.ReadInt();
            proto.Defense = ms.ReadInt();
            proto.Hit = ms.ReadInt();
            proto.Dodge = ms.ReadInt();
            proto.Cri = ms.ReadInt();
            proto.Res = ms.ReadInt();
            proto.Fighting = ms.ReadInt();
            proto.LastInWorldMapId = ms.ReadInt();
            proto.LastInWorldMapPos = ms.ReadUTF8String();
            proto.Equip_Weapon = ms.ReadInt();
            proto.Equip_Pants = ms.ReadInt();
            proto.Equip_Clothes = ms.ReadInt();
            proto.Equip_Belt = ms.ReadInt();
            proto.Equip_Cuff = ms.ReadInt();
            proto.Equip_Necklace = ms.ReadInt();
            proto.Equip_Shoe = ms.ReadInt();
            proto.Equip_Ring = ms.ReadInt();
            proto.Equip_WeaponTableId = ms.ReadInt();
            proto.Equip_PantsTableId = ms.ReadInt();
            proto.Equip_ClothesTableId = ms.ReadInt();
            proto.Equip_BeltTableId = ms.ReadInt();
            proto.Equip_CuffTableId = ms.ReadInt();
            proto.Equip_NecklaceTableId = ms.ReadInt();
            proto.Equip_ShoeTableId = ms.ReadInt();
            proto.Equip_RingTableId = ms.ReadInt();
        }
        else
        {
            proto.MsgCode = ms.ReadInt();
        }
        return proto;
    }
}