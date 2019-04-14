namespace ReadExcel
{
    partial class frmMSG
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnCreateAll = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnDelMenu = new System.Windows.Forms.Button();
            this.btnAddProto = new System.Windows.Forms.Button();
            this.btnAddMenu = new System.Windows.Forms.Button();
            this.myTree = new System.Windows.Forms.TreeView();
            this.btnSaveProto = new System.Windows.Forms.Button();
            this.groupProtoInfo = new System.Windows.Forms.GroupBox();
            this.checkBoxLua = new System.Windows.Forms.CheckBox();
            this.checkBoxCSharp = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.protoCategory = new System.Windows.Forms.ComboBox();
            this.btnCreateCode = new System.Windows.Forms.Button();
            this.btnMoveNextAtt = new System.Windows.Forms.Button();
            this.btnMovePrevAtt = new System.Windows.Forms.Button();
            this.btnDelAtt = new System.Windows.Forms.Button();
            this.btnAttpreview = new System.Windows.Forms.Button();
            this.txtAttpreview = new System.Windows.Forms.TextBox();
            this.btnDelProto = new System.Windows.Forms.Button();
            this.txtProtoDesc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtProtoCode = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtProtoCnName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProtoEnName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dvGrid = new System.Windows.Forms.DataGridView();
            this.AttType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttEnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttCnName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttIsLoop = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AttToLoop = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttToBool = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.AttToBoolResult = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AttToCus = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.txtCSharpProtoPath = new System.Windows.Forms.TextBox();
            this.txtCSharpProtoHandlerPath = new System.Windows.Forms.TextBox();
            this.txtLuaProtoPath = new System.Windows.Forms.TextBox();
            this.txtLuaProtoHandlerPath = new System.Windows.Forms.TextBox();
            this.btnSavePath = new System.Windows.Forms.Button();
            this.txtServerProtoPath = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupProtoInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnCreateAll);
            this.groupBox1.Controls.Add(this.btnNext);
            this.groupBox1.Controls.Add(this.btnPrev);
            this.groupBox1.Controls.Add(this.btnDelMenu);
            this.groupBox1.Controls.Add(this.btnAddProto);
            this.groupBox1.Controls.Add(this.btnAddMenu);
            this.groupBox1.Controls.Add(this.myTree);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(237, 762);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "协议树";
            // 
            // btnCreateAll
            // 
            this.btnCreateAll.Location = new System.Drawing.Point(3, 683);
            this.btnCreateAll.Name = "btnCreateAll";
            this.btnCreateAll.Size = new System.Drawing.Size(228, 73);
            this.btnCreateAll.TabIndex = 15;
            this.btnCreateAll.Text = "生成全部协议";
            this.btnCreateAll.UseVisualStyleBackColor = true;
            this.btnCreateAll.Click += new System.EventHandler(this.btnCreateAll_Click);
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(156, 654);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(75, 23);
            this.btnNext.TabIndex = 14;
            this.btnNext.Text = "下移";
            this.btnNext.UseVisualStyleBackColor = true;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(156, 625);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(75, 23);
            this.btnPrev.TabIndex = 13;
            this.btnPrev.Text = "上移";
            this.btnPrev.UseVisualStyleBackColor = true;
            this.btnPrev.Click += new System.EventHandler(this.btnPrev_Click);
            // 
            // btnDelMenu
            // 
            this.btnDelMenu.Location = new System.Drawing.Point(87, 532);
            this.btnDelMenu.Name = "btnDelMenu";
            this.btnDelMenu.Size = new System.Drawing.Size(144, 23);
            this.btnDelMenu.TabIndex = 12;
            this.btnDelMenu.Text = "删除选定模块";
            this.btnDelMenu.UseVisualStyleBackColor = true;
            this.btnDelMenu.Click += new System.EventHandler(this.btnDelMenu_Click);
            // 
            // btnAddProto
            // 
            this.btnAddProto.Location = new System.Drawing.Point(0, 654);
            this.btnAddProto.Name = "btnAddProto";
            this.btnAddProto.Size = new System.Drawing.Size(75, 23);
            this.btnAddProto.TabIndex = 11;
            this.btnAddProto.Text = "添加协议";
            this.btnAddProto.UseVisualStyleBackColor = true;
            this.btnAddProto.Click += new System.EventHandler(this.btnAddProto_Click);
            // 
            // btnAddMenu
            // 
            this.btnAddMenu.Location = new System.Drawing.Point(6, 532);
            this.btnAddMenu.Name = "btnAddMenu";
            this.btnAddMenu.Size = new System.Drawing.Size(75, 23);
            this.btnAddMenu.TabIndex = 9;
            this.btnAddMenu.Text = "添加模块";
            this.btnAddMenu.UseVisualStyleBackColor = true;
            this.btnAddMenu.Click += new System.EventHandler(this.btnAddMenu_Click);
            // 
            // myTree
            // 
            this.myTree.Location = new System.Drawing.Point(3, 17);
            this.myTree.Name = "myTree";
            this.myTree.Size = new System.Drawing.Size(228, 509);
            this.myTree.TabIndex = 0;
            this.myTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.myTree_NodeMouseClick);
            this.myTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.myTree_NodeMouseDoubleClick);
            // 
            // btnSaveProto
            // 
            this.btnSaveProto.Location = new System.Drawing.Point(802, 15);
            this.btnSaveProto.Name = "btnSaveProto";
            this.btnSaveProto.Size = new System.Drawing.Size(75, 23);
            this.btnSaveProto.TabIndex = 10;
            this.btnSaveProto.Text = "保存协议";
            this.btnSaveProto.UseVisualStyleBackColor = true;
            this.btnSaveProto.Click += new System.EventHandler(this.btnSaveProto_Click);
            // 
            // groupProtoInfo
            // 
            this.groupProtoInfo.Controls.Add(this.checkBoxLua);
            this.groupProtoInfo.Controls.Add(this.checkBoxCSharp);
            this.groupProtoInfo.Controls.Add(this.label6);
            this.groupProtoInfo.Controls.Add(this.label5);
            this.groupProtoInfo.Controls.Add(this.protoCategory);
            this.groupProtoInfo.Controls.Add(this.btnCreateCode);
            this.groupProtoInfo.Controls.Add(this.btnMoveNextAtt);
            this.groupProtoInfo.Controls.Add(this.btnMovePrevAtt);
            this.groupProtoInfo.Controls.Add(this.btnDelAtt);
            this.groupProtoInfo.Controls.Add(this.btnAttpreview);
            this.groupProtoInfo.Controls.Add(this.txtAttpreview);
            this.groupProtoInfo.Controls.Add(this.btnDelProto);
            this.groupProtoInfo.Controls.Add(this.txtProtoDesc);
            this.groupProtoInfo.Controls.Add(this.label4);
            this.groupProtoInfo.Controls.Add(this.btnSaveProto);
            this.groupProtoInfo.Controls.Add(this.txtProtoCode);
            this.groupProtoInfo.Controls.Add(this.label3);
            this.groupProtoInfo.Controls.Add(this.txtProtoCnName);
            this.groupProtoInfo.Controls.Add(this.label2);
            this.groupProtoInfo.Controls.Add(this.txtProtoEnName);
            this.groupProtoInfo.Controls.Add(this.label1);
            this.groupProtoInfo.Controls.Add(this.dvGrid);
            this.groupProtoInfo.Location = new System.Drawing.Point(255, 12);
            this.groupProtoInfo.Name = "groupProtoInfo";
            this.groupProtoInfo.Size = new System.Drawing.Size(964, 648);
            this.groupProtoInfo.TabIndex = 1;
            this.groupProtoInfo.TabStop = false;
            this.groupProtoInfo.Text = "协议结构";
            // 
            // checkBoxLua
            // 
            this.checkBoxLua.AutoSize = true;
            this.checkBoxLua.Location = new System.Drawing.Point(526, 47);
            this.checkBoxLua.Name = "checkBoxLua";
            this.checkBoxLua.Size = new System.Drawing.Size(42, 16);
            this.checkBoxLua.TabIndex = 24;
            this.checkBoxLua.Text = "Lua";
            this.checkBoxLua.UseVisualStyleBackColor = true;
            // 
            // checkBoxCSharp
            // 
            this.checkBoxCSharp.AutoSize = true;
            this.checkBoxCSharp.Location = new System.Drawing.Point(484, 47);
            this.checkBoxCSharp.Name = "checkBoxCSharp";
            this.checkBoxCSharp.Size = new System.Drawing.Size(36, 16);
            this.checkBoxCSharp.TabIndex = 23;
            this.checkBoxCSharp.Text = "c#";
            this.checkBoxCSharp.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(425, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 22;
            this.label6.Text = "协议所属";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(425, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "协议分类";
            // 
            // protoCategory
            // 
            this.protoCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.protoCategory.FormattingEnabled = true;
            this.protoCategory.Location = new System.Drawing.Point(484, 17);
            this.protoCategory.Name = "protoCategory";
            this.protoCategory.Size = new System.Drawing.Size(174, 20);
            this.protoCategory.TabIndex = 20;
            // 
            // btnCreateCode
            // 
            this.btnCreateCode.Location = new System.Drawing.Point(440, 572);
            this.btnCreateCode.Name = "btnCreateCode";
            this.btnCreateCode.Size = new System.Drawing.Size(75, 23);
            this.btnCreateCode.TabIndex = 19;
            this.btnCreateCode.Text = "生成协议";
            this.btnCreateCode.UseVisualStyleBackColor = true;
            this.btnCreateCode.Click += new System.EventHandler(this.btnCreateCode_Click);
            // 
            // btnMoveNextAtt
            // 
            this.btnMoveNextAtt.Location = new System.Drawing.Point(266, 572);
            this.btnMoveNextAtt.Name = "btnMoveNextAtt";
            this.btnMoveNextAtt.Size = new System.Drawing.Size(75, 23);
            this.btnMoveNextAtt.TabIndex = 18;
            this.btnMoveNextAtt.Text = "下移字段";
            this.btnMoveNextAtt.UseVisualStyleBackColor = true;
            this.btnMoveNextAtt.Click += new System.EventHandler(this.btnMoveNextAtt_Click);
            // 
            // btnMovePrevAtt
            // 
            this.btnMovePrevAtt.Location = new System.Drawing.Point(266, 543);
            this.btnMovePrevAtt.Name = "btnMovePrevAtt";
            this.btnMovePrevAtt.Size = new System.Drawing.Size(75, 23);
            this.btnMovePrevAtt.TabIndex = 17;
            this.btnMovePrevAtt.Text = "上移字段";
            this.btnMovePrevAtt.UseVisualStyleBackColor = true;
            this.btnMovePrevAtt.Click += new System.EventHandler(this.btnMovePrevAtt_Click);
            // 
            // btnDelAtt
            // 
            this.btnDelAtt.Location = new System.Drawing.Point(347, 543);
            this.btnDelAtt.Name = "btnDelAtt";
            this.btnDelAtt.Size = new System.Drawing.Size(75, 23);
            this.btnDelAtt.TabIndex = 16;
            this.btnDelAtt.Text = "删除字段";
            this.btnDelAtt.UseVisualStyleBackColor = true;
            this.btnDelAtt.Click += new System.EventHandler(this.btnDelAtt_Click);
            // 
            // btnAttpreview
            // 
            this.btnAttpreview.Location = new System.Drawing.Point(440, 543);
            this.btnAttpreview.Name = "btnAttpreview";
            this.btnAttpreview.Size = new System.Drawing.Size(75, 23);
            this.btnAttpreview.TabIndex = 15;
            this.btnAttpreview.Text = "预览";
            this.btnAttpreview.UseVisualStyleBackColor = true;
            this.btnAttpreview.Click += new System.EventHandler(this.btnAttpreview_Click);
            // 
            // txtAttpreview
            // 
            this.txtAttpreview.Location = new System.Drawing.Point(521, 543);
            this.txtAttpreview.Multiline = true;
            this.txtAttpreview.Name = "txtAttpreview";
            this.txtAttpreview.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAttpreview.Size = new System.Drawing.Size(437, 88);
            this.txtAttpreview.TabIndex = 13;
            // 
            // btnDelProto
            // 
            this.btnDelProto.Location = new System.Drawing.Point(883, 15);
            this.btnDelProto.Name = "btnDelProto";
            this.btnDelProto.Size = new System.Drawing.Size(75, 23);
            this.btnDelProto.TabIndex = 11;
            this.btnDelProto.Text = "删除协议";
            this.btnDelProto.UseVisualStyleBackColor = true;
            this.btnDelProto.Click += new System.EventHandler(this.btnDelProto_Click);
            // 
            // txtProtoDesc
            // 
            this.txtProtoDesc.Location = new System.Drawing.Point(95, 98);
            this.txtProtoDesc.Multiline = true;
            this.txtProtoDesc.Name = "txtProtoDesc";
            this.txtProtoDesc.Size = new System.Drawing.Size(635, 73);
            this.txtProtoDesc.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 101);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 7;
            this.label4.Text = "协议描述";
            // 
            // txtProtoCode
            // 
            this.txtProtoCode.Location = new System.Drawing.Point(95, 17);
            this.txtProtoCode.Name = "txtProtoCode";
            this.txtProtoCode.Size = new System.Drawing.Size(307, 21);
            this.txtProtoCode.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(36, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "协议编号";
            // 
            // txtProtoCnName
            // 
            this.txtProtoCnName.Location = new System.Drawing.Point(95, 71);
            this.txtProtoCnName.Name = "txtProtoCnName";
            this.txtProtoCnName.Size = new System.Drawing.Size(307, 21);
            this.txtProtoCnName.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 74);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 12);
            this.label2.TabIndex = 3;
            this.label2.Text = "协议中文名称";
            // 
            // txtProtoEnName
            // 
            this.txtProtoEnName.Location = new System.Drawing.Point(95, 44);
            this.txtProtoEnName.Name = "txtProtoEnName";
            this.txtProtoEnName.Size = new System.Drawing.Size(307, 21);
            this.txtProtoEnName.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "协议英文名称";
            // 
            // dvGrid
            // 
            this.dvGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.AttType,
            this.AttEnName,
            this.AttCnName,
            this.AttIsLoop,
            this.AttToLoop,
            this.AttToBool,
            this.AttToBoolResult,
            this.AttToCus});
            this.dvGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dvGrid.Location = new System.Drawing.Point(6, 177);
            this.dvGrid.Name = "dvGrid";
            this.dvGrid.RowTemplate.Height = 23;
            this.dvGrid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvGrid.Size = new System.Drawing.Size(952, 349);
            this.dvGrid.TabIndex = 0;
            // 
            // AttType
            // 
            this.AttType.HeaderText = "字段类型";
            this.AttType.Name = "AttType";
            // 
            // AttEnName
            // 
            this.AttEnName.HeaderText = "字段名称";
            this.AttEnName.Name = "AttEnName";
            // 
            // AttCnName
            // 
            this.AttCnName.HeaderText = "字段描述";
            this.AttCnName.Name = "AttCnName";
            this.AttCnName.Width = 150;
            // 
            // AttIsLoop
            // 
            this.AttIsLoop.HeaderText = "循环此项";
            this.AttIsLoop.Name = "AttIsLoop";
            this.AttIsLoop.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.AttIsLoop.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.AttIsLoop.ToolTipText = "仅限于int类型";
            // 
            // AttToLoop
            // 
            this.AttToLoop.HeaderText = "隶属循环项";
            this.AttToLoop.Name = "AttToLoop";
            this.AttToLoop.ToolTipText = "隶属于某个字段循环";
            // 
            // AttToBool
            // 
            this.AttToBool.HeaderText = "隶属于布尔";
            this.AttToBool.Name = "AttToBool";
            // 
            // AttToBoolResult
            // 
            this.AttToBoolResult.HeaderText = "布尔结果";
            this.AttToBoolResult.Name = "AttToBoolResult";
            // 
            // AttToCus
            // 
            this.AttToCus.HeaderText = "隶属于自定义";
            this.AttToCus.Name = "AttToCus";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(301, 671);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(89, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "CSharp协议路径";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(688, 671);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(131, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "CSharp协议Handler路径";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(319, 696);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(71, 12);
            this.label9.TabIndex = 4;
            this.label9.Text = "Lua协议路径";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(706, 699);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "Lua协议Handler路径";
            // 
            // txtCSharpProtoPath
            // 
            this.txtCSharpProtoPath.Location = new System.Drawing.Point(396, 666);
            this.txtCSharpProtoPath.Name = "txtCSharpProtoPath";
            this.txtCSharpProtoPath.Size = new System.Drawing.Size(280, 21);
            this.txtCSharpProtoPath.TabIndex = 6;
            // 
            // txtCSharpProtoHandlerPath
            // 
            this.txtCSharpProtoHandlerPath.Location = new System.Drawing.Point(825, 666);
            this.txtCSharpProtoHandlerPath.Name = "txtCSharpProtoHandlerPath";
            this.txtCSharpProtoHandlerPath.Size = new System.Drawing.Size(280, 21);
            this.txtCSharpProtoHandlerPath.TabIndex = 7;
            // 
            // txtLuaProtoPath
            // 
            this.txtLuaProtoPath.Location = new System.Drawing.Point(395, 695);
            this.txtLuaProtoPath.Name = "txtLuaProtoPath";
            this.txtLuaProtoPath.Size = new System.Drawing.Size(281, 21);
            this.txtLuaProtoPath.TabIndex = 8;
            // 
            // txtLuaProtoHandlerPath
            // 
            this.txtLuaProtoHandlerPath.Location = new System.Drawing.Point(825, 696);
            this.txtLuaProtoHandlerPath.Name = "txtLuaProtoHandlerPath";
            this.txtLuaProtoHandlerPath.Size = new System.Drawing.Size(280, 21);
            this.txtLuaProtoHandlerPath.TabIndex = 9;
            // 
            // btnSavePath
            // 
            this.btnSavePath.Location = new System.Drawing.Point(1144, 671);
            this.btnSavePath.Name = "btnSavePath";
            this.btnSavePath.Size = new System.Drawing.Size(75, 97);
            this.btnSavePath.TabIndex = 25;
            this.btnSavePath.Text = "保存路径";
            this.btnSavePath.UseVisualStyleBackColor = true;
            this.btnSavePath.Click += new System.EventHandler(this.btnSavePath_Click);
            // 
            // txtServerProtoPath
            // 
            this.txtServerProtoPath.Location = new System.Drawing.Point(396, 722);
            this.txtServerProtoPath.Name = "txtServerProtoPath";
            this.txtServerProtoPath.Size = new System.Drawing.Size(281, 21);
            this.txtServerProtoPath.TabIndex = 26;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(301, 725);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(89, 12);
            this.label11.TabIndex = 27;
            this.label11.Text = "服务器协议路径";
            // 
            // frmMSG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1231, 786);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.txtServerProtoPath);
            this.Controls.Add(this.btnSavePath);
            this.Controls.Add(this.txtLuaProtoHandlerPath);
            this.Controls.Add(this.txtLuaProtoPath);
            this.Controls.Add(this.txtCSharpProtoHandlerPath);
            this.Controls.Add(this.txtCSharpProtoPath);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.groupProtoInfo);
            this.Controls.Add(this.groupBox1);
            this.Name = "frmMSG";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MMO 协议管理器-北京边涯-悠游课堂";
            this.Load += new System.EventHandler(this.frmMSG_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupProtoInfo.ResumeLayout(false);
            this.groupProtoInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvGrid)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupProtoInfo;
        private System.Windows.Forms.TreeView myTree;
        private System.Windows.Forms.DataGridView dvGrid;
        private System.Windows.Forms.TextBox txtProtoCode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtProtoCnName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProtoEnName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtProtoDesc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSaveProto;
        private System.Windows.Forms.Button btnAddMenu;
        private System.Windows.Forms.Button btnAddProto;
        private System.Windows.Forms.Button btnDelMenu;
        private System.Windows.Forms.Button btnDelProto;
        private System.Windows.Forms.Button btnPrev;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Button btnAttpreview;
        private System.Windows.Forms.TextBox txtAttpreview;
        private System.Windows.Forms.Button btnDelAtt;
        private System.Windows.Forms.Button btnMoveNextAtt;
        private System.Windows.Forms.Button btnMovePrevAtt;
        private System.Windows.Forms.DataGridViewTextBoxColumn AttType;
        private System.Windows.Forms.DataGridViewTextBoxColumn AttEnName;
        private System.Windows.Forms.DataGridViewTextBoxColumn AttCnName;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AttIsLoop;
        private System.Windows.Forms.DataGridViewTextBoxColumn AttToLoop;
        private System.Windows.Forms.DataGridViewTextBoxColumn AttToBool;
        private System.Windows.Forms.DataGridViewCheckBoxColumn AttToBoolResult;
        private System.Windows.Forms.DataGridViewTextBoxColumn AttToCus;
        private System.Windows.Forms.Button btnCreateCode;
        private System.Windows.Forms.Button btnCreateAll;
        private System.Windows.Forms.CheckBox checkBoxLua;
        private System.Windows.Forms.CheckBox checkBoxCSharp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox protoCategory;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtCSharpProtoPath;
        private System.Windows.Forms.TextBox txtCSharpProtoHandlerPath;
        private System.Windows.Forms.TextBox txtLuaProtoPath;
        private System.Windows.Forms.TextBox txtLuaProtoHandlerPath;
        private System.Windows.Forms.Button btnSavePath;
        private System.Windows.Forms.TextBox txtServerProtoPath;
        private System.Windows.Forms.Label label11;
    }
}