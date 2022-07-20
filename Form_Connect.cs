using System;
using System.Windows.Forms;


namespace ProjectInventoris
{
    public partial class Form_Connect : Form
    {
        public Form_Connect()
        {
            InitializeComponent(); //inisialisasi komponen form
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if ((this.tbServer.Text == ".\\" || this.tbServer.Text == "") && this.tbDBName.Text == "") //jika kedua textbox kosong 
            {
                MessageBox.Show("Please enter the valid Server and Database name!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information); // tampilkan pesan
            }
            else
            {
                string connection = this.tbServer.Text; // teks dari tbServer sebagai string koneksi
                string dbName = this.tbDBName.Text; // teks dari DBName sebagai string dbName
                Connector bridge = new Connector(connection, dbName); // inisialisasi koneksi ke dataabse

                if (bridge.QueryStatus == false) // jika query inisialisasi gagal, tampilkan pesan
                {
                    MessageBox.Show(bridge.ErrorMessage, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                else //jika tidak
                {
                    using (Form form = new Form_Main(bridge)) //buka form utama
                    {
                        this.Hide();
                        form.ShowDialog();
                        this.Close();
                    }
                }

            }
        }

        private void btn_Default_Click(object sender, EventArgs e) //tombol default untuk mengeset nilai tbDBName dan tbServer kesemula semula
        {
            tbDBName.Text = "InventoryManager";
            tbServer.Text = ".\\SQLEXPRESS";
        }
    }
}
