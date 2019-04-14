using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadExcel
{
    public partial class frmModelEdit : Form
    {
        public frmModelEdit()
        {
            InitializeComponent();
        }

        public string ModelName
        {
            get
            {
                return this.txtModelName.Text;
            }
            set
            {
                this.txtModelName.Text = value;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtModelName.Text))
            {
                MessageBox.Show("请输入模块名称");
                this.txtModelName.Focus();
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
