using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientReservasi_076
{
    public partial class Register : Form
    {
        ServiceReference1.Service1Client service = new ServiceReference1.Service1Client();
        public Register()
        {
            InitializeComponent();
            TampilData();
            textboxID.Visible = false;
            btUpdate.Enabled = false;
            btHapus.Enabled = false;
        }


        public void TampilData()
        {
            var List = service.DataRegist();
            dtRegister.DataSource = List;
        }

        public void Clear()
        {
            textboxUsername.Clear();
            textboxPassword.Clear();
            comboBoxKategori.SelectedItem = null;

            btSimpan.Enabled = true;
            btUpdate.Enabled = false;
            btHapus.Enabled = false;
        }

        private void dtRegister_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textboxID.Text = Convert.ToString(dtRegister.Rows[e.RowIndex].Cells[0].Value);
            textboxUsername.Text = Convert.ToString(dtRegister.Rows[e.RowIndex].Cells[1].Value);
            textboxPassword.Text = Convert.ToString(dtRegister.Rows[e.RowIndex].Cells[2].Value);
            comboBoxKategori.Text = Convert.ToString(dtRegister.Rows[e.RowIndex].Cells[3].Value);


            btSimpan.Enabled = false;
            btUpdate.Enabled = true;
            btHapus.Enabled = true;
        }

        private void btUpdate_Click_1(object sender, EventArgs e)
        {
            string username = textboxUsername.Text;
            string password = textboxPassword.Text;
            string kategori = comboBoxKategori.Text;
            int id = Convert.ToInt32(textboxID.Text);
            string a = service.UpdateRegister(username, password, kategori, id);

            if (textboxUsername.Text == "" || textboxPassword.Text == "" || comboBoxKategori.Text == "")
            {
                MessageBox.Show("Semua data wajib diisi.");
            }
            else
            {
                MessageBox.Show(a);
                Refresh();
                TampilData();
            }
        }

        private void btClear_Click_1(object sender, EventArgs e)
        {
            Clear();
        }

        private void btSimpan_Click_1(object sender, EventArgs e)
        {
            string username = textboxUsername.Text;
            string password = textboxPassword.Text;
            string kategori = comboBoxKategori.Text;
            string a = service.Register(username, password, kategori);

            if (textboxUsername.Text == "" || textboxPassword.Text == "" || comboBoxKategori.Text == "")
            {
                MessageBox.Show("Semua data wajib diisi.");
            }
            else
            {
                MessageBox.Show(a);
                Refresh();
                TampilData();
            }
        }

        private void btHapus_Click_1(object sender, EventArgs e)
        {
            string username = textboxUsername.Text;

            DialogResult dialogResult = MessageBox.Show("Apakah anda yakin ingin menghapus data ini?", "Hapus Data", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string a = service.DeleteRegister(username);
                MessageBox.Show(a);
                Clear();
                TampilData();
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }
    }
}
