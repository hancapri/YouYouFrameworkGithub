using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace ReadExcel
{
    public class DataMgr
    {
        private static DataMgr instance;

        public static DataMgr Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataMgr();
                }
                return instance;
            }
        }

        private string m_Path = null;
        XDocument m_Doc;
        public List<MyMenu> MenuList = new List<MyMenu>();

        private DataMgr()
        {
            m_Path = Application.StartupPath + "\\Data.xml";
        }

        public void LoadXml()
        {
            MenuList.Clear();

            m_Doc = XDocument.Load(m_Path);

            if (m_Doc != null)
            {
                List<XElement> lst = m_Doc.Root.Elements("Menu").ToList();

                for (int i = 0; i < lst.Count; i++)
                {
                    long menuID = long.Parse(lst[i].Attribute("MenuId").Value);
                    string menuName = lst[i].Attribute("MenuName").Value;
                    bool hasChild = bool.Parse(lst[i].Attribute("HasChild").Value);

                    MyMenu menu = new MyMenu();
                    menu.MenuId = menuID;
                    menu.MenuName = menuName;
                    menu.HasChild = hasChild;

                    if (hasChild)
                    {
                        List<MyProto> lstProto = GetProtoListByMenuName(menuID);
                        if (lstProto != null)
                        {
                            menu.ProtoList.AddRange(lstProto);
                        }
                    }

                    MenuList.Add(menu);
                }
            }
        }

        private List<MyProto> GetProtoListByMenuName(long menuID)
        {
            int index = 0;
            XElement xe = GetXElementByID(menuID, out index);

            if (xe == null) return null;

            List<XElement> lst = xe.Elements("Proto").ToList();
            if (lst == null || lst.Count == 0) return null;

            List<MyProto> retList = new List<MyProto>();

            for (int i = 0; i < lst.Count; i++)
            {
                long protoID = long.Parse(lst[i].Attribute("ProtoID").Value);
                string protoCode = lst[i].Attribute("ProtoCode").Value;
                string protoEnName = lst[i].Attribute("ProtoEnName").Value;
                string protoCnName = lst[i].Attribute("ProtoCnName").Value;
                string protoDesc = lst[i].Attribute("ProtoDesc").Value;

                string protoCategory = lst[i].Attribute("ProtoCategory") == null ? "C2S" : lst[i].Attribute("ProtoCategory").Value;
                string isCSharp = lst[i].Attribute("IsCSharp") == null ? "false" : lst[i].Attribute("IsCSharp").Value;
                string isLua = lst[i].Attribute("IsLua") == null ? "false" : lst[i].Attribute("IsLua").Value;

                MyProto proto = new MyProto();
                proto.ProtoID = protoID;
                proto.ProtoCode = protoCode;
                proto.ProtoEnName = protoEnName;
                proto.ProtoCnName = protoCnName;
                proto.ProtoDesc = protoDesc;
                proto.ProtoCategory = protoCategory;
                proto.IsCSharp = bool.Parse(isCSharp);
                proto.IsLua = bool.Parse(isLua);

                proto.MenuID = menuID;
                retList.Add(proto);
            }
            return retList;
        }

        //====================================================================================

        #region 菜单操作
        public void AddMenu(MyMenu menu)
        {
            XElement xe = new XElement("Menu", new XAttribute("MenuId", menu.MenuId), new XAttribute("MenuName", menu.MenuName), new XAttribute("HasChild", menu.HasChild));
            m_Doc.Root.Add(xe);
        }

        public void UpdateMenu(MyMenu menu)
        {
            int index = 0;
            XElement xe = GetXElementByID(menu.MenuId, out index);
            if (xe == null) return;
            xe.Attribute("MenuName").SetValue(menu.MenuName);
        }

        public void DeleteMenu(MyMenu menu)
        {
            int index = 0;
            XElement xe = GetXElementByID(menu.MenuId, out index);
            if (xe == null) return;

            xe.Remove();

            MenuList.Remove(menu);
        }

        public bool MoveMenu(MyMenu menu, bool isPrev)
        {
            //元素的索引
            int index = 0;
            XElement xe = GetXElementByID(menu.MenuId, out index);

            if (isPrev)
            {
                //上移
                if (index == 0)
                {
                    MessageBox.Show("已经是第一个");
                    return false;
                }

                int toIndex = index - 1;

                XElement prev = GetXElementByIndex(toIndex);
                prev.AddBeforeSelf(xe);
                xe.Remove();
            }
            else
            {
                //下移
                if (index == m_Doc.Root.Elements("Menu").ToList().Count - 1)
                {
                    MessageBox.Show("已经是最后一个");
                    return false;
                }

                int toIndex = index + 1;
                XElement next = GetXElementByIndex(toIndex);
                next.AddAfterSelf(xe);
                xe.Remove();
            }

            return true;
        }
        #endregion
        //===================================================

        #region 协议操作
        public void AddProto(MyProto node, long menuID)
        {
            int index = 0;
            XElement xMenu = GetXElementByID(menuID, out index);

            if (xMenu == null)
            {
                MessageBox.Show("添加协议失败 找不到菜单");
                return;
            }

            xMenu.Attribute("HasChild").SetValue(true);

            XElement xe = new XElement("Proto", new XAttribute("ProtoID", node.ProtoID), new XAttribute("ProtoCode", node.ProtoCode), new XAttribute("ProtoEnName", node.ProtoEnName), new XAttribute("ProtoCnName", node.ProtoCnName), new XAttribute("ProtoDesc", node.ProtoDesc));
            xMenu.Add(xe);
        }

        public void UpdateProto(MyProto proto)
        {
            int protoIndex = 0;
            XElement xe = GetProtoElement(proto.ProtoID, proto.MenuID, out protoIndex);

            if (xe == null) return;

            xe.Attribute("ProtoCode").SetValue(proto.ProtoCode);
            xe.Attribute("ProtoEnName").SetValue(proto.ProtoEnName);
            xe.Attribute("ProtoCnName").SetValue(proto.ProtoCnName);
            xe.Attribute("ProtoDesc").SetValue(proto.ProtoDesc);

            xe.SetAttributeValue("ProtoCategory", proto.ProtoCategory);
            xe.SetAttributeValue("IsCSharp", proto.IsCSharp);
            xe.SetAttributeValue("IsLua", proto.IsLua);
        }

        public bool MoveProto(MyProto proto, bool isPrev)
        {
            //元素的索引
            int protoIndex = 0;
            XElement xe = GetProtoElement(proto.ProtoID, proto.MenuID, out protoIndex);

            if (isPrev)
            {
                //上移
                if (protoIndex == 0)
                {
                    MessageBox.Show("已经是第一个");
                    return false;
                }

                int toIndex = protoIndex - 1;

                XElement prev = GetProtoElementByIndex(proto.MenuID, toIndex);
                prev.AddBeforeSelf(xe);
                xe.Remove();
            }
            else
            {
                int menuindex = 0;
                //下移
                if (protoIndex == GetXElementByID(proto.MenuID, out menuindex).Elements("Proto").ToList().Count - 1)
                {
                    MessageBox.Show("已经是最后一个");
                    return false;
                }

                int toIndex = protoIndex + 1;
                XElement next = GetProtoElementByIndex(proto.MenuID, toIndex);
                next.AddAfterSelf(xe);
                xe.Remove();
            }

            return true;
        }

        public void DeleteProto(MyProto proto)
        {
            int protoIndex = 0;
            XElement xe = GetProtoElement(proto.ProtoID, proto.MenuID, out protoIndex);
            if (xe == null) return;

            xe.Remove();
        }
        #endregion


        #region 数据表操作

        public void ProtoDataSave(List<MyProtoAttr> lst, long protoID, long menuID)
        {
            int protoIndex = 0;
            XElement xe = GetProtoElement(protoID, menuID, out protoIndex);

            xe.Elements("Attr").Remove();

            for (int i = 0; i < lst.Count; i++)
            {
                XElement attr = new XElement("Attr",
                    new XAttribute("AttID", lst[i].AttID),
                     new XAttribute("AttType", lst[i].AttType),
                      new XAttribute("AttEnName", lst[i].AttEnName),
                       new XAttribute("AttCnName", lst[i].AttCnName),
                        new XAttribute("AttIsLoop", lst[i].AttIsLoop),
                         new XAttribute("AttToLoop", lst[i].AttToLoop),
                          new XAttribute("AttToBool", lst[i].AttToBool),
                           new XAttribute("AttToBoolResult", lst[i].AttToBoolResult),
                            new XAttribute("AttToCus", lst[i].AttToCus)
                    );

                xe.Add(attr);
            }
        }

        public List<MyProtoAttr> GetProtoArrtList(long protoID, long menuID)
        {
            int protoIndex = 0;
            XElement xe = GetProtoElement(protoID, menuID, out protoIndex);

            List<XElement> lst = xe.Elements("Attr").ToList();

            List<MyProtoAttr> retLst = new List<MyProtoAttr>();

            if (lst != null && lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    retLst.Add(new MyProtoAttr()
                    {
                        AttID = long.Parse(lst[i].Attribute("AttID").Value),
                        AttType = lst[i].Attribute("AttType").Value,
                        AttEnName = lst[i].Attribute("AttEnName").Value,
                        AttCnName = lst[i].Attribute("AttCnName").Value,
                        AttIsLoop = bool.Parse(lst[i].Attribute("AttIsLoop").Value),
                        AttToLoop = lst[i].Attribute("AttToLoop").Value,
                        AttToBool = lst[i].Attribute("AttToBool").Value,
                        AttToBoolResult = bool.Parse(lst[i].Attribute("AttToBoolResult").Value),
                        AttToCus = lst[i].Attribute("AttToCus").Value
                    });
                }
            }

            return retLst;
        }

        #endregion






        public void SaveXml()
        {
            m_Doc.Save(m_Path);
        }


        #region 查找节点
        private XElement GetXElementByID(long menuID, out int index)
        {
            index = 0;

            List<XElement> lst = m_Doc.Root.Elements("Menu").ToList();
            for (int i = 0; i < lst.Count; i++)
            {
                if (long.Parse(lst[i].Attribute("MenuId").Value) == menuID)
                {
                    index = i;
                    return lst[i];
                }
            }

            return null;
        }

        private XElement GetXElementByIndex(int index)
        {
            List<XElement> lst = m_Doc.Root.Elements("Menu").ToList();
            return lst[index];
        }

        private XElement GetProtoElement(long protoID, long menuID, out int protoIndex)
        {
            protoIndex = 0;
            int index = 0;
            XElement xe = GetXElementByID(menuID, out index);

            List<XElement> lst = xe.Elements("Proto").ToList();

            for (int i = 0; i < lst.Count; i++)
            {
                if (long.Parse(lst[i].Attribute("ProtoID").Value) == protoID)
                {
                    protoIndex = i;
                    return lst[i];
                }
            }

            return null;
        }

        private XElement GetProtoElementByIndex(long menuID, int index)
        {
            int menuIndex = 0;
            XElement xe = GetXElementByID(menuID, out menuIndex);
            List<XElement> lst = xe.Elements("Proto").ToList();
            return lst[index];
        }
        #endregion
    }

    public class NodeTag
    {
        public bool IsMenu;

        public MyMenu Menu;
        public MyProto Proto;
    }

    /// <summary>
    /// 菜单对象
    /// </summary>
    public class MyMenu
    {
        /// <summary>
        /// 菜单编号
        /// </summary>
        public long MenuId;

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName;

        /// <summary>
        /// 是否有子节点
        /// </summary>
        public bool HasChild;


        private List<MyProto> _ProtoList = new List<MyProto>();
        /// <summary>
        /// 协议集合
        /// </summary>
        public List<MyProto> ProtoList
        {
            get
            {
                return _ProtoList;
            }
        }
    }

    /// <summary>
    /// 协议对象
    /// </summary>
    public class MyProto
    {
        public long ProtoID;
        public string ProtoCode;
        public string ProtoEnName;
        public string ProtoCnName;
        public string ProtoDesc;

        /// <summary>
        /// 协议分类
        /// </summary>
        public string ProtoCategory;

        /// <summary>
        /// 是否c#协议
        /// </summary>
        public bool IsCSharp;

        /// <summary>
        /// 是否Lua协议
        /// </summary>
        public bool IsLua;

        //这个不保存数据 而是在内存中
        public long MenuID;
    }

    //协议属性
    public class MyProtoAttr
    {
        public long AttID;
        public string AttType;

        /// <summary>
        /// 英文名称
        /// </summary>
        public string AttEnName;

        /// <summary>
        /// 中文名称
        /// </summary>
        public string AttCnName;

        /// <summary>
        /// 是否循环项
        /// </summary>
        public bool AttIsLoop;

        /// <summary>
        /// 隶属于循环项
        /// </summary>
        public string AttToLoop;

        /// <summary>
        /// 隶属于布尔
        /// </summary>
        public string AttToBool;

        /// <summary>
        /// 隶属于布尔结果
        /// </summary>
        public bool AttToBoolResult;

        /// <summary>
        /// 隶属于自定义
        /// </summary>
        public string AttToCus;

        //不存储，计算用的
        public bool AttIsUsed;
    }
}