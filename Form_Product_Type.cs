using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjectInventoris
{
    public partial class Form_Product_Type : Form
    {
        Connector bridge = null;
        string err = "";



        //----Function----
        void input_Filter()//menyaring input yang akan digunakan
        {
            err = "";
            if (tb_Product_Type.Text == "") err += "Product Type tidak boleh kosong.";
        } 
        void input_Search_Filter()//menyaring input yang akan digunakan
        {
            err = "";
            var regexAlphaNumericSpace = new Regex("^[a-zA-Z0-9 '.]+$");
            var regexNumeric = new Regex("^[0-9]+$");
            if (tb_Search_Product_Type_Id.Text.Trim() != "" && !regexNumeric.IsMatch(tb_Search_Product_Type_Id.Text)) err += "Product Type ID only allow numbers.\n";
        } 
        void load_DGV(List<Product_Item_Type> list = null)//menampilkan isi tabel product_type ke dgv
        {
            
            if (list == null) list = bridge.Get_Product_Type_List(); //mengambil daftar jika list kosong
            dgv_Product_Type.DataSource = null; //data source dgv
            if (bridge.QueryStatus) //cek apakah perintah sql berhasil
            {//jika ya
                if (list?.Count > 0) //selama produk dalam list belum habis
                {
                    dgv_Product_Type.DataSource = list; //sumber data adalah list_product
                    //databinding kolom data grid view dengan data yang berada didalam daftar
                    this.dgv_Product_Type.Columns[0].DataPropertyName = nameof(Product_Item_Type.product_type_id);
                    this.dgv_Product_Type.Columns[1].DataPropertyName = nameof(Product_Item_Type.product_type_name);
                }
                dgv_Product_Type.ClearSelection();
                tb_Product_Type.Clear();
            }
            else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        //====Function end-----

        //-----Form start-----
        public Form_Product_Type(Connector _bridge)
        {
            InitializeComponent();
            this.bridge = _bridge;
            dgv_Product_Type.AutoGenerateColumns = false;
        }


        private void Form_Product_Type_Load(object sender, EventArgs e)
        {
            load_DGV();
        }
        
        private void btn_Update_Click(object sender, EventArgs e)
        {
            input_Filter();
            if (err != "") MessageBox.Show(err, "Error");

            else
            {
                int int_product_type_id;
                //parsing...
                if (!(Int32.TryParse(dgv_Product_Type.CurrentRow.Cells[0].Value.ToString(), out int_product_type_id))) err += "Failed when parsing product type in update.\n";
                
                //jika ada error, tampilkan error
                if(err !="") MessageBox.Show(err, "Error");
                else //jika tidak ada
                {
                    //buat objek product_item_type
                    Product_Item_Type product_type = new Product_Item_Type
                    {
                        product_type_id = int_product_type_id,
                        product_type_name = tb_Product_Type.Text
                    };
                    //panggil function edit_product_type dari connector
                    if (bridge.Update_Product_Type(product_type, dgv_Product_Type.CurrentRow.Cells[1].Value.ToString()))
                    {
                        MessageBox.Show("Update Success!"); //pesan update sukses
                        load_DGV(); //panggil load_dgv
                        tb_Product_Type.Text = ""; 
                    }
                    else MessageBox.Show(bridge.ErrorMessage, "Error"); //jika gagal, tampilkan error
                }
                
            }

        }
        
        private void btn_Add_Click(object sender, EventArgs e)
        {
            input_Filter();
            if (err != "") MessageBox.Show(err, "Error");

            else
            {
                Product_Item_Type product_type = new Product_Item_Type
                {
                    product_type_name = tb_Product_Type.Text
                };
                if (bridge.Add_Product_Type(product_type))
                {
                    MessageBox.Show("Add Success!");
                    load_DGV();
                    tb_Product_Type.Text = "";
                }
                else MessageBox.Show(bridge.ErrorMessage, "Error");
            }
        }
        
        private void btn_Search_Click(object sender, EventArgs e)
        {
            input_Search_Filter();
            if (err != "") MessageBox.Show(err, "Error");

            else
            {
                List<Product_Item_Type> list = bridge.Search_Product_Type(tb_Search_Product_Type_Id.Text, tb_Search_Product_Type_Name.Text);
                if (bridge.QueryStatus)
                {
                    load_DGV(list);
                }
                else MessageBox.Show(bridge.ErrorMessage, "Error");
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            load_DGV();
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            int int_product_type_id;
            DialogResult dialogResult = MessageBox.Show("Do you want to remove all related data to this product type?", "Option", MessageBoxButtons.YesNoCancel);
            if (dialogResult == DialogResult.Yes)
            {
                dialogResult = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (!(Int32.TryParse(dgv_Product_Type.CurrentRow.Cells[0].Value.ToString(), out int_product_type_id))) err += "Failed when parsing product type in update.\n";

                    if (err != "") MessageBox.Show(err, "Error");
                    if (bridge.Remove_Related_To_Product_Type(int_product_type_id))
                    {
                        MessageBox.Show("Remove Success!");
                        load_DGV();
                        tb_Product_Type.Text = "";
                    }
                    else MessageBox.Show(bridge.ErrorMessage, "Error");
                }
            }
            if (dialogResult == DialogResult.No)
            {
                dialogResult = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    if (!(Int32.TryParse(dgv_Product_Type.CurrentRow.Cells[0].Value.ToString(), out int_product_type_id))) err += "Failed when parsing product type in update.\n";

                    if (err != "") MessageBox.Show(err, "Error");
                    else
                    {
                        if (bridge.Remove_Product_Type(int_product_type_id))
                        {
                            MessageBox.Show("Remove Success!");
                            load_DGV();
                            tb_Product_Type.Text = "";
                        }
                        else MessageBox.Show(bridge.ErrorMessage, "Error");
                    }
                }
            }
        }
       
        private void tb_Search_Product_Type_Id_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Enter) e.Handled = false;
            else e.Handled = true;
        }

        private void tb_Search_Product_Type_Name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == '\'') e.Handled = false;
            else e.Handled = true;
        }

        private void tb_Search_Product_Type_Id_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Enter) e.Handled = false;
            else e.Handled = true;
        }

        private void dgv_Product_Type_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            tb_Product_Type.Text = dgv_Product_Type.CurrentRow.Cells[1].Value.ToString();
        }

        private void dgv_Product_Type_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_Product_Type.ClearSelection();
        }
        //------Form End-----
    }
}
