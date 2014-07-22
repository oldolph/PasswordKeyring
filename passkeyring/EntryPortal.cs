using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace passkeyring
{
    public partial class EntryPortal : Form
    {
        public EntryPortal()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //TODO: finish user selected pass, but for now, global pass
            // self ref: store MD5 salted hash on client machine
            string tmpPass = "l3tm31n";

            if (tmpPass == txtPassword.Text.ToLower())
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                lblMsg.Visible = true;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void EntryPortal_Load(object sender, EventArgs e)
        {
            lblMsg.Visible = false;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnOK_Click(null, null);
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShowPassword.Checked)
            {
                txtPassword.PasswordChar = '\0';
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }
    }
}
