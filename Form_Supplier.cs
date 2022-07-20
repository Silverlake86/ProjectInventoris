using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjectInventoris
{
    public partial class Form_Supplier : Form
    {
        Connector bridge = null;
        string err = "";
        


        //-------Function---------
        void load_DGV(List<Supplier> list = null)
        {

            if (list == null) list = bridge.Get_Supplier_List();
            dgv_Supplier.DataSource = null;
            if (bridge.QueryStatus) //cek apakah perintah sql berhasil
            {//jika ya
                if (list?.Count > 0) //selama produk dalam list belum habis
                {
                    dgv_Supplier.DataSource = list; //sumber data adalah list_product
                    //databinding kolom data grid view dengan data yang berada didalam daftar
                    this.dgv_Supplier.Columns[0].DataPropertyName = nameof(Supplier.supplier_id);
                    this.dgv_Supplier.Columns[1].DataPropertyName = nameof(Supplier.supplier_name);
                    this.dgv_Supplier.Columns[2].DataPropertyName = nameof(Supplier.supplier_address);
                }
                dgv_Supplier.ClearSelection();
            }
            else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void input_Filter()
        {
            var regexAlphaNumericSpace = new Regex("^[a-zA-Z0-9 '.]+$");
            var regexNumeric = new Regex("^[0-9]+$");
            if (tb_Supplier_Name.Text == "") err += "Supplier Name is empty.\n";
            if (tb_Supplier_Address.Text == "") err += "Supplier Address is empty.";
        }

        void input_Search_Filter()
        {
            var regexAlphaNumericSpace = new Regex("^[a-zA-Z0-9 '.]+$");
            var regexNumeric = new Regex("^[0-9]+$");
            if (tb_Search_Supplier_ID.Text != "" && !regexNumeric.IsMatch(tb_Search_Supplier_ID.Text)) err += "Supplier ID is empty.\n";
        }


 

        //-----Form Events-----
        public Form_Supplier(Connector _bridge)
        {
            InitializeComponent();
            bridge = _bridge;
            dgv_Supplier.AutoGenerateColumns = false;
        }
        private void Form_Supplier_Load(object sender, EventArgs e)
        {
            load_DGV();
        }

        private void btn_Add_Click(object sender, EventArgs e)
        {
            input_Filter();
            if (err != "") MessageBox.Show(err, "Error");
            else
            {
                Supplier supplier = new Supplier
                {
                    supplier_name = tb_Supplier_Name.Text,
                    supplier_address = tb_Supplier_Address.Text
                };

                if (bridge.Add_Supplier(supplier))
                {
                    MessageBox.Show("Add Success");
                    load_DGV();
                    tb_Supplier_Address.Clear();
                    tb_Supplier_Name.Clear();
                }
                else MessageBox.Show(bridge.ErrorMessage);
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            input_Filter();
            if (err != "") MessageBox.Show(err, "Error");
            else
            {
                int int_supplier_id;
                err = "";
                if (!(Int32.TryParse(dgv_Supplier.CurrentRow.Cells[0].Value.ToString(), out int_supplier_id))) err += "Error when trying to parse supplier id in Update";
                if (err != "") MessageBox.Show(err);
                else
                {
                    Supplier supplier = new Supplier
                    {
                        supplier_id = int_supplier_id,
                        supplier_name = tb_Supplier_Name.Text,
                        supplier_address = tb_Supplier_Address.Text
                    };

                    if (bridge.Update_Supplier(supplier))
                    {
                        MessageBox.Show("Edit Success");
                        load_DGV();
                        tb_Supplier_Address.Clear();
                        tb_Supplier_Name.Clear();
                    }
                    else MessageBox.Show(bridge.ErrorMessage);
                }
                
            }
        }

        private void btn_Refresh_Click(object sender, EventArgs e)
        {
            load_DGV();
        }

        private void btn_Remove_Click(object sender, EventArgs e)
        {
            int int_supplier_id;
            if (!(Int32.TryParse(dgv_Supplier.CurrentRow.Cells[0].Value.ToString(), out int_supplier_id))) err += "Failed when parsing product type in update.\n";

            if (err != "") MessageBox.Show(err, "Error");
            else
            {
                DialogResult dialogResult = MessageBox.Show("Do you want to remove all related data to this product type?", "Option", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes)
                {
                    dialogResult = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {

                        if (bridge.Remove_Related_To_Supplier(int_supplier_id))
                        {
                            MessageBox.Show("Remove Success!");
                            load_DGV();
                            tb_Supplier_Address.Clear();
                            tb_Supplier_Name.Clear();
                        }
                        else MessageBox.Show(bridge.ErrorMessage, "Error");
                    }
                }
                if (dialogResult == DialogResult.No)
                {
                    dialogResult = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.Yes)
                    {
                        if (bridge.Remove_Supplier(int_supplier_id))
                        {
                            MessageBox.Show("Remove Success!");
                            load_DGV();
                            tb_Supplier_Address.Clear();
                            tb_Supplier_Name.Clear();
                        }
                        else MessageBox.Show(bridge.ErrorMessage, "Error");
                    }
                }
            }
            
        }

        private void dgv_Supplier_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            var row = dgv_Supplier.CurrentRow;
            tb_Supplier_Name.Text = row.Cells[1].Value.ToString();
            tb_Supplier_Address.Text = row.Cells[2].Value.ToString();
        }

        private void dgv_Supplier_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dgv_Supplier.ClearSelection();
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            input_Search_Filter();
            if (err != "") MessageBox.Show(err, "Error");
            else
            { 
                List <Supplier>  list = bridge.Search_Supplier(tb_Search_Supplier_ID.Text, tb_Search_Supplier_Name.Text, tb_Search_Supplier_Address.Text);
                if(bridge.QueryStatus)
                {
                    load_DGV(list);
                }
                else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);//jika tidak tampilkan pesan error
            }
        }

        private void tb_Search_Supplier_ID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Enter) e.Handled = false;
            else e.Handled = true;
        }

        private void tb_Supplier_Name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == '\'') e.Handled = false;
            else e.Handled = true;
        }
    }
}
