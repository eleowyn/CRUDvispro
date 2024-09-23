using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace logindatabase
{
    public partial class Form2 : Form
    {
        private MySqlConnection koneksi;
        private MySqlDataAdapter adapter;
        private MySqlCommand perintah;
        private DataSet ds = new DataSet();
        private string alamat, query;

        public Form2()
        {
            alamat = "server=localhost; database=db_mahasiswa; username=root; password=;";
            koneksi = new MySqlConnection(alamat);
            InitializeComponent();
        }

        private void labelberhasil_Click(object sender, EventArgs e)
        {


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            koneksi.Open();
            query = string.Format("select * from tbl_pengguna");
            perintah = new MySqlCommand(query, koneksi);
            adapter = new MySqlDataAdapter(perintah);
            perintah.ExecuteNonQuery();
            ds.Clear();
            adapter.Fill(ds);
            koneksi.Close();
            dataGridView1.DataSource = ds.Tables[0];
            dataGridView1.Columns[0].Width = 100;
            dataGridView1.Columns[0].HeaderText = "ID Pengguna";
            dataGridView1.Columns[1].Width = 150;
            dataGridView1.Columns[1].HeaderText = "Username";
            dataGridView1.Columns[2].Width = 120;
            dataGridView1.Columns[2].HeaderText = "Password";
            dataGridView1.Columns[3].Width = 120;
            dataGridView1.Columns[3].HeaderText = "Nama Pengguna";
            dataGridView1.Columns[4].Width = 120;
            dataGridView1.Columns[4].HeaderText = "Level";

            txtID.Clear();
            txtNamaPengguna.Clear();
            txtPassword.Clear();
            txtUsername.Clear();
            txtID.Focus();
            buttonUpdate.Enabled = false;
            buttonDelete.Enabled = false;
            buttonClear.Enabled = false;
            buttonSave.Enabled = true;
            buttonSearch.Enabled = true;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text != "" && txtPassword.Text != "" && txtNamaPengguna.Text != "")
                {

                    query = string.Format("insert into tbl_pengguna  values ('{0}','{1}','{2}','{3}','{4}');", txtID.Text, txtUsername.Text, txtPassword.Text, txtNamaPengguna.Text, comboBoxLevel.Text);


                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    int res = perintah.ExecuteNonQuery();
                    koneksi.Close();
                    if (res == 1)
                    {
                        MessageBox.Show("Successfully insert data");
                        Form2_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Failed Insert Data");
                    }
                }
                else
                {
                    MessageBox.Show("Data Tidak lengkap !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text != "" && txtNamaPengguna.Text != "" && txtUsername.Text != "" && txtID.Text != "")
                {

                    query = string.Format("update tbl_pengguna set password = '{0}', nama_pengguna = '{1}', level = '{2}' where id_pengguna = '{3}'", txtPassword.Text, txtNamaPengguna.Text, comboBoxLevel.Text, txtID.Text);


                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    int res = perintah.ExecuteNonQuery();
                    koneksi.Close();
                    if (res == 1)
                    {
                        MessageBox.Show("Succsessfully update data");
                        Form2_Load(null, null);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update data");
                    }
                }
                else
                {
                    MessageBox.Show("Data Tidak lengkap !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtID.Text != "")
                {
                    if (MessageBox.Show("Are you sure to delete this?", "Warning", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        query = string.Format("Delete from tbl_pengguna where id_pengguna = '{0}'", txtID.Text);
                        ds.Clear();
                        koneksi.Open();
                        perintah = new MySqlCommand(query, koneksi);
                        adapter = new MySqlDataAdapter(perintah);
                        int res = perintah.ExecuteNonQuery();
                        koneksi.Close();
                        if (res == 1)
                        {
                            MessageBox.Show("Successfully delete data");
                        }
                        else
                        {
                            MessageBox.Show("Failed to delete Data");
                        }
                    }
                    Form2_Load(null, null);
                }
                else
                {
                    MessageBox.Show("Data Yang Anda Pilih Tidak Ada !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            try
            {
                Form2_Load(null, null);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            Form1 form1 = new Form1();
            form1.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtUsername.Text != "")
                {
                    query = string.Format("select * from tbl_pengguna where username = '{0}'", txtUsername.Text);
                    ds.Clear();
                    koneksi.Open();
                    perintah = new MySqlCommand(query, koneksi);
                    adapter = new MySqlDataAdapter(perintah);
                    perintah.ExecuteNonQuery();
                    adapter.Fill(ds);
                    koneksi.Close();
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow kolom in ds.Tables[0].Rows)
                        {
                            txtID.Text = kolom["id_pengguna"].ToString();
                            txtPassword.Text = kolom["password"].ToString();
                            txtNamaPengguna.Text = kolom["nama_pengguna"].ToString();
                            comboBoxLevel.Text = kolom["level"].ToString();

                        }
                        txtUsername.Enabled = false;
                        dataGridView1.DataSource = ds.Tables[0];
                        buttonSave.Enabled = false;
                        buttonUpdate.Enabled = true;
                        buttonDelete.Enabled = true;
                        buttonSearch.Enabled = false;
                        buttonClear.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Data Tidak Ada !!");
                        Form2_Load(null, null);
                    }

                }
                else
                {
                    MessageBox.Show("Data Yang Anda Pilih Tidak Ada !!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
