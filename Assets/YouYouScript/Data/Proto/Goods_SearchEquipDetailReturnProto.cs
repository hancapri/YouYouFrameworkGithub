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
/// 服务器返回查询装备详情消息
/// </summary>
public struct Goods_SearchEquipDetailReturnProto : IProto
{
    public ushort ProtoCode { get { return 16007; } }
    public string ProtoEnName { get { return "Goods_SearchEquipDetailReturn"; } }

    public int EnchantLevel; //强化等级
    public byte BaseAttr1Type; //基础属性1类型
    public int BaseAttr1Value; //基础属性1值
    public byte BaseAttr2Type; //基础属性2类型
    public int BaseAttr2Value; //基础属性2值
    public int HP; //生命
    public int MP; //魔法
    public int Attack; //攻击
    public int Defense; //防御
    public int Hit; //命中
    public int Dodge; //闪避
    public int Cri; //暴击
    public int Res; //抗性
    public byte IsPutOn; //是否穿戴

    public byte[] ToArray()
    {
        MMO_MemoryStream ms = GameEntry.Socket.SocketSendMS;
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);

        ms.WriteInt(EnchantLevel);
        ms.WriteByte(BaseAttr1Type);
        ms.WriteInt(BaseAttr1Value);
        ms.WriteByte(BaseAttr2Type);
        ms.WriteInt(BaseAttr2Value);
        ms.WriteInt(HP);
        ms.WriteInt(MP);
        ms.WriteInt(Attack);
        ms.WriteInt(Defense);
        ms.WriteInt(Hit);
        ms.WriteInt(Dodge);
        ms.WriteInt(Cri);
        ms.WriteInt(Res);
        ms.WriteByte(IsPutOn);

        return ms.ToArray();
    }

    public static Goods_SearchEquipDetailReturnProto GetProto(byte[] buffer)
    {
        Goods_SearchEquipDetailReturnProto proto = new Goods_SearchEquipDetailReturnProto();
        MMO_MemoryStream ms = GameEntry.Socket.SocketReceiveMS;
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;

        proto.EnchantLevel = ms.ReadInt();
        proto.BaseAttr1Type = (byte)ms.ReadByte();
        proto.BaseAttr1Value = ms.ReadInt();
        proto.BaseAttr2Type = (byte)ms.ReadByte();
        proto.BaseAttr2Value = ms.ReadInt();
        proto.HP = ms.ReadInt();
        proto.MP = ms.ReadInt();
        proto.Attack = ms.ReadInt();
        proto.Defense = ms.ReadInt();
        proto.Hit = ms.ReadInt();
        proto.Dodge = ms.ReadInt();
        proto.Cri = ms.ReadInt();
        proto.Res = ms.ReadInt();
        proto.IsPutOn = (byte)ms.ReadByte();

        return proto;
    }
}