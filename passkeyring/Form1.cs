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
    public partial class Form1 : Form
    {
        private const int NAME_CELL_INDEX = 0;
        private const int DESC_CELL_INDEX = 3;
        private const int USER_CELL_INDEX = 1;
        private const int PASS_CELL_INDEX = 2;

        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            AddNewItem();
        }

        private void AddNewItem()
        {
            AddKeyItem akiForm = new AddKeyItem(AddKeyItem.Mode.add);
            if (akiForm.ShowDialog() == DialogResult.OK)
            {
                KeyRingController keyringController = new KeyRingController();
                KeyRingItem kri = new KeyRingItem();

                kri.Name = akiForm.KeyName;
                kri.Desc = akiForm.Desc;
                kri.Username = akiForm.Username;
                kri.Password = akiForm.Password;
                keyringController.AddToKeyRing(kri);

                LoadData();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            LoadData();
            SetUpDGProperties();
        }

        private void SetUpDGProperties()
        {
            dgKeyRing.Columns[NAME_CELL_INDEX].Width = 150;
            dgKeyRing.Columns[DESC_CELL_INDEX].Width = 400;
            dgKeyRing.Columns[DESC_CELL_INDEX].HeaderText = "Description";
            dgKeyRing.Columns[USER_CELL_INDEX].Width = 150;
            dgKeyRing.Columns[PASS_CELL_INDEX].Width = 150;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EntryPortal epForm = new EntryPortal();
            if (epForm.ShowDialog() != DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void LoadData()
        {
            KeyRingController krc = new KeyRingController();
            dgKeyRing.DataSource = krc.GetKeyRing();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddNewItem();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        private void EditItem()
        {
            if (dgKeyRing.SelectedRows.Count == 1)
            {
                AddKeyItem akiForm = new AddKeyItem(AddKeyItem.Mode.edit);
                //add data to fields here
                akiForm.KeyName = (string)dgKeyRing.SelectedRows[0].Cells[NAME_CELL_INDEX].Value;
                akiForm.Desc = (string)dgKeyRing.SelectedRows[0].Cells[DESC_CELL_INDEX].Value;
                akiForm.Username = (string)dgKeyRing.SelectedRows[0].Cells[USER_CELL_INDEX].Value;
                akiForm.Password = (string)dgKeyRing.SelectedRows[0].Cells[PASS_CELL_INDEX].Value;

                if (akiForm.ShowDialog() == DialogResult.OK)
                {
                    KeyRingController keyringController = new KeyRingController();
                    KeyRingItem kri = new KeyRingItem();
        
                    kri.Name = akiForm.KeyName;
                    kri.Desc = akiForm.Desc;
                    kri.Username = akiForm.Username;
                    kri.Password = akiForm.Password;
                    keyringController.EditKeyRingItem(kri);

                    int i = dgKeyRing.SelectedRows[0].Index;
                    LoadData();
                    dgKeyRing.Rows[i].Selected = true;
                }
            }
        }

        private void editPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void DeleteItem()
        {
            if (dgKeyRing.SelectedRows.Count == 1)
            {
                if (MessageBox.Show("Are you sure you want to delete item: " + dgKeyRing.SelectedRows[0].Cells[NAME_CELL_INDEX].Value + "?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) 
                {
                    KeyRingController keyringController = new KeyRingController();
                    keyringController.DeleteKeyRingItem((string)dgKeyRing.SelectedRows[0].Cells[NAME_CELL_INDEX].Value);

                    LoadData();
                }
            }
        }

        private void deletePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void copyUsernameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgKeyRing.SelectedRows.Count == 1)
            {
                Clipboard.SetText((string)dgKeyRing.SelectedRows[0].Cells[USER_CELL_INDEX].Value, TextDataFormat.Text);
            }
        }

        private void copToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgKeyRing.SelectedRows.Count == 1)
            {
                Clipboard.SetText((string)dgKeyRing.SelectedRows[0].Cells[PASS_CELL_INDEX].Value, TextDataFormat.Text);
            }
        }

        private void editKeyRingItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditItem();
        }

        private void deleteKeyRingItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeleteItem();
        }

        private void dgKeyRing_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dgKeyRing.Rows[e.RowIndex].Selected = true;
            }
        }
    }
}
