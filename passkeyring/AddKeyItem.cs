using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace passkeyring
{
    public partial class AddKeyItem : Form
    {
        public enum Mode
        {
            add,
            edit
        }
        public string KeyName 
        {
            get
            {
                return txtName.Text;
            }
            set
            {
                txtName.Text = value;
            }
        }
        public string Desc 
        {
            get
            {
                return txtDesc.Text;
            }
            set
            {
                txtDesc.Text = value.Replace("\n", System.Environment.NewLine);
            }
        }
        public string Username 
        {
            get
            {
                return txtUsername.Text;
            }
            set
            {
                txtUsername.Text = value;
            }
        }
        public string Password 
        {
            get
            {
                return txtPassword.Text;
            }
            set
            {
                txtPassword.Text = value;
            }
        }

        private Mode _mode;

        public AddKeyItem(Mode  mode)
        {
            _mode = mode;
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void AddKeyItem_Load(object sender, EventArgs e)
        {
            if (_mode == Mode.add)
            {
                this.Text = "Add Key Ring Item";
                txtName.Enabled = true;
            }
            else
            {
                this.Text = "Edit Key Ring Item";
                txtName.Enabled = false;
            }
        }

    }
}
