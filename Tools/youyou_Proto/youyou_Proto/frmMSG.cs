using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadExcel
{
    public partial class frmMSG : Form
    {
        public frmMSG()
        {
            InitializeComponent();

            LoadXml();
        }

        private void LoadXml()
        {
            groupProtoInfo.Enabled = false;
            DataMgr.Instance.LoadXml();

            if (DataMgr.Instance.MenuList != null)
            {
                for (int i = 0; i < DataMgr.Instance.MenuList.Count; i++)
                {
                    TreeNode node = new TreeNode();
                    node.Text = string.Format("{0}", DataMgr.Instance.MenuList[i].MenuName);




                    NodeTag tag = new NodeTag();
                    tag.IsMenu = true;
                    tag.Menu = DataMgr.Instance.MenuList[i];

                    //========================================================

                    {
                        if (tag.Menu.HasChild)
                        {
                            List<MyProto> lstProto = tag.Menu.ProtoList;

                            if (lstProto != null && lstProto.Count > 0)
                            {
                                for (int j = 0; j < lstProto.Count; j++)
                                {
                                    MyProto proto = lstProto[j];

                                    NodeTag protoTag = new NodeTag();
                                    protoTag.IsMenu = false;
                                    protoTag.Proto = proto;

                                    TreeNode nodeProto = new TreeNode();
                                    nodeProto.Text = string.Format("({0}){1}", proto.ProtoCode, proto.ProtoCnName);
                                    nodeProto.Tag = protoTag;
                                    node.Nodes.Add(nodeProto);
                                }
                            }
                        }
                    }

                    //========================================================

                    node.Tag = tag;
                    myTree.Nodes.Add(node);


                    myTree.Refresh();
                }
            }
        }

        private TreeNode m_CurrentSelectNode = null;


        #region 菜单操作
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddMenu_Click(object sender, EventArgs e)
        {
            frmModelEdit frmModelEdit = new frmModelEdit();
            if (frmModelEdit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                //DATA
                MyMenu menu = new MyMenu();

                menu.MenuId = DateTime.Now.Ticks;
                menu.MenuName = frmModelEdit.ModelName;
                DataMgr.Instance.AddMenu(menu);
                DataMgr.Instance.SaveXml();


                //UI
                NodeTag _tag = new NodeTag();
                _tag.IsMenu = true;
                _tag.Menu = menu;

                TreeNode node = new TreeNode();
                node.Text = string.Format("{0}", menu.MenuName);
                node.Tag = _tag;
                myTree.Nodes.Add(node);
                myTree.Refresh();
                myTree.Focus();
                myTree.SelectedNode = node;
            }
        }

        //双击
        private void myTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            m_CurrentSelectNode = myTree.SelectedNode;
            if (m_CurrentSelectNode == null) return;

            NodeTag tag = m_CurrentSelectNode.Tag as NodeTag;

            if (tag == null) return;
            if (tag.IsMenu)
            {
                frmModelEdit frmModelEdit = new frmModelEdit();

                frmModelEdit.ModelName = tag.Menu.MenuName;

                if (frmModelEdit.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    tag.Menu.MenuName = frmModelEdit.ModelName;

                    //DATA
                    DataMgr.Instance.UpdateMenu(tag.Menu);
                    DataMgr.Instance.SaveXml();

                    //UI
                    m_CurrentSelectNode.Text = string.Format("{0}", tag.Menu.MenuName);
                    myTree.Refresh();
                    myTree.Focus();
                    myTree.SelectedNode = m_CurrentSelectNode;
                }
            }
        }

        private void btnDelMenu_Click(object sender, EventArgs e)
        {
            m_CurrentSelectNode = myTree.SelectedNode;

            NodeTag tag = m_CurrentSelectNode.Tag as NodeTag;
            if (tag == null) return;

            if (tag.IsMenu)
            {
                if (MessageBox.Show("您确定要删除此模块吗？谨慎操作", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    //DATA
                    DataMgr.Instance.DeleteMenu(tag.Menu);
                    DataMgr.Instance.SaveXml();

                    //UI
                    myTree.Nodes.Remove(m_CurrentSelectNode);
                    myTree.Refresh();
                }
            }
            else
            {
                MessageBox.Show("这个按钮只能删除模块");
            }
        }
        #endregion

        //================================协议操作=====================================

        #region btnAddProto_Click 添加协议
        /// <summary>
        /// 添加协议
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddProto_Click(object sender, EventArgs e)
        {
            m_CurrentSelectNode = myTree.SelectedNode;

            if (m_CurrentSelectNode == null) return;

            NodeTag tag = m_CurrentSelectNode.Tag as NodeTag;
            if (tag == null) return;

            if (!tag.IsMenu)
            {
                MessageBox.Show("您需要在某个模块下添加协议");
                return;
            }

            //DATA
            MyProto proto = new MyProto();
            proto.ProtoID = DateTime.Now.Ticks;
            proto.ProtoCode = "-1";
            proto.ProtoCnName = "新建协议";
            proto.ProtoEnName = "";
            proto.ProtoDesc = "";
            proto.MenuID = tag.Menu.MenuId;

            DataMgr.Instance.AddProto(proto, tag.Menu.MenuId);
            DataMgr.Instance.SaveXml();


            //UI
            NodeTag _tag = new NodeTag();
            _tag.IsMenu = false;
            _tag.Proto = proto;

            TreeNode node = new TreeNode();
            node.Text = string.Format("({0}){1}", proto.ProtoCode, proto.ProtoCnName);
            node.Tag = _tag;
            m_CurrentSelectNode.Nodes.Add(node);

            myTree.Refresh();
            myTree.Focus();
            myTree.SelectedNode = node;

            m_CurrentSelectNode.Expand();

            m_CurrentSelectNode = node;
            ShowProtoInfo(proto);
        }
        #endregion

        private MyProto m_CurrentProto;

        private void ShowProtoInfo(MyProto proto)
        {
            if (proto == null) return;

            m_CurrentProto = proto;

            groupProtoInfo.Enabled = true;

            this.txtProtoCode.Text = proto.ProtoCode;
            this.txtProtoEnName.Text = proto.ProtoEnName;
            this.txtProtoCnName.Text = proto.ProtoCnName;
            this.txtProtoDesc.Text = proto.ProtoDesc;

            this.protoCategory.SelectedValue = proto.ProtoCategory;
            this.checkBoxCSharp.Checked = proto.IsCSharp;
            this.checkBoxLua.Checked = proto.IsLua;

            //====================================

            List<MyProtoAttr> lst = DataMgr.Instance.GetProtoArrtList(proto.ProtoID, proto.MenuID);

            dvGrid.Rows.Clear();

            for (int i = 0; i < lst.Count; i++)
            {
                DataGridViewRow dataGridViewRow = dvGrid.Rows[0].Clone() as DataGridViewRow;


                dataGridViewRow.Tag = lst[i];

                dataGridViewRow.Cells[0].Value = lst[i].AttType;
                dataGridViewRow.Cells[1].Value = lst[i].AttEnName;
                dataGridViewRow.Cells[2].Value = lst[i].AttCnName;
                dataGridViewRow.Cells[3].Value = lst[i].AttIsLoop;
                dataGridViewRow.Cells[4].Value = lst[i].AttToLoop;
                dataGridViewRow.Cells[5].Value = lst[i].AttToBool;
                dataGridViewRow.Cells[6].Value = lst[i].AttToBoolResult;
                dataGridViewRow.Cells[7].Value = lst[i].AttToCus;

                dvGrid.Rows.Add(dataGridViewRow);
            }
        }

        private void btnSaveProto_Click(object sender, EventArgs e)
        {
            SaveProto();
        }

        private void SaveProto()
        {
            if (m_CurrentSelectNode == null) return;

            if (this.txtProtoCode.Text.Trim() == "-1" || string.IsNullOrEmpty(this.txtProtoCode.Text.Trim()))
            {
                MessageBox.Show("请输入协议编码 只能是数字");
                this.txtProtoCode.Focus();
                return;
            }

            int protoCode = 0;
            int.TryParse(this.txtProtoCode.Text, out protoCode);

            if (protoCode < 1)
            {
                MessageBox.Show("请输入协议编码 只能是数字");
                this.txtProtoCode.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.txtProtoEnName.Text.Trim()))
            {
                MessageBox.Show("请输入协议英文名称");
                this.txtProtoEnName.Focus();
                return;
            }

            if (string.IsNullOrEmpty(this.txtProtoCnName.Text.Trim()))
            {
                MessageBox.Show("请输入协议中文名称");
                this.txtProtoCnName.Focus();
                return;
            }

            m_CurrentProto.ProtoCode = protoCode.ToString();
            m_CurrentProto.ProtoEnName = this.txtProtoEnName.Text.Trim();
            m_CurrentProto.ProtoCnName = this.txtProtoCnName.Text.Trim();
            m_CurrentProto.ProtoDesc = this.txtProtoDesc.Text.Trim();
            m_CurrentProto.ProtoCategory = this.protoCategory.SelectedValue.ToString();
            m_CurrentProto.IsCSharp = checkBoxCSharp.Checked;
            m_CurrentProto.IsLua = checkBoxLua.Checked;

            DataMgr.Instance.UpdateProto(m_CurrentProto);


            //UI
            m_CurrentSelectNode.Text = string.Format("({0}){1}", m_CurrentProto.ProtoCode, m_CurrentProto.ProtoCnName);

            myTree.Refresh();
            myTree.Focus();
            myTree.SelectedNode = m_CurrentSelectNode;

            //========================保存数据表===========================

            List<MyProtoAttr> lst = new List<MyProtoAttr>();

            int id = 1;
            for (int i = 0; i < dvGrid.Rows.Count; i++)
            {
                id++;
                lst.Add(new MyProtoAttr()
                {
                    AttID = dvGrid.Rows[i].Tag == null ? DateTime.Now.Ticks + id : ((MyProtoAttr)dvGrid.Rows[i].Tag).AttID,
                    AttType = dvGrid.Rows[i].Cells[0].Value == null ? "" : dvGrid.Rows[i].Cells[0].Value.ToString(),
                    AttEnName = dvGrid.Rows[i].Cells[1].Value == null ? "" : dvGrid.Rows[i].Cells[1].Value.ToString(),
                    AttCnName = dvGrid.Rows[i].Cells[2].Value == null ? "" : dvGrid.Rows[i].Cells[2].Value.ToString(),
                    AttIsLoop = dvGrid.Rows[i].Cells[3].Value == null ? false : (bool)dvGrid.Rows[i].Cells[3].Value,
                    AttToLoop = dvGrid.Rows[i].Cells[4].Value == null ? "" : dvGrid.Rows[i].Cells[4].Value.ToString(),
                    AttToBool = dvGrid.Rows[i].Cells[5].Value == null ? "" : dvGrid.Rows[i].Cells[5].Value.ToString(),
                    AttToBoolResult = dvGrid.Rows[i].Cells[6].Value == null ? false : (bool)dvGrid.Rows[i].Cells[6].Value,
                    AttToCus = dvGrid.Rows[i].Cells[7].Value == null ? "" : dvGrid.Rows[i].Cells[7].Value.ToString()
                });
            }

            //把最后一个移除
            lst.RemoveAt(lst.Count - 1);

            DataMgr.Instance.ProtoDataSave(lst, m_CurrentProto.ProtoID, m_CurrentProto.MenuID);

            DataMgr.Instance.SaveXml();
        }

        private void btnDelProto_Click(object sender, EventArgs e)
        {
            if (m_CurrentSelectNode == null) return;

            NodeTag tag = m_CurrentSelectNode.Tag as NodeTag;
            if (tag == null) return;

            if (!tag.IsMenu)
            {
                if (MessageBox.Show("您确定要删除此模块吗？谨慎操作", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    //DATA
                    DataMgr.Instance.DeleteProto(tag.Proto);
                    DataMgr.Instance.SaveXml();

                    //UI
                    m_CurrentSelectNode.Parent.Nodes.Remove(m_CurrentSelectNode);
                    myTree.Refresh();
                    m_CurrentSelectNode = null;
                }
            }
            else
            {
                MessageBox.Show("这个按钮只能删除协议");
            }
        }

        private void myTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            m_CurrentSelectNode = e.Node;

            if (m_CurrentSelectNode == null) return;

            NodeTag tag = m_CurrentSelectNode.Tag as NodeTag;

            if (tag == null) return;
            if (!tag.IsMenu)
            {
                ShowProtoInfo(tag.Proto);
            }
            else
            {
                groupProtoInfo.Enabled = false;
            }
        }

        //========================上下移动节点=============================================

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (m_CurrentSelectNode == null) return;

            NodeTag tag = m_CurrentSelectNode.Tag as NodeTag;

            if (tag == null) return;
            if (tag.IsMenu)
            {
                if (DataMgr.Instance.MoveMenu(tag.Menu, true))
                {
                    DataMgr.Instance.SaveXml();

                    int oldIndex = m_CurrentSelectNode.Index;
                    myTree.Nodes.RemoveAt(oldIndex);
                    myTree.Nodes.Insert(oldIndex - 1, m_CurrentSelectNode);

                    myTree.Refresh();
                    myTree.Focus();
                    myTree.SelectedNode = m_CurrentSelectNode;
                }
            }
            else
            {
                if (DataMgr.Instance.MoveProto(tag.Proto, true))
                {
                    DataMgr.Instance.SaveXml();

                    int oldIndex = m_CurrentSelectNode.Index;

                    TreeNode parent = m_CurrentSelectNode.Parent;

                    parent.Nodes.RemoveAt(oldIndex);
                    parent.Nodes.Insert(oldIndex - 1, m_CurrentSelectNode);

                    myTree.Refresh();
                    myTree.Focus();
                    myTree.SelectedNode = m_CurrentSelectNode;
                }
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (m_CurrentSelectNode == null) return;

            NodeTag tag = m_CurrentSelectNode.Tag as NodeTag;

            if (tag == null) return;
            if (tag.IsMenu)
            {
                if (DataMgr.Instance.MoveMenu(tag.Menu, false))
                {
                    DataMgr.Instance.SaveXml();

                    int oldIndex = m_CurrentSelectNode.Index;
                    myTree.Nodes.RemoveAt(oldIndex);
                    myTree.Nodes.Insert(oldIndex + 1, m_CurrentSelectNode);

                    myTree.Refresh();
                    myTree.Focus();
                    myTree.SelectedNode = m_CurrentSelectNode;
                }
            }
            else
            {
                if (DataMgr.Instance.MoveProto(tag.Proto, false))
                {
                    DataMgr.Instance.SaveXml();

                    int oldIndex = m_CurrentSelectNode.Index;

                    TreeNode parent = m_CurrentSelectNode.Parent;
                    parent.Nodes.RemoveAt(oldIndex);
                    parent.Nodes.Insert(oldIndex + 1, m_CurrentSelectNode);

                    myTree.Refresh();
                    myTree.Focus();
                    myTree.SelectedNode = m_CurrentSelectNode;
                }
            }
        }

        private void btnMovePrevAtt_Click(object sender, EventArgs e)
        {
            if (this.dvGrid.SelectedRows.Count <= 0) return;

            //上移 取本行的第一个索引号 把选择的行 加入本索引号-1的地方

            //选择的行
            DataGridViewSelectedRowCollection rows = this.dvGrid.SelectedRows;

            List<DataGridViewRow> lst = new List<DataGridViewRow>();

            int index = -1; //找出最小的索引值

            for (int i = 0; i < rows.Count; i++)
            {
                if (i == 0)
                {
                    index = rows[i].Index;
                }
                else
                {
                    if (rows[i].Index < index)
                    {
                        index = rows[i].Index;
                    }
                }
            }

            //如果已经是0的位置 直接返回
            if (index == 0) return;

            for (int i = 0; i < rows.Count; i++)
            {
                lst.Add(rows[i]);
                dvGrid.Rows.Remove(rows[i]);
            }

            int toIndex = index - 1;

            for (int i = 0; i < lst.Count; i++)
            {
                dvGrid.Rows.Insert(toIndex, lst[i]);
            }

            for (int i = 0; i < dvGrid.Rows.Count; i++)
            {
                dvGrid.Rows[i].Selected = false;
            }

            for (int i = 0; i < lst.Count; i++)
            {
                lst[i].Selected = true;
            }
        }

        private void btnMoveNextAtt_Click(object sender, EventArgs e)
        {
            if (this.dvGrid.SelectedRows.Count <= 0) return;

            //上移 取本行的第一个索引号 把选择的行 加入本索引号-1的地方

            //选择的行
            DataGridViewSelectedRowCollection rows = this.dvGrid.SelectedRows;

            List<DataGridViewRow> lst = new List<DataGridViewRow>();

            int index = -1; //找出最小的索引值
            int maxIndex = -1; //最大索引

            for (int i = 0; i < rows.Count; i++)
            {
                if (i == 0)
                {
                    index = rows[i].Index;
                    maxIndex = index;
                }
                else
                {
                    if (rows[i].Index < index)
                    {
                        index = rows[i].Index;
                    }
                    else if (rows[i].Index > index)
                    {
                        maxIndex = rows[i].Index;
                    }
                }
            }

            //如果已经是0的位置 直接返回
            if (maxIndex == dvGrid.Rows.Count - 2) return;

            for (int i = 0; i < rows.Count; i++)
            {
                lst.Add(rows[i]);
                dvGrid.Rows.Remove(rows[i]);
            }

            int toIndex = index + 1;

            for (int i = 0; i < lst.Count; i++)
            {
                dvGrid.Rows.Insert(toIndex, lst[i]);
            }

            for (int i = 0; i < dvGrid.Rows.Count; i++)
            {
                dvGrid.Rows[i].Selected = false;
            }

            for (int i = 0; i < lst.Count; i++)
            {
                lst[i].Selected = true;
            }
        }

        private void btnDelAtt_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("您确定要删除此字段吗？谨慎操作", "提示", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                DataGridViewSelectedRowCollection rows = this.dvGrid.SelectedRows;

                for (int i = 0; i < rows.Count; i++)
                {
                    dvGrid.Rows.Remove(rows[i]);
                }
            }
        }

        private void btnAttpreview_Click(object sender, EventArgs e)
        {
            //预览前需要先保存
            SaveProto();

            List<MyProtoAttr> lst = DataMgr.Instance.GetProtoArrtList(m_CurrentProto.ProtoID, m_CurrentProto.MenuID);

            StringBuilder sbr = new StringBuilder();

            sbr.Append("{\r\n");
            for (int i = 0; i < lst.Count; i++)
            {
                MyProtoAttr attr = lst[i];

                if (attr.AttIsUsed) continue;

                if (attr.AttType == "bool" || attr.AttType == "byte" || attr.AttType == "short" || attr.AttType == "int" || attr.AttType == "long" || attr.AttType == "string" || attr.AttType == "char" || attr.AttType == "float" || attr.AttType == "decimal")
                {
                    if (string.IsNullOrEmpty(attr.AttToCus))
                    {
                        sbr.AppendFormat("      {0} {1} //{2}\r\n", attr.AttType, attr.AttEnName, attr.AttCnName);
                    }
                }

                //如果是布尔变量
                if (attr.AttType == "bool")
                {
                    #region 布尔
                    bool isHasSuccess = false;
                    //查找隶属于这个bool成功的
                    {
                        List<MyProtoAttr> list = GetListByToBoolName(lst, attr.AttEnName, true);
                        if (list != null && list.Count > 0)
                        {
                            isHasSuccess = true;
                            sbr.AppendFormat("      if({0})\r\n", attr.AttEnName);
                            sbr.Append("      {\r\n");
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                //然后写到这个bool变量下
                                sbr.AppendFormat("            {0} {1} //{2}\r\n", list[j].AttType, list[j].AttEnName, list[j].AttCnName);
                            }
                            sbr.Append("      }\r\n");
                        }
                    }

                    //查找隶属于这个bool失败的
                    {
                        List<MyProtoAttr> list = GetListByToBoolName(lst, attr.AttEnName, false);
                        if (list != null && lst.Count > 0)
                        {
                            if (isHasSuccess)
                            {
                                sbr.AppendFormat("      else\r\n", attr.AttEnName);
                            }
                            else
                            {
                                //如果没有成功项
                                sbr.AppendFormat("      if(!{0})\r\n", attr.AttEnName);
                            }
                            sbr.Append("      {\r\n");
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                //然后写到这个bool变量下
                                sbr.AppendFormat("            {0} {1} //{2}\r\n", list[j].AttType, list[j].AttEnName, list[j].AttCnName);
                            }
                            sbr.Append("      }\r\n");
                        }
                    }
                    #endregion
                }
                else if ((attr.AttType == "byte" || attr.AttType == "short" || attr.AttType == "int" || attr.AttType == "long") && attr.AttIsLoop)
                {
                    #region 循环项目

                    sbr.AppendFormat("      for(int i=0;i<{0};i++)\r\n", attr.AttEnName);
                    sbr.Append("      {\r\n");
                    //查找隶属于这个循环项的
                    {
                        List<MyProtoAttr> list = lst.Where(p => p.AttToLoop.Equals(attr.AttEnName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                        if (list != null && list.Count > 0)
                        {
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                //然后写到这个bool变量下
                                sbr.AppendFormat("            {0} {1} //{2}\r\n", list[j].AttType, list[j].AttEnName, list[j].AttCnName);
                            }
                        }
                    }
                    sbr.Append("      }\r\n");

                    #endregion
                }
                else if (attr.AttType != "byte" && attr.AttType != "short" && attr.AttType != "int" && attr.AttType != "long" && attr.AttType != "string" && attr.AttType != "char" && attr.AttType != "float" && attr.AttType != "decimal" && attr.AttType != "bool" && attr.AttType != "ushort")
                {
                    //查找隶属于这个自定义的项
                    List<MyProtoAttr> list = lst.Where(p => p.AttToCus.Equals(attr.AttEnName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                    if (list != null && list.Count > 0)
                    {
                        //如果是自定义
                        if (!attr.AttIsLoop)
                        {
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                //然后写到这个bool变量下
                                sbr.AppendFormat("      {0} {1}.{2} //{3}\r\n", list[j].AttType, attr.AttEnName, list[j].AttEnName, list[j].AttCnName);
                            }
                        }
                        else
                        {
                            sbr.AppendFormat("      int {0}Count {1}\r\n", attr.AttEnName, attr.AttCnName);
                            sbr.AppendFormat("      for(int i=0;i<{0}Count;i++)\r\n", attr.AttEnName);
                            sbr.Append("      {\r\n");
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                //然后写到这个bool变量下
                                sbr.AppendFormat("            {0} {1}.{2} //{3}\r\n", list[j].AttType, attr.AttEnName, list[j].AttEnName, list[j].AttCnName);
                            }
                            sbr.Append("      }\r\n");
                        }
                    }
                }
            }
            sbr.Append("}\r\n");
            this.txtAttpreview.Text = sbr.ToString();
        }

        //根据隶书布尔名称和结果查找项
        private List<MyProtoAttr> GetListByToBoolName(List<MyProtoAttr> lst, string boolName, bool isTrue)
        {
            return lst.Where(p => p.AttToBool.Equals(boolName, StringComparison.CurrentCultureIgnoreCase)).Where(p => p.AttToBoolResult == isTrue).ToList();
        }

        private void btnCreateCode_Click(object sender, EventArgs e)
        {
            CreateProto(m_CurrentProto, true);
            CreateServerProto(m_CurrentProto, true);
            CreateProtoHandler(m_CurrentProto);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="outType">0=对应的lua类型, 1=Lua默认值</param>
        /// <returns></returns>
        private string ToLuaType(string type, int outType)
        {
            string str = string.Empty;
            string str1 = string.Empty;
            switch (type)
            {
                case "short":
                    str = "number";
                    str1 = "0";
                    break;
                case "ushort":
                    str = "number";
                    str1 = "0";
                    break;
                case "int":
                    str = "number";
                    str1 = "0";
                    break;
                case "uint":
                    str = "number";
                    str1 = "0";
                    break;
                case "long":
                    str = "number";
                    str1 = "0";
                    break;
                case "ulong":
                    str = "number";
                    str1 = "0";
                    break;
                case "float":
                    str = "number";
                    str1 = "0";
                    break;
                case "string":
                    str = "string";
                    str1 = "\"\"";
                    break;
                case "bool":
                    str = "boolean";
                    str1 = "false";
                    break;
                case "byte":
                    str = "number";
                    str1 = "0";
                    break;
            }

            if (outType == 0)
            {
                return str;
            }
            else if (outType == 1)
            {
                return str1;
            }
            else
            {
                return str;
            }
        }

        private string ChangeTypeName(string type)
        {
            string str = string.Empty;

            switch (type)
            {
                case "short":
                    str = "Short()";
                    break;
                case "ushort":
                    str = "UShort()";
                    break;
                case "int":
                    str = "Int()";
                    break;
                case "uint":
                    str = "UInt()";
                    break;
                case "long":
                    str = "Long()";
                    break;
                case "ulong":
                    str = "ULong()";
                    break;
                case "float":
                    str = "Float()";
                    break;
                case "string":
                    str = "UTF8String()";
                    break;
                case "bool":
                    str = "Bool()";
                    break;
                case "byte":
                    str = "Byte()";
                    break;
            }

            return str;
        }

        private void CreateServerProto(MyProto proto, bool isSave = false)
        {
            if (isSave)
            {
                //生成协议代码
                SaveProto();
            }

            List<MyProtoAttr> lst = DataMgr.Instance.GetProtoArrtList(proto.ProtoID, proto.MenuID);

            StringBuilder sbr = new StringBuilder();

            sbr.AppendFormat("//===================================================\r\n");
            sbr.AppendFormat("//作    者：边涯  http://www.u3dol.com\r\n");
            sbr.AppendFormat("//创建时间：{0}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sbr.AppendFormat("//备    注：\r\n");
            sbr.AppendFormat("//===================================================\r\n");
            sbr.AppendFormat("using System.Collections;\r\n");
            sbr.AppendFormat("using System.Collections.Generic;\r\n");
            sbr.AppendFormat("using System;\r\n");
            sbr.Append("\r\n");
            sbr.AppendFormat("/// <summary>\r\n");
            sbr.AppendFormat("/// {0}\r\n", proto.ProtoCnName);
            sbr.AppendFormat("/// </summary>\r\n");
            sbr.AppendFormat("public struct {0}Proto : IProto\r\n", proto.ProtoEnName);
            sbr.AppendFormat("{{\r\n");
            sbr.AppendFormat("    public ushort ProtoCode {{ get {{ return {0}; }} }}\r\n", proto.ProtoCode);

            sbr.AppendFormat("    public string ProtoEnName {{ get {{ return \"{0}\"; }} }}\r\n", proto.ProtoEnName);
            sbr.Append("\r\n");

            foreach (var item in lst)
            {
                if (!string.IsNullOrEmpty(item.AttToLoop))
                {
                    //这里是循环项的从属属性

                    sbr.AppendFormat("    public List<{0}> {1}List; //{2}\r\n", item.AttType, item.AttEnName, item.AttCnName);
                }
                else
                {
                    //生成标准的基本属性
                    if (string.IsNullOrEmpty(item.AttToCus))
                    {
                        sbr.AppendFormat("    public {0} {1}; //{2}\r\n", item.AttType, item.AttEnName, item.AttCnName);
                    }
                }
            }
            sbr.Append("\r\n");



            #region 生成自定义类型结构
            foreach (var item in lst)
            {
                //如果是自定义属性类型
                if (item.AttType != "byte" && item.AttType != "short" && item.AttType != "int" && item.AttType != "long" && item.AttType != "string" && item.AttType != "char" && item.AttType != "float" && item.AttType != "decimal" && item.AttType != "bool" && item.AttType != "ushort")
                {
                    sbr.AppendFormat("    [Serializable]\r\n");
                    sbr.AppendFormat("    /// <summary>\r\n");
                    sbr.AppendFormat("    /// {0}\r\n", item.AttCnName);
                    sbr.AppendFormat("    /// </summary>\r\n");
                    sbr.AppendFormat("    public struct {0}\r\n", item.AttType);
                    sbr.AppendFormat("    {{\r\n");

                    string strForNextLua = "";
                    //====================隶属于此自定义项的属性=================
                    //查找隶属于这个循环项的
                    {
                        List<MyProtoAttr> list = lst.Where(p => p.AttToCus.Equals(item.AttEnName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                        if (list != null && list.Count > 0)
                        {
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;
                                sbr.AppendFormat("        public {0} {1}; //{2}\r\n", list[j].AttType, list[j].AttEnName, list[j].AttCnName);

                                strForNextLua += string.Format("{0} = {1}, ", list[j].AttEnName, ToLuaType(list[j].AttType, 1));
                            }
                        }
                    }
                    sbr.AppendFormat("    }}\r\n");
                    sbr.Append("\r\n");
                }
            }
            #endregion

            #region ToArray 方法
            sbr.AppendFormat("    public byte[] ToArray(MMO_MemoryStream ms)\r\n");
            sbr.AppendFormat("    {{\r\n");
            sbr.AppendFormat("        ms.SetLength(0);\r\n");
            sbr.AppendFormat("        ms.WriteUShort(ProtoCode);\r\n");
            sbr.Append("\r\n");

            //写入数据流
            foreach (var item in lst)
            {
                if (item.AttIsUsed) continue;

                sbr.AppendFormat("        ms.Write{0}({1});\r\n", ChangeTypeName(item.AttType).Replace("()", ""), item.AttEnName);

                if (item.AttType == "bool")
                {
                    //如果是布尔变量
                    #region 布尔
                    bool isHasSuccess = false;
                    //查找隶属于这个bool成功的
                    {
                        List<MyProtoAttr> list = GetListByToBoolName(lst, item.AttEnName, true);
                        if (list != null && list.Count > 0)
                        {
                            isHasSuccess = true;
                            sbr.AppendFormat("        if ({0})\r\n", item.AttEnName);

                            sbr.Append("        {\r\n");
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                //然后写到这个bool变量下
                                sbr.AppendFormat("            ms.Write{0}({1});\r\n", ChangeTypeName(list[j].AttType).Replace("()", ""), list[j].AttEnName);
                            }
                            sbr.Append("        }\r\n");
                        }
                    }

                    //查找隶属于这个bool失败的
                    {
                        List<MyProtoAttr> list = GetListByToBoolName(lst, item.AttEnName, false);
                        if (list != null && lst.Count > 0)
                        {
                            if (isHasSuccess)
                            {
                                sbr.Append("        else\r\n");
                            }
                            else
                            {
                                //如果没有成功项
                                sbr.AppendFormat("        if (!{0})\r\n", item.AttEnName);
                            }
                            sbr.Append("        {\r\n");
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                //然后写到这个bool变量下
                                sbr.AppendFormat("            ms.Write{0}({1});\r\n", ChangeTypeName(list[j].AttType).Replace("()", ""), list[j].AttEnName);
                            }
                            sbr.Append("        }\r\n");
                        }
                    }
                    #endregion
                }
                else if ((item.AttType == "byte" || item.AttType == "short" || item.AttType == "int" || item.AttType == "long") && item.AttIsLoop)
                {
                    #region 循环项目

                    sbr.AppendFormat("        for (int i = 0; i < {0}; i++)\r\n", item.AttEnName);

                    sbr.Append("        {\r\n");
                    //查找隶属于这个循环项的
                    {
                        List<MyProtoAttr> list = lst.Where(p => p.AttToLoop.Equals(item.AttEnName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                        if (list != null && list.Count > 0)
                        {
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                //如果是自定义属性类型
                                if (list[j].AttType != "byte" && list[j].AttType != "short" && list[j].AttType != "int" && list[j].AttType != "long" && list[j].AttType != "string" && list[j].AttType != "char" && list[j].AttType != "float" && list[j].AttType != "decimal" && list[j].AttType != "bool" && list[j].AttType != "ushort")
                                {
                                    {
                                        {
                                            List<MyProtoAttr> list2 = lst.Where(p => p.AttToCus.Equals(list[j].AttEnName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                                            if (list2 != null && list2.Count > 0)
                                            {
                                                for (int l = 0; l < list2.Count; l++)
                                                {
                                                    //把已经提前用的了设置一下
                                                    lst.Where(p => p.AttID == list2[l].AttID).First().AttIsUsed = true;
                                                    sbr.AppendFormat("            ms.Write{0}({1}List[i].{2});\r\n", ChangeTypeName(list2[l].AttType).Replace("()", ""), list[j].AttEnName, list2[l].AttEnName);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //然后写到这个bool变量下
                                    sbr.AppendFormat("            ms.Write{0}({1}List[i]);\r\n", ChangeTypeName(list[j].AttType).Replace("()", ""), list[j].AttEnName, list[j].AttCnName);
                                }
                            }
                        }
                    }
                    sbr.Append("        }\r\n");

                    #endregion
                }


            }
            sbr.Append("\r\n");
            sbr.AppendFormat("        return ms.ToArray();\r\n");
            sbr.AppendFormat("    }}\r\n");
            sbr.Append("\r\n");

            #endregion

            #region GetProto 方法
            sbr.AppendFormat("    public static {0}Proto GetProto(MMO_MemoryStream ms, byte[] buffer)\r\n", proto.ProtoEnName);
            sbr.AppendFormat("    {{\r\n");
            sbr.AppendFormat("        {0}Proto proto = new {0}Proto();\r\n", proto.ProtoEnName);
            sbr.AppendFormat("        ms.SetLength(0);\r\n");
            sbr.AppendFormat("        ms.Write(buffer, 0, buffer.Length);\r\n");
            sbr.AppendFormat("        ms.Position = 0;\r\n");
            sbr.Append("\r\n");


            //从数据流读取
            foreach (var item in lst)
            {
                if (item.AttIsUsed) continue;

                if (item.AttType == "byte")
                {
                    sbr.AppendFormat("        proto.{0} = (byte)ms.Read{1};\r\n", item.AttEnName, ChangeTypeName(item.AttType));
                }
                else
                {
                    sbr.AppendFormat("        proto.{0} = ms.Read{1};\r\n", item.AttEnName, ChangeTypeName(item.AttType));
                }


                if (item.AttType == "bool")
                {
                    //如果是布尔变量
                    #region 布尔
                    bool isHasSuccess = false;
                    //查找隶属于这个bool成功的
                    {
                        List<MyProtoAttr> list = GetListByToBoolName(lst, item.AttEnName, true);
                        if (list != null && list.Count > 0)
                        {
                            isHasSuccess = true;
                            sbr.AppendFormat("        if (proto.{0})\r\n", item.AttEnName);
                            sbr.Append("        {\r\n");
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                //然后写到这个bool变量下
                                if (list[j].AttType == "byte")
                                {
                                    sbr.AppendFormat("            proto.{0} = (byte)ms.Read{1};\r\n", list[j].AttEnName, ChangeTypeName(list[j].AttType));
                                }
                                else
                                {
                                    sbr.AppendFormat("            proto.{0} = ms.Read{1};\r\n", list[j].AttEnName, ChangeTypeName(list[j].AttType));
                                }
                            }
                            sbr.Append("        }\r\n");
                        }
                    }

                    //查找隶属于这个bool失败的
                    {
                        List<MyProtoAttr> list = GetListByToBoolName(lst, item.AttEnName, false);
                        if (list != null && lst.Count > 0)
                        {
                            if (isHasSuccess)
                            {
                                sbr.Append("        else\r\n");
                            }
                            else
                            {
                                //如果没有成功项
                                sbr.AppendFormat("        if (!proto.{0})\r\n", item.AttEnName);
                            }
                            sbr.Append("        {\r\n");
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                if (list[j].AttType == "byte")
                                {
                                    //然后写到这个bool变量下
                                    sbr.AppendFormat("            proto.{0} = (byte)ms.Read{1};\r\n", list[j].AttEnName, ChangeTypeName(list[j].AttType));
                                }
                                else
                                {
                                    sbr.AppendFormat("            proto.{0} = ms.Read{1};\r\n", list[j].AttEnName, ChangeTypeName(list[j].AttType));
                                }
                            }
                            sbr.Append("        }\r\n");
                        }
                    }
                    #endregion
                }
                else if ((item.AttType == "byte" || item.AttType == "short" || item.AttType == "int" || item.AttType == "long") && item.AttIsLoop)
                {
                    #region 循环项目

                    //======================================
                    //1.定义列表
                    {
                        List<MyProtoAttr> list = lst.Where(p => p.AttToLoop.Equals(item.AttEnName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                        if (list != null && list.Count > 0)
                        {
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;
                                sbr.AppendFormat("        proto.{0}List = new List<{1}>();\r\n", list[j].AttEnName, list[j].AttType);
                            }
                        }
                    }
                    //======================================

                    sbr.AppendFormat("        for (int i = 0; i < proto.{0}; i++)\r\n", item.AttEnName);

                    sbr.Append("        {\r\n");
                    //查找隶属于这个循环项的
                    {
                        List<MyProtoAttr> list = lst.Where(p => p.AttToLoop.Equals(item.AttEnName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                        if (list != null && list.Count > 0)
                        {
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                //如果是自定义属性类型
                                if (list[j].AttType != "byte" && list[j].AttType != "short" && list[j].AttType != "int" && list[j].AttType != "long" && list[j].AttType != "string" && list[j].AttType != "char" && list[j].AttType != "float" && list[j].AttType != "decimal" && list[j].AttType != "bool" && list[j].AttType != "ushort")
                                {
                                    {
                                        {
                                            List<MyProtoAttr> list2 = lst.Where(p => p.AttToCus.Equals(list[j].AttEnName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                                            if (list2 != null && list2.Count > 0)
                                            {
                                                sbr.AppendFormat("            {0} _{1} = new {0}();\r\n", list[j].AttType, list[j].AttEnName);

                                                for (int l = 0; l < list2.Count; l++)
                                                {
                                                    //把已经提前用的了设置一下
                                                    lst.Where(p => p.AttID == list2[l].AttID).First().AttIsUsed = true;

                                                    if (list2[l].AttType == "byte")
                                                    {
                                                        sbr.AppendFormat("            _{0}.{1} = (byte)ms.Read{2}();\r\n", list[j].AttEnName, list2[l].AttEnName, ChangeTypeName(list2[l].AttType).Replace("()", ""));
                                                    }
                                                    else
                                                    {
                                                        sbr.AppendFormat("            _{0}.{1} = ms.Read{2}();\r\n", list[j].AttEnName, list2[l].AttEnName, ChangeTypeName(list2[l].AttType).Replace("()", ""));
                                                    }
                                                }
                                                sbr.AppendFormat("            proto.{0}List.Add(_{0});\r\n", list[j].AttEnName);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (list[j].AttType == "byte")
                                    {
                                        //然后写到这个bool变量下
                                        sbr.AppendFormat("            {0} _{1} = (byte)ms.Read{2}();  //{3}\r\n", list[j].AttType, list[j].AttEnName, ChangeTypeName(list[j].AttType).Replace("()", ""), list[j].AttCnName);
                                    }
                                    else
                                    {
                                        sbr.AppendFormat("            {0} _{1} = ms.Read{2}();  //{3}\r\n", list[j].AttType, list[j].AttEnName, ChangeTypeName(list[j].AttType).Replace("()", ""), list[j].AttCnName);
                                    }
                                    sbr.AppendFormat("            proto.{0}List.Add(_{0});\r\n", list[j].AttEnName);
                                }
                            }
                        }
                    }
                    sbr.Append("        }\r\n");
                    #endregion
                }

            }

            sbr.AppendFormat("\r\n");
            sbr.AppendFormat("        return proto;\r\n");
            sbr.AppendFormat("    }}\r\n");
            #endregion

            sbr.AppendFormat("}}");


            using (FileStream fs = new FileStream(string.Format("{0}/{1}Proto.cs", txtServerProtoPath.Text, proto.ProtoEnName), FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(sbr.ToString());
                }
            }
        }

        private void CreateProto(MyProto proto, bool isSave = false)
        {
            if (isSave)
            {
                //生成协议代码
                SaveProto();
            }

            List<MyProtoAttr> lst = DataMgr.Instance.GetProtoArrtList(proto.ProtoID, proto.MenuID);

            StringBuilder sbr = new StringBuilder();

            //Lua协议代码
            StringBuilder sbrLua = new StringBuilder();

            sbr.AppendFormat("//===================================================\r\n");
            sbr.AppendFormat("//作    者：边涯  http://www.u3dol.com\r\n");
            sbr.AppendFormat("//创建时间：{0}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sbr.AppendFormat("//备    注：\r\n");
            sbr.AppendFormat("//===================================================\r\n");
            sbr.AppendFormat("using System.Collections;\r\n");
            sbr.AppendFormat("using System.Collections.Generic;\r\n");
            sbr.AppendFormat("using System;\r\n");
            sbr.AppendFormat("using YouYouFramework;\r\n");
            sbr.Append("\r\n");
            sbr.AppendFormat("/// <summary>\r\n");
            sbr.AppendFormat("/// {0}\r\n", proto.ProtoCnName);
            sbr.AppendFormat("/// </summary>\r\n");
            sbr.AppendFormat("public struct {0}Proto : IProto\r\n", proto.ProtoEnName);
            sbr.AppendFormat("{{\r\n");
            sbr.AppendFormat("    public ushort ProtoCode {{ get {{ return {0}; }} }}\r\n", proto.ProtoCode);

            sbr.AppendFormat("    public string ProtoEnName {{ get {{ return \"{0}\"; }} }}\r\n", proto.ProtoEnName);
            sbr.Append("\r\n");

            //======================lua协议代码开始

            sbrLua.AppendFormat("--{0}\r\n", proto.ProtoCnName);
            sbrLua.AppendFormat("{0}Proto = {{ ProtoCode = {1}, ", proto.ProtoEnName, proto.ProtoCode);


            foreach (var item in lst)
            {
                if (!string.IsNullOrEmpty(item.AttToLoop))
                {
                    //这里是循环项的从属属性

                    sbrLua.AppendFormat("{0}Table = {{ }}, ", item.AttEnName);
                }
                else
                {
                    //生成标准的基本属性
                    if (string.IsNullOrEmpty(item.AttToCus))
                    {
                        sbrLua.AppendFormat("{0} = {1}, ", item.AttEnName, ToLuaType(item.AttType, 1));
                    }
                }
            }
            sbrLua.Remove(sbrLua.Length - 2, 2);
            sbrLua.Append(" }\r\n");


            sbrLua.AppendFormat("local this = {0}Proto;\r\n", proto.ProtoEnName);
            sbrLua.Append("\r\n");
            sbrLua.AppendFormat("{0}Proto.__index = {0}Proto;\r\n", proto.ProtoEnName);
            sbrLua.Append("\r\n");
            sbrLua.AppendFormat("function {0}Proto.New()\r\n", proto.ProtoEnName);
            sbrLua.Append("    local self = { };\r\n");
            sbrLua.AppendFormat("    setmetatable(self, {0}Proto);\r\n", proto.ProtoEnName);
            sbrLua.Append("    return self;\r\n");
            sbrLua.Append("end\r\n\r\n");

            //协议名字
            sbrLua.AppendFormat("function {0}Proto.GetProtoName()\r\n", proto.ProtoEnName);
            sbrLua.AppendFormat("    return \"{0}\";\r\n", proto.ProtoEnName);
            sbrLua.Append("end\r\n");
            //======================lua协议代码结束


            foreach (var item in lst)
            {
                if (!string.IsNullOrEmpty(item.AttToLoop))
                {
                    //这里是循环项的从属属性

                    sbr.AppendFormat("    public List<{0}> {1}List; //{2}\r\n", item.AttType, item.AttEnName, item.AttCnName);
                }
                else
                {
                    //生成标准的基本属性
                    if (string.IsNullOrEmpty(item.AttToCus))
                    {
                        sbr.AppendFormat("    public {0} {1}; //{2}\r\n", item.AttType, item.AttEnName, item.AttCnName);
                    }
                }
            }
            sbr.Append("\r\n");



            #region 生成自定义类型结构
            foreach (var item in lst)
            {
                //如果是自定义属性类型
                if (item.AttType != "byte" && item.AttType != "short" && item.AttType != "int" && item.AttType != "long" && item.AttType != "string" && item.AttType != "char" && item.AttType != "float" && item.AttType != "decimal" && item.AttType != "bool" && item.AttType != "ushort")
                {
                    sbr.AppendFormat("    [Serializable]\r\n");
                    sbr.AppendFormat("    /// <summary>\r\n");
                    sbr.AppendFormat("    /// {0}\r\n", item.AttCnName);
                    sbr.AppendFormat("    /// </summary>\r\n");
                    sbr.AppendFormat("    public struct {0}\r\n", item.AttType);
                    sbr.AppendFormat("    {{\r\n");

                    string strForNextLua = "";
                    //====================隶属于此自定义项的属性=================
                    //查找隶属于这个循环项的
                    {
                        List<MyProtoAttr> list = lst.Where(p => p.AttToCus.Equals(item.AttEnName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                        if (list != null && list.Count > 0)
                        {
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;
                                sbr.AppendFormat("        public {0} {1}; //{2}\r\n", list[j].AttType, list[j].AttEnName, list[j].AttCnName);

                                strForNextLua += string.Format("{0} = {1}, ", list[j].AttEnName, ToLuaType(list[j].AttType, 1));
                            }
                        }
                    }
                    sbr.AppendFormat("    }}\r\n");
                    sbr.Append("\r\n");


                    //======================lua协议代码开始
                    sbrLua.Append("\r\n");
                    sbrLua.Append("\r\n");
                    sbrLua.AppendFormat("--定义{0}\r\n", item.AttCnName);
                    sbrLua.AppendFormat("{0} = {{ {1} }}\r\n", item.AttEnName, strForNextLua.TrimEnd(',', ' '));
                    sbrLua.AppendFormat("{0}.__index = {0};\r\n", item.AttEnName);
                    sbrLua.AppendFormat("function {0}.New()\r\n", item.AttEnName);
                    sbrLua.Append("    local self = { };\r\n");
                    sbrLua.AppendFormat("    setmetatable(self, {0});\r\n", item.AttEnName);
                    sbrLua.Append("    return self;\r\n");
                    sbrLua.Append("end\r\n");
                    //======================lua协议代码结束
                }
            }
            #endregion

            #region ToArray 方法
            sbr.AppendFormat("    public byte[] ToArray()\r\n");
            sbr.AppendFormat("    {{\r\n");
            sbr.AppendFormat("        MMO_MemoryStream ms = GameEntry.Socket.SocketSendMS;\r\n");
            sbr.AppendFormat("        ms.SetLength(0);\r\n");
            sbr.AppendFormat("        ms.WriteUShort(ProtoCode);\r\n");
            sbr.Append("\r\n");

            //======================lua协议代码开始
            sbrLua.Append("\r\n");
            sbrLua.Append("\r\n");
            sbrLua.Append("--发送协议\r\n");
            sbrLua.AppendFormat("function {0}Proto.SendProto(proto)\r\n", proto.ProtoEnName);
            sbrLua.Append("\r\n");
            sbrLua.Append("    local ms = CS.YouYouFramework.GameEntry.Socket.SocketSendMS;\r\n");
            sbrLua.Append("    ms:SetLength(0);\r\n");
            sbrLua.Append("    ms:WriteUShort(proto.ProtoCode);\r\n");
            sbrLua.Append("\r\n");
            //======================lua协议代码结束

            //写入数据流
            foreach (var item in lst)
            {
                if (item.AttIsUsed) continue;

                sbr.AppendFormat("        ms.Write{0}({1});\r\n", ChangeTypeName(item.AttType).Replace("()", ""), item.AttEnName);

                //======================lua协议代码开始
                sbrLua.AppendFormat("    ms:Write{0}(proto.{1});\r\n", ChangeTypeName(item.AttType).Replace("()", ""), item.AttEnName);
                //======================lua协议代码结束

                if (item.AttType == "bool")
                {
                    //如果是布尔变量
                    #region 布尔
                    bool isHasSuccess = false;
                    //查找隶属于这个bool成功的
                    {
                        List<MyProtoAttr> list = GetListByToBoolName(lst, item.AttEnName, true);
                        if (list != null && list.Count > 0)
                        {
                            isHasSuccess = true;
                            sbr.AppendFormat("        if ({0})\r\n", item.AttEnName);
                            sbrLua.AppendFormat("    if(proto.{0}) then\r\n", item.AttEnName);

                            sbr.Append("        {\r\n");
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                //然后写到这个bool变量下
                                sbr.AppendFormat("            ms.Write{0}({1});\r\n", ChangeTypeName(list[j].AttType).Replace("()", ""), list[j].AttEnName);
                                sbrLua.AppendFormat("        ms:Write{0}({1});\r\n", ChangeTypeName(list[j].AttType).Replace("()", ""), list[j].AttEnName);

                            }
                            sbr.Append("        }\r\n");
                        }
                    }

                    //查找隶属于这个bool失败的
                    {
                        List<MyProtoAttr> list = GetListByToBoolName(lst, item.AttEnName, false);
                        if (list != null && lst.Count > 0)
                        {
                            if (isHasSuccess)
                            {
                                sbr.Append("        else\r\n");

                                sbrLua.Append("        else\r\n");
                            }
                            else
                            {
                                //如果没有成功项
                                sbr.AppendFormat("        if (!{0})\r\n", item.AttEnName);
                                sbrLua.AppendFormat("    if(not proto.{0}) then\r\n", item.AttEnName);
                            }
                            sbr.Append("        {\r\n");
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                //然后写到这个bool变量下
                                sbr.AppendFormat("            ms.Write{0}({1});\r\n", ChangeTypeName(list[j].AttType).Replace("()", ""), list[j].AttEnName);
                                sbrLua.AppendFormat("        ms:Write{0}({1});\r\n", ChangeTypeName(list[j].AttType).Replace("()", ""), list[j].AttEnName);
                            }
                            sbr.Append("        }\r\n");
                        }
                    }

                    sbrLua.AppendFormat("    end\r\n");
                    #endregion
                }
                else if ((item.AttType == "byte" || item.AttType == "short" || item.AttType == "int" || item.AttType == "long") && item.AttIsLoop)
                {
                    #region 循环项目

                    sbr.AppendFormat("        for (int i = 0; i < {0}; i++)\r\n", item.AttEnName);

                    //======================lua协议代码开始
                    sbrLua.AppendFormat("    for i = 1, proto.{0}, 1 do\r\n", item.AttEnName);
                    //======================lua协议代码结束

                    sbr.Append("        {\r\n");
                    //查找隶属于这个循环项的
                    {
                        List<MyProtoAttr> list = lst.Where(p => p.AttToLoop.Equals(item.AttEnName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                        if (list != null && list.Count > 0)
                        {
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                //如果是自定义属性类型
                                if (list[j].AttType != "byte" && list[j].AttType != "short" && list[j].AttType != "int" && list[j].AttType != "long" && list[j].AttType != "string" && list[j].AttType != "char" && list[j].AttType != "float" && list[j].AttType != "decimal" && list[j].AttType != "bool" && list[j].AttType != "ushort")
                                {
                                    {
                                        {
                                            List<MyProtoAttr> list2 = lst.Where(p => p.AttToCus.Equals(list[j].AttEnName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                                            if (list2 != null && list2.Count > 0)
                                            {
                                                for (int l = 0; l < list2.Count; l++)
                                                {
                                                    //把已经提前用的了设置一下
                                                    lst.Where(p => p.AttID == list2[l].AttID).First().AttIsUsed = true;
                                                    sbr.AppendFormat("            ms.Write{0}({1}List[i].{2});\r\n", ChangeTypeName(list2[l].AttType).Replace("()", ""), list[j].AttEnName, list2[l].AttEnName);
                                                    sbrLua.AppendFormat("        ms:Write{0}(proto.{1}Table[i].{2});\r\n", ChangeTypeName(list2[l].AttType).Replace("()", ""), list[j].AttEnName, list2[l].AttEnName);
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //然后写到这个bool变量下
                                    sbr.AppendFormat("            ms.Write{0}({1}List[i]);\r\n", ChangeTypeName(list[j].AttType).Replace("()", ""), list[j].AttEnName, list[j].AttCnName);
                                    sbrLua.AppendFormat("        ms:Write{0}(proto.{1}Table[i]);\r\n", ChangeTypeName(list[j].AttType).Replace("()", ""), list[j].AttEnName, list[j].AttCnName);
                                }
                            }
                        }
                    }
                    sbr.Append("        }\r\n");

                    //======================lua协议代码开始
                    sbrLua.Append("    end\r\n");
                    //======================lua协议代码结束

                    #endregion
                }


            }
            sbr.Append("\r\n");
            sbr.AppendFormat("        return ms.ToArray();\r\n");
            sbr.AppendFormat("    }}\r\n");
            sbr.Append("\r\n");

            //======================lua协议代码开始
            sbrLua.Append("\r\n");
            sbrLua.Append("    if(CS.YouYouFramework.GameEntry.Lua.DebugLogProto == true) then\r\n");
            sbrLua.Append("        print(string.format(\"<color=#ffa200>发送消息:</color><color=#FFFB80>%s %s</color>\", this.GetProtoName(), proto.ProtoCode));\r\n");
            sbrLua.Append("        print(string.format(\"<color=#ffdeb3>==>>%s</color>\", json.encode(proto)));\r\n");
            sbrLua.Append("    end\r\n");
            sbrLua.Append("\r\n");
            sbrLua.Append("    CS.YouYouFramework.GameEntry.Socket:SendMsg(ms:ToArray());\r\n");
            sbrLua.Append("end\r\n");
            //======================lua协议代码结束

            #endregion

            #region GetProto 方法
            sbr.AppendFormat("    public static {0}Proto GetProto(byte[] buffer)\r\n", proto.ProtoEnName);
            sbr.AppendFormat("    {{\r\n");
            sbr.AppendFormat("        {0}Proto proto = new {0}Proto();\r\n", proto.ProtoEnName);
            sbr.AppendFormat("        MMO_MemoryStream ms = GameEntry.Socket.SocketReceiveMS;\r\n");
            sbr.AppendFormat("        ms.SetLength(0);\r\n");
            sbr.AppendFormat("        ms.Write(buffer, 0, buffer.Length);\r\n");
            sbr.AppendFormat("        ms.Position = 0;\r\n");
            sbr.Append("\r\n");

            //======================lua协议代码开始
            sbrLua.Append("\r\n");
            sbrLua.Append("\r\n");
            sbrLua.Append("--解析协议\r\n");
            sbrLua.AppendFormat("function {0}Proto.GetProto(buffer)\r\n", proto.ProtoEnName);
            sbrLua.Append("\r\n");
            sbrLua.AppendFormat("    local proto = {0}Proto.New(); --实例化一个协议对象\r\n", proto.ProtoEnName);
            sbrLua.Append("    local ms = CS.YouYouFramework.GameEntry.Lua:LoadSocketReceiveMS(buffer);\r\n");
            sbrLua.Append("\r\n");
            //======================lua协议代码结束


            //从数据流读取
            foreach (var item in lst)
            {
                if (item.AttIsUsed) continue;

                if (item.AttType == "byte")
                {
                    sbr.AppendFormat("        proto.{0} = (byte)ms.Read{1};\r\n", item.AttEnName, ChangeTypeName(item.AttType));
                    sbrLua.AppendFormat("    proto.{0} = ms:Read{1};\r\n", item.AttEnName, ChangeTypeName(item.AttType));
                }
                else
                {
                    sbr.AppendFormat("        proto.{0} = ms.Read{1};\r\n", item.AttEnName, ChangeTypeName(item.AttType));
                    sbrLua.AppendFormat("    proto.{0} = ms:Read{1};\r\n", item.AttEnName, ChangeTypeName(item.AttType));
                }


                if (item.AttType == "bool")
                {
                    //如果是布尔变量
                    #region 布尔
                    bool isHasSuccess = false;
                    //查找隶属于这个bool成功的
                    {
                        List<MyProtoAttr> list = GetListByToBoolName(lst, item.AttEnName, true);
                        if (list != null && list.Count > 0)
                        {
                            isHasSuccess = true;
                            sbr.AppendFormat("        if (proto.{0})\r\n", item.AttEnName);
                            sbrLua.AppendFormat("    if(proto.{0}) then\r\n", item.AttEnName);
                            sbr.Append("        {\r\n");
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                //然后写到这个bool变量下
                                if (list[j].AttType == "byte")
                                {
                                    sbr.AppendFormat("            proto.{0} = (byte)ms.Read{1};\r\n", list[j].AttEnName, ChangeTypeName(list[j].AttType));
                                    sbrLua.AppendFormat("        proto.{0} = ms:Read{1};\r\n", list[j].AttEnName, ChangeTypeName(list[j].AttType));
                                }
                                else
                                {
                                    sbr.AppendFormat("            proto.{0} = ms.Read{1};\r\n", list[j].AttEnName, ChangeTypeName(list[j].AttType));
                                    sbrLua.AppendFormat("        proto.{0} = ms:Read{1};\r\n", list[j].AttEnName, ChangeTypeName(list[j].AttType));
                                }
                            }
                            sbr.Append("        }\r\n");
                        }
                    }

                    //查找隶属于这个bool失败的
                    {
                        List<MyProtoAttr> list = GetListByToBoolName(lst, item.AttEnName, false);
                        if (list != null && lst.Count > 0)
                        {
                            if (isHasSuccess)
                            {
                                sbr.Append("        else\r\n");

                                sbrLua.Append("        else\r\n");
                            }
                            else
                            {
                                //如果没有成功项
                                sbr.AppendFormat("        if (!proto.{0})\r\n", item.AttEnName);
                                sbrLua.AppendFormat("    if(not proto.{0}) then\r\n", item.AttEnName);
                            }
                            sbr.Append("        {\r\n");
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                if (list[j].AttType == "byte")
                                {
                                    //然后写到这个bool变量下
                                    sbr.AppendFormat("            proto.{0} = (byte)ms.Read{1};\r\n", list[j].AttEnName, ChangeTypeName(list[j].AttType));
                                    sbrLua.AppendFormat("        proto.{0} = ms:Read{1};\r\n", list[j].AttEnName, ChangeTypeName(list[j].AttType));
                                }
                                else
                                {
                                    sbr.AppendFormat("            proto.{0} = ms.Read{1};\r\n", list[j].AttEnName, ChangeTypeName(list[j].AttType));
                                    sbrLua.AppendFormat("        proto.{0} = ms:Read{1};\r\n", list[j].AttEnName, ChangeTypeName(list[j].AttType));
                                }
                            }
                            sbr.Append("        }\r\n");
                        }
                    }

                    sbrLua.AppendFormat("    end\r\n");
                    #endregion
                }
                else if ((item.AttType == "byte" || item.AttType == "short" || item.AttType == "int" || item.AttType == "long") && item.AttIsLoop)
                {
                    #region 循环项目

                    //======================================
                    //1.定义列表
                    {
                        List<MyProtoAttr> list = lst.Where(p => p.AttToLoop.Equals(item.AttEnName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                        if (list != null && list.Count > 0)
                        {
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;
                                sbr.AppendFormat("        proto.{0}List = new List<{1}>();\r\n", list[j].AttEnName, list[j].AttType);

                                sbrLua.AppendFormat("	proto.{0}Table = {{}};\r\n", list[j].AttEnName);
                            }
                        }
                    }
                    //======================================

                    sbr.AppendFormat("        for (int i = 0; i < proto.{0}; i++)\r\n", item.AttEnName);

                    //======================lua协议代码开始
                    sbrLua.AppendFormat("    for i = 1, proto.{0}, 1 do\r\n", item.AttEnName);
                    //======================lua协议代码结束

                    sbr.Append("        {\r\n");
                    //查找隶属于这个循环项的
                    {
                        List<MyProtoAttr> list = lst.Where(p => p.AttToLoop.Equals(item.AttEnName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                        if (list != null && list.Count > 0)
                        {
                            for (int j = 0; j < list.Count; j++)
                            {
                                //把已经提前用的了设置一下
                                lst.Where(p => p.AttID == list[j].AttID).First().AttIsUsed = true;

                                //如果是自定义属性类型
                                if (list[j].AttType != "byte" && list[j].AttType != "short" && list[j].AttType != "int" && list[j].AttType != "long" && list[j].AttType != "string" && list[j].AttType != "char" && list[j].AttType != "float" && list[j].AttType != "decimal" && list[j].AttType != "bool" && list[j].AttType != "ushort")
                                {
                                    {
                                        {
                                            List<MyProtoAttr> list2 = lst.Where(p => p.AttToCus.Equals(list[j].AttEnName, StringComparison.CurrentCultureIgnoreCase)).ToList();
                                            if (list2 != null && list2.Count > 0)
                                            {
                                                sbr.AppendFormat("            {0} _{1} = new {0}();\r\n", list[j].AttType, list[j].AttEnName);

                                                sbrLua.AppendFormat("        local _{1} = {0}.New();\r\n", list[j].AttEnName, list[j].AttEnName);

                                                for (int l = 0; l < list2.Count; l++)
                                                {
                                                    //把已经提前用的了设置一下
                                                    lst.Where(p => p.AttID == list2[l].AttID).First().AttIsUsed = true;

                                                    if (list2[l].AttType == "byte")
                                                    {
                                                        sbr.AppendFormat("            _{0}.{1} = (byte)ms.Read{2}();\r\n", list[j].AttEnName, list2[l].AttEnName, ChangeTypeName(list2[l].AttType).Replace("()", ""));
                                                        sbrLua.AppendFormat("        _{0}.{1} = ms:Read{2}();\r\n", list[j].AttEnName, list2[l].AttEnName, ChangeTypeName(list2[l].AttType).Replace("()", ""));
                                                    }
                                                    else
                                                    {
                                                        sbr.AppendFormat("            _{0}.{1} = ms.Read{2}();\r\n", list[j].AttEnName, list2[l].AttEnName, ChangeTypeName(list2[l].AttType).Replace("()", ""));
                                                        sbrLua.AppendFormat("        _{0}.{1} = ms:Read{2}();\r\n", list[j].AttEnName, list2[l].AttEnName, ChangeTypeName(list2[l].AttType).Replace("()", ""));
                                                    }
                                                }
                                                sbr.AppendFormat("            proto.{0}List.Add(_{0});\r\n", list[j].AttEnName);
                                                sbrLua.AppendFormat("        proto.{0}Table[#proto.{0}Table+1] = _{0};\r\n", list[j].AttEnName);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (list[j].AttType == "byte")
                                    {
                                        //然后写到这个bool变量下
                                        sbr.AppendFormat("            {0} _{1} = (byte)ms.Read{2}();  //{3}\r\n", list[j].AttType, list[j].AttEnName, ChangeTypeName(list[j].AttType).Replace("()", ""), list[j].AttCnName);
                                        sbrLua.AppendFormat("        local _{1} = ms:Read{2}();  --{3}\r\n", list[j].AttType, list[j].AttEnName, ChangeTypeName(list[j].AttType).Replace("()", ""), list[j].AttCnName);
                                    }
                                    else
                                    {
                                        sbr.AppendFormat("            {0} _{1} = ms.Read{2}();  //{3}\r\n", list[j].AttType, list[j].AttEnName, ChangeTypeName(list[j].AttType).Replace("()", ""), list[j].AttCnName);
                                        sbrLua.AppendFormat("        local _{1} = ms:Read{2}();  --{3}\r\n", list[j].AttType, list[j].AttEnName, ChangeTypeName(list[j].AttType).Replace("()", ""), list[j].AttCnName);
                                    }
                                    sbr.AppendFormat("            proto.{0}List.Add(_{0});\r\n", list[j].AttEnName);
                                    sbrLua.AppendFormat("        proto.{0}Table[#proto.{0}Table+1] = _{0};\r\n", list[j].AttEnName);
                                }
                            }
                        }
                    }
                    sbr.Append("        }\r\n");

                    //======================lua协议代码开始
                    sbrLua.Append("    end\r\n");
                    //======================lua协议代码结束
                    #endregion
                }

            }

            sbr.AppendFormat("\r\n");
            sbr.AppendFormat("        return proto;\r\n");
            sbr.AppendFormat("    }}\r\n");

            sbrLua.Append("\r\n");
            sbrLua.AppendFormat("    if(CS.YouYouFramework.GameEntry.Lua.DebugLogProto == true) then\r\n");
            sbrLua.AppendFormat("        print(string.format(\"<color=#00eaff>接收消息:</color><color=#00ff9c>%s %s</color>\", this.GetProtoName(), proto.ProtoCode));\r\n");
            sbrLua.AppendFormat("        print(string.format(\"<color=#c5e1dc>==>>%s</color>\", json.encode(proto)));\r\n");
            sbrLua.AppendFormat("    end\r\n");
            sbrLua.AppendFormat("    return proto;\r\n");
            sbrLua.AppendFormat("end");
            #endregion

            sbr.AppendFormat("}}");


            if (proto.IsCSharp)
            {
                using (FileStream fs = new FileStream(string.Format("{0}/{1}Proto.cs", txtCSharpProtoPath.Text, proto.ProtoEnName), FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(sbr.ToString());
                    }
                }
            }

            if (proto.IsLua)
            {
                using (FileStream fs = new FileStream(string.Format("{0}/{1}Proto.bytes", txtLuaProtoPath.Text, proto.ProtoEnName), FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(sbrLua.ToString());
                    }
                }
            }
        }

        private void CreateProtoHandler(MyProto proto)
        {
            if (!proto.ProtoCategory.Equals("MS2C") && !proto.ProtoCategory.Equals("SS2C")) return;

            StringBuilder sbr = new StringBuilder();

            sbr.Append("//===================================================\r\n");
            sbr.Append("//作    者：边涯  http://www.u3dol.com\r\n");
            sbr.Append("//创建时间：\r\n");
            sbr.Append("//备    注：\r\n");
            sbr.Append("//===================================================\r\n");
            sbr.Append("using System.Collections;\r\n");
            sbr.Append("using System.Collections.Generic;\r\n");
            sbr.Append("using UnityEngine;\r\n");

            sbr.Append("\r\n");
            sbr.Append("/// <summary>\r\n");
            sbr.AppendFormat("/// {0}（工具只生成一次）\r\n", proto.ProtoCnName);
            sbr.Append("/// </summary>\r\n");
            sbr.AppendFormat("public sealed class {0}Handler\r\n", proto.ProtoEnName);
            sbr.Append("{\r\n");
            sbr.AppendFormat("    public static void On{0}(byte[] buffer)\r\n", proto.ProtoEnName);
            sbr.Append("    {\r\n");
            sbr.AppendFormat("        {0}Proto proto = {0}Proto.GetProto(buffer);\r\n", proto.ProtoEnName);
            sbr.Append("#if DEBUG_LOG_PROTO\r\n");
            sbr.AppendFormat("        Debug.Log(\"<color=#00eaff>接收消息:</color><color=#00ff9c>\" + proto.ProtoEnName + \" \" + proto.ProtoCode + \"</color>\");\r\n");
            sbr.AppendFormat("        Debug.Log(\"<color=#c5e1dc>==>>\" + JsonUtility.ToJson(proto) + \"</color>\");\r\n");
            sbr.Append("#endif\r\n");
            sbr.Append("    }\r\n");
            sbr.Append("}");

            string csharpPath = string.Format("{0}/{1}Handler.cs", txtCSharpProtoHandlerPath.Text, proto.ProtoEnName);
            if (proto.IsCSharp && !File.Exists(csharpPath))
            {
                using (FileStream fs = new FileStream(csharpPath, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(sbr.ToString());
                    }
                }
            }

            //==========================Lua
            StringBuilder sbrLua = new StringBuilder();
            sbrLua.AppendFormat("--{0}（工具只生成一次）\r\n", proto.ProtoCnName);
            sbrLua.AppendFormat("{0}Handler = {{ }};\r\n", proto.ProtoEnName);
            sbrLua.AppendFormat("\r\n");
            sbrLua.AppendFormat("local this = {0}Handler;\r\n", proto.ProtoEnName);
            sbrLua.AppendFormat("\r\n");
            sbrLua.AppendFormat("function {0}Handler.On{0}(buffer)\r\n", proto.ProtoEnName);
            sbrLua.AppendFormat("    local proto = {0}Proto.GetProto(buffer);\r\n", proto.ProtoEnName);
            sbrLua.AppendFormat("end");

            string luaPath = string.Format("{0}/{1}Handler.bytes", txtLuaProtoHandlerPath.Text, proto.ProtoEnName);
            if (proto.IsLua && !File.Exists(luaPath))
            {
                using (FileStream fs = new FileStream(luaPath, FileMode.Create))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(sbrLua.ToString());
                    }
                }
            }
        }

        private void CreateProtoCodeDef()
        {
            StringBuilder sbr = new StringBuilder();

            sbr.AppendFormat("//===================================================\r\n");
            sbr.AppendFormat("//作    者：边涯  http://www.u3dol.com\r\n");
            sbr.AppendFormat("//创建时间：{0}\r\n", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sbr.AppendFormat("//备    注：\r\n");
            sbr.AppendFormat("//===================================================\r\n");
            sbr.AppendFormat("using System.Collections;\r\n");
            sbr.AppendFormat("\r\n");
            sbr.AppendFormat("/// <summary>\r\n");
            sbr.AppendFormat("/// 协议编号定义\r\n");
            sbr.AppendFormat("/// </summary>\r\n");
            sbr.AppendFormat("public class ProtoCodeDef\r\n");
            sbr.AppendFormat("{{\r\n");

            StringBuilder sbrLua = new StringBuilder();
            sbrLua.AppendFormat("ProtoCode =\r\n");
            sbrLua.AppendFormat("{{\r\n");

            string strLuaRequire = "";

            foreach (var menu in DataMgr.Instance.MenuList)
            {
                foreach (var proto in menu.ProtoList)
                {
                    if (proto.IsCSharp)
                    {
                        sbr.AppendFormat("    /// <summary>\r\n");
                        sbr.AppendFormat("    /// {0}\r\n", proto.ProtoCnName);
                        sbr.AppendFormat("    /// </summary>\r\n");
                        sbr.AppendFormat("    public const ushort {0} = {1};\r\n", proto.ProtoEnName, proto.ProtoCode);
                        sbr.AppendFormat("\r\n");
                    }

                    if (proto.IsLua)
                    {
                        sbrLua.AppendFormat("    {0} = {1},\r\n", proto.ProtoEnName, proto.ProtoCode);
                        strLuaRequire += string.Format("require \"DataNode/Proto/{0}Proto\";\r\n", proto.ProtoEnName);
                        if (proto.ProtoCategory.Equals("MS2C") || proto.ProtoCategory.Equals("SS2C"))
                        {
                            //如果是到客户端的
                            strLuaRequire += string.Format("require \"DataNode/ProtoHandler/{0}Handler\";\r\n", proto.ProtoEnName);
                        }
                    }
                }
            }

            sbr.AppendFormat("}}\r\n");

            sbrLua.Remove(sbrLua.Length - 3, 3);


            sbrLua.AppendFormat("\r\n}}\r\n");
            sbrLua.Append(strLuaRequire);

            using (FileStream fs = new FileStream(string.Format("{0}/ProtoCodeDef.cs", txtCSharpProtoPath.Text), FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(sbr.ToString());
                }
            }

            using (FileStream fs = new FileStream(string.Format("{0}/ProtoCodeDef.cs", txtServerProtoPath.Text), FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(sbr.ToString());
                }
            }

            using (FileStream fs = new FileStream(string.Format("{0}/ProtoCodeDef.bytes", txtLuaProtoPath.Text), FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(sbrLua.ToString());
                }
            }
        }

        private void CreateSocketProtoListener()
        {
            StringBuilder sbr = new StringBuilder();
            sbr.Append("//===================================================\r\n");
            sbr.Append("//作    者：边涯  http://www.u3dol.com\r\n");
            sbr.Append("//创建时间：\r\n");
            sbr.Append("//备    注：\r\n");
            sbr.Append("//===================================================\r\n");
            sbr.Append("using YouYouFramework;\r\n");
            sbr.Append("\r\n");
            sbr.Append("/// <summary>\r\n");
            sbr.Append("/// Socket协议监听（工具生成）\r\n");
            sbr.Append("/// </summary>\r\n");
            sbr.Append("public sealed class SocketProtoListener\r\n");
            sbr.Append("{\r\n");
            sbr.Append("    /// <summary>\r\n");
            sbr.Append("    /// 添加协议监听\r\n");
            sbr.Append("    /// </summary>\r\n");
            sbr.Append("    public static void AddProtoListener()\r\n");
            sbr.Append("    {\r\n");
            foreach (var menu in DataMgr.Instance.MenuList)
            {
                foreach (var proto in menu.ProtoList)
                {
                    if (proto.IsCSharp && (proto.ProtoCategory.Equals("MS2C") || proto.ProtoCategory.Equals("SS2C")))
                    {
                        sbr.AppendFormat("        GameEntry.Event.SocketEvent.AddEventListener(ProtoCodeDef.{0}, {0}Handler.On{0});\r\n", proto.ProtoEnName);
                    }
                }
            }
            sbr.Append("    }\r\n");
            sbr.Append("\r\n");
            sbr.Append("    /// <summary>\r\n");
            sbr.Append("    /// 移除协议监听\r\n");
            sbr.Append("    /// </summary>\r\n");
            sbr.Append("    public static void RemoveProtoListener()\r\n");
            sbr.Append("    {\r\n");
            foreach (var menu in DataMgr.Instance.MenuList)
            {
                foreach (var proto in menu.ProtoList)
                {
                    if (proto.IsCSharp && (proto.ProtoCategory.Equals("MS2C") || proto.ProtoCategory.Equals("SS2C")))
                    {
                        sbr.AppendFormat("        GameEntry.Event.SocketEvent.RemoveEventListener(ProtoCodeDef.{0}, {0}Handler.On{0});\r\n", proto.ProtoEnName);
                    }
                }
            }
            sbr.Append("    }\r\n");
            sbr.Append("}");

            using (FileStream fs = new FileStream(string.Format("{0}/SocketProtoListener.cs", txtCSharpProtoHandlerPath.Text), FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(sbr.ToString());
                }
            }

            //==============================Lua
            StringBuilder sbrLua = new StringBuilder();
            sbrLua.AppendFormat("--Socket协议监听（工具生成）\r\n");
            sbrLua.AppendFormat("\r\n");
            sbrLua.AppendFormat("SocketProtoListenerForLua = {{ }};\r\n");
            sbrLua.AppendFormat("\r\n");
            sbrLua.AppendFormat("local this = SocketProtoListenerForLua;\r\n");
            sbrLua.AppendFormat("\r\n");
            sbrLua.AppendFormat("function SocketProtoListenerForLua.AddProtoListener()\r\n");
            foreach (var menu in DataMgr.Instance.MenuList)
            {
                foreach (var proto in menu.ProtoList)
                {
                    if (proto.IsLua && (proto.ProtoCategory.Equals("MS2C") || proto.ProtoCategory.Equals("SS2C")))
                    {
                        sbrLua.AppendFormat("    CS.YouYouFramework.GameEntry.Event.SocketEvent:AddEventListener(ProtoCode.{0}, {0}Handler.On{0});\r\n", proto.ProtoEnName);
                    }
                }
            }

            sbrLua.AppendFormat("end");
            using (FileStream fs = new FileStream(string.Format("{0}/SocketProtoListenerForLua.bytes", txtLuaProtoHandlerPath.Text), FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(sbrLua.ToString());
                }
            }
        }

        private void btnCreateAll_Click(object sender, EventArgs e)
        {
            DataMgr.Instance.LoadXml();

            CreateProtoCodeDef();
            CreateSocketProtoListener();

            foreach (var menu in DataMgr.Instance.MenuList)
            {
                foreach (var proto in menu.ProtoList)
                {
                    CreateProto(proto);
                    CreateServerProto(proto);
                    CreateProtoHandler(proto);
                }
            }

            MessageBox.Show("代码生成完毕");
        }

        private void frmMSG_Load(object sender, EventArgs e)
        {
            List<KeyValuePair<string, string>> lst = new List<KeyValuePair<string, string>>();
            lst.Add(new KeyValuePair<string, string>("C2MS", "客户端->主服务器"));
            lst.Add(new KeyValuePair<string, string>("MS2C", "主服务器->客户端"));
            lst.Add(new KeyValuePair<string, string>("C2SS", "客户端->同步服务器"));
            lst.Add(new KeyValuePair<string, string>("SS2C", "同步服务器->客户端"));
            protoCategory.DataSource = lst;
            protoCategory.ValueMember = "key";
            protoCategory.DisplayMember = "value";

            //======加载路径设置======
            LoadConfig();
        }


        private void LoadConfig()
        {
            string configPath = Application.StartupPath + "/config.txt";

            if (File.Exists(configPath))
            {
                string str = "";
                using (FileStream fs = new FileStream(configPath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(fs))
                    {
                        str = sr.ReadToEnd();
                    }
                }

                if (!string.IsNullOrEmpty(str))
                {
                    string[] arr = str.Split('\n');
                    if (arr.Length >= 5)
                    {
                        txtCSharpProtoPath.Text = arr[0];
                        txtCSharpProtoHandlerPath.Text = arr[1];
                        txtLuaProtoPath.Text = arr[2];
                        txtLuaProtoHandlerPath.Text = arr[3];
                        txtServerProtoPath.Text = arr[4];
                    }
                }
            }
        }

        private void btnSavePath_Click(object sender, EventArgs e)
        {
            string configPath = Application.StartupPath + "/config.txt";
            if (File.Exists(configPath))
            {
                //如果配置文件存在 先删除
                File.Delete(configPath);
            }

            //创建文本
            StringBuilder sbr = new StringBuilder();
            sbr.Append(txtCSharpProtoPath.Text);
            sbr.Append("\n");
            sbr.Append(txtCSharpProtoHandlerPath.Text);
            sbr.Append("\n");
            sbr.Append(txtLuaProtoPath.Text);
            sbr.Append("\n");
            sbr.Append(txtLuaProtoHandlerPath.Text);
            sbr.Append("\n");
            sbr.Append(txtServerProtoPath.Text);

            using (FileStream fs = new FileStream(configPath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(sbr.ToString());
                }
            }
        }
    }
}