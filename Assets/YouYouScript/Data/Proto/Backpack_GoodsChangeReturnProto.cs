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
/// 服务器返回背包物品更新消息
/// </summary>
public struct Backpack_GoodsChangeReturnProto : IProto
{
    public ushort ProtoCode { get { return 16003; } }
    public string ProtoEnName { get { return "Backpack_GoodsChangeReturn"; } }

    public int BackpackItemChangeCount; //更新的种类数量
    public List<ChangeItem> ItemList; //更改项

    [Serializable]
    /// <summary>
    /// 更改项
    /// </summary>
    public struct ChangeItem
    {
        public int BackpackId; //背包项编号
        public byte ChangeType; //改变类型
        public byte GoodsType; //物品类型
        public int GoodsId; //物品编号
        public int GoodsCount; //物品数量
        public int GoodsServerId; //物品服务器端Id
    }

    public byte[] ToArray()
    {
        MMO_MemoryStream ms = GameEntry.Socket.SocketSendMS;
        ms.SetLength(0);
        ms.WriteUShort(ProtoCode);

        ms.WriteInt(BackpackItemChangeCount);
        for (int i = 0; i < BackpackItemChangeCount; i++)
        {
            ms.WriteInt(ItemList[i].BackpackId);
            ms.WriteByte(ItemList[i].ChangeType);
            ms.WriteByte(ItemList[i].GoodsType);
            ms.WriteInt(ItemList[i].GoodsId);
            ms.WriteInt(ItemList[i].GoodsCount);
            ms.WriteInt(ItemList[i].GoodsServerId);
        }

        return ms.ToArray();
    }

    public static Backpack_GoodsChangeReturnProto GetProto(byte[] buffer)
    {
        Backpack_GoodsChangeReturnProto proto = new Backpack_GoodsChangeReturnProto();
        MMO_MemoryStream ms = GameEntry.Socket.SocketReceiveMS;
        ms.SetLength(0);
        ms.Write(buffer, 0, buffer.Length);
        ms.Position = 0;

        proto.BackpackItemChangeCount = ms.ReadInt();
        proto.ItemList = new List<ChangeItem>();
        for (int i = 0; i < proto.BackpackItemChangeCount; i++)
        {
            ChangeItem _Item = new ChangeItem();
            _Item.BackpackId = ms.ReadInt();
            _Item.ChangeType = (byte)ms.ReadByte();
            _Item.GoodsType = (byte)ms.ReadByte();
            _Item.GoodsId = ms.ReadInt();
            _Item.GoodsCount = ms.ReadInt();
            _Item.GoodsServerId = ms.ReadInt();
            proto.ItemList.Add(_Item);
        }

        return proto;
    }
}