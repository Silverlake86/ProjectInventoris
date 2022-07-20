using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProjectInventoris
{
    public partial class Form_Main : Form
    {
        Connector bridge = null;
        List<Products_Item> list_Product = null;
        List<Product_Item_Type> list_Product_Type = null;
        List<Supplier> list_Supplier = null; 
        int selected_Row_Count;
        int need_Supply, ok;
        string err = ""; //pesan error

        //-----FUNCTION START-----
        void RowColor() //mewarnai kolom yang jumlahnya produk yang ada digudang dibawah minimal
        {
            int on_Hand, minimum;
            
            foreach (DataGridViewRow row in tab_Inventory_dgv_Inventory.Rows) //untuk setiap baris didalam tab_Inventory_dgv_Inventory
            {
                //coba parse nilai yang ada pada dgv lalu masukkan ke dalam on_hand dan minimum
                if (Int32.TryParse(row.Cells[6].Value.ToString(), out on_Hand) && Int32.TryParse(row.Cells[7].Value.ToString(), out minimum)) 
                {
                    if (on_Hand > minimum) 
                    {
                        row.DefaultCellStyle.BackColor = Color.White; //mensetel warna default
                        row.DefaultCellStyle.ForeColor = Color.Black;
                        ok++; //jumlah yang baik-baik saja bertambah
                    }
                    else if (on_Hand <= minimum)
                    {
                        row.DefaultCellStyle.BackColor = Color.Gray; //mensetel warna jika dibawah jumlah produk minimal
                        row.DefaultCellStyle.ForeColor = Color.White;
                        need_Supply++; //jumlah yang dibawah minimum bertambah
                    }
                }
                else MessageBox.Show("Failed when Converting inventory on hand and/or minimum required to integer."); //jika gagal parsing
                
            }
    }
       
        void load_DGV_Inventory(List<Products_Item> list = null)
        {
            need_Supply = 0;
            ok = 0;
            //load data grid view dari tabel produk
            tab_Inventory_dgv_Inventory.DataSource = null; //membersihkan sumber data data grid view Inventory
            if (list == null) list = bridge.Get_Product_list();//mengambil daftar produk dari sql jika tidak ada daftar yang diinput
            if (bridge.QueryStatus) //cek apakah perintah sql berhasil
            {//jika ya
                if (list?.Count > 0) //selama produk dalam list belum habis
                {
                    this.tab_Inventory_dgv_Inventory.DataSource = list; //sumber data adalah list_product
                    //databinding kolom data grid view dengan data yang berada didalam daftar
                    this.tab_Inventory_dgv_Inventory.Columns[0].DataPropertyName = nameof(Products_Item.product_id);
                    this.tab_Inventory_dgv_Inventory.Columns[1].DataPropertyName = nameof(Products_Item.product_name);
                    this.tab_Inventory_dgv_Inventory.Columns[2].DataPropertyName = nameof(Products_Item.product_type_name);
                    this.tab_Inventory_dgv_Inventory.Columns[3].DataPropertyName = nameof(Products_Item.starting_inventory);
                    this.tab_Inventory_dgv_Inventory.Columns[4].DataPropertyName = nameof(Products_Item.inventory_received);
                    this.tab_Inventory_dgv_Inventory.Columns[5].DataPropertyName = nameof(Products_Item.inventory_shipped);
                    this.tab_Inventory_dgv_Inventory.Columns[6].DataPropertyName = nameof(Products_Item.inventory_on_hand);
                    this.tab_Inventory_dgv_Inventory.Columns[7].DataPropertyName = nameof(Products_Item.minimum_required);
                }
                RowColor(); //panggil method RowColor()
                //mensetel teks tab_Inventory_tb_Need_Supplu sesuai dengan nilai need_Supply
                tab_Inventory_lbl_Need_Supply.Text = this.need_Supply.ToString(); 
                //mensetel teks tab_Inventory_dgv_Inventory sesuai dengan nilai need_Supply
                tab_Inventory_lbl_Ok.Text = this.ok.ToString(); 
                tab_Inventory_dgv_Inventory.ClearSelection(); //membersihkan dgv dari pilihan yang ada
            }
            //jika tidak, maka panggil messagebox
            else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        void load_DGV_Order(List<Order> list = null)
        {
            need_Supply = 0;
            ok = 0;
            //load data grid view dari tabel produk
            tab_Order_dgv_Order.DataSource = null; //membersihkan sumber data data grid view Inventory
            if (list == null) list = bridge.Get_Order_List();//mengambil daftar produk dari sql jika tidak ada daftar yang diinput
            if (bridge.QueryStatus) //cek apakah perintah sql berhasil
            {//jika ya
                if (list?.Count > 0) //selama produk dalam list belum habis
                {
                    tab_Order_dgv_Order.DataSource = list; //sumber data adalah list_product
                    //databinding kolom data grid view dengan data yang berada didalam daftar
                    this.tab_Order_dgv_Order.Columns[0].DataPropertyName = nameof(Order.order_id);
                    this.tab_Order_dgv_Order.Columns[1].DataPropertyName = nameof(Order.order_date);
                    this.tab_Order_dgv_Order.Columns[2].DataPropertyName = nameof(Order.product_id);
                    this.tab_Order_dgv_Order.Columns[3].DataPropertyName = nameof(Order.product_name);
                    this.tab_Order_dgv_Order.Columns[4].DataPropertyName = nameof(Order.product_type_name);
                    this.tab_Order_dgv_Order.Columns[5].DataPropertyName = nameof(Order.title);
                    this.tab_Order_dgv_Order.Columns[6].DataPropertyName = nameof(Order.number_shipped);
                }
                tab_Order_dgv_Order.ClearSelection(); //membersihkan dgv dari pilihan yang ada
            }
            else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //jika tidak tampilkan pesan
        }
        void load_DGV_Purchase(List<Purchase> list = null)
        {
            //load data grid view dari tabel produk
            tab_Purchase_dgv_Purchase.DataSource = null; //membersihkan sumber data data grid view Inventory
            if (list == null) list = bridge.Get_Purchase_List();//mengambil daftar produk dari sql jika tidak ada daftar yang diinput
            if (bridge.QueryStatus) //cek apakah perintah sql berhasil
            {//jika ya
                if (list?.Count > 0) //selama produk dalam list belum habis
                {
                    tab_Purchase_dgv_Purchase.DataSource = list; //sumber data adalah list_product
                    //databinding kolom data grid view dengan data yang berada didalam daftar
                    this.tab_Purchase_dgv_Purchase.Columns[0].DataPropertyName = nameof(Purchase.purchase_id);
                    this.tab_Purchase_dgv_Purchase.Columns[1].DataPropertyName = nameof(Purchase.purchase_date);
                    this.tab_Purchase_dgv_Purchase.Columns[2].DataPropertyName = nameof(Purchase.product_id);
                    this.tab_Purchase_dgv_Purchase.Columns[3].DataPropertyName = nameof(Purchase.product_name);
                    this.tab_Purchase_dgv_Purchase.Columns[4].DataPropertyName = nameof(Purchase.supplier_name);
                    this.tab_Purchase_dgv_Purchase.Columns[5].DataPropertyName = nameof(Purchase.number_received);
                }
            }
            else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void load_cb_Product_Type_Inventory()
        {
            tab_Inventory_cb_Product_Type.Items.Clear(); //bersihkan item-item yang ada didalam tab_Inventory_cb_Product_Type
            tab_Inventory_cb_Search_Product_Type.Items.Clear();//bersihkan item-item yang ada didalam tab_Inventory_cb_Search_Product_Type
            list_Product_Type = null; //bersihkan list_Product_Type
            //isi list_Product_Type dengan memanggil daftar produk type dari database dengan menggunakan Get_Product_Type_List()
            list_Product_Type = bridge.Get_Product_Type_List(); 
            tab_Inventory_cb_Search_Product_Type.Items.Add("NONE"); //tambah item "NONE" kedalam  tab_Inventory_cb_Search_Product_Type
            if (bridge.QueryStatus) //cek apakah perintah sql berhasil
            {
                if (list_Product_Type != null)  //jika list tidak kosong
                {
                    foreach (var product_item_type in list_Product_Type) //untuk setiap product_item_type yang ada didalam list
                    {
                        //masukkan product_type_name dari product_item_type kedalam combobox tab_Inventory_cb_Product_Type
                        tab_Inventory_cb_Product_Type.Items.Add(product_item_type.product_type_name);

                        //masukkan product_type_name dari product_item_type kedalam combobox tab_Inventory_cb_Search_Product_Type
                        tab_Inventory_cb_Search_Product_Type.Items.Add(product_item_type.product_type_name); 
                    }
                    tab_Inventory_cb_Product_Type.SelectedIndex = 0; //pilihan tab_Inventory_cb_Product_Type adalah pilihan yang pertama

                    //pilihan tab_Inventory_cb_Search_Product_Type adalah pilihan yang pertama
                    tab_Inventory_cb_Search_Product_Type.SelectedIndex = 0;
                }
            }
            // jika tidak tampilkan pesan error
            else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        void load_cb_Product_Type_Order()
        {
            tab_Order_cb_Search_Product_Type.Items.Clear(); //bersihkan isi dari tab_Order_cb_Search_Product_Type
            //load daftar tipe produk ke combo box product type
            list_Product_Type = null; //kosongkan isi list_Product_Type
            //isi list_Product_Type dengan mengambil daftar product type dari database dengan menggunakan Get_Product_Type_List dari connector
            list_Product_Type = bridge.Get_Product_Type_List(); 
            tab_Order_cb_Search_Product_Type.Items.Add("NONE"); //tambahkan item "NONE" kedalam tab_Order_cb_Search_Product_Type
            if (bridge.QueryStatus) //cek apakah perintah sql berhasil
            {
                if(list_Product_Type != null) //jika list kosong
                {
                    foreach (var product_item_type in list_Product_Type) //untuk setiap product_item_type yang ada didalam list_Product_Type
                    {
                        //isi product_type_name product_item_type tersebut kedalam tab_Order_cb_Search_Product_Type
                        tab_Order_cb_Search_Product_Type.Items.Add(product_item_type.product_type_name);
                    }
                    //mensetel pilihan  tab_Order_cb_Search_Product_Type menjadi item pertama
                    tab_Order_cb_Search_Product_Type.SelectedIndex = 0; 
                }               
            }
            else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); // tampilkan pesan error
        } 

        void load_cb_Product_Name_Order()
        {
            tab_Order_cb_Product_Name.Items.Clear(); //bersihkan isi dari tab_Order_cb_Product_Name
            list_Product = null; //bersihkan isi dari list_Product
            //isi list_Product dengan daftar product dari database dengan memanggil Get_Product_List dari connector
            list_Product = bridge.Get_Product_list(); 
            if (bridge.QueryStatus)//jika perintah sql berhasil
            {
                if(list_Product != null) //jika isi list_Product tidak kosong
                {
                    foreach (var item in list_Product) //untuk setiap objek yang ada didalam list_Product
                    {
                        tab_Order_cb_Product_Name.Items.Add(String.Format("{0,5} | {1}",item.product_id, item.product_name)); //tambahkan kedalam tab_Order_cb_Product_Name.Items
                    }
                    tab_Order_cb_Product_Name.SelectedIndex = 0;//membuat pilihan awal dari tab_Order_cb_Product_Name menjadi item pertama
                }
            }
            else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);//tampilkan pesan
        }
        void load_cb_Supplier_Name_Purchase()
        {
            tab_Purchase_cb_Supplier.Items.Clear();
            list_Supplier = null;
            list_Supplier = bridge.Get_Supplier_List();
            if (bridge.QueryStatus)
            {
                if (list_Supplier != null)
                {
                    foreach (var item in list_Supplier)
                    {
                        tab_Purchase_cb_Supplier.Items.Add(item.supplier_name);
                        tab_Purchase_cb_Search_Supplier.Items.Add(item.supplier_name);
                    }
                    tab_Purchase_cb_Supplier.SelectedIndex = 0;
                    tab_Purchase_cb_Search_Supplier.SelectedIndex = 0;
                }
            }
            else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        void load_cb_Product_Name_Purchase()
        {
            tab_Purchase_cb_Product_Name.Items.Clear();
            list_Product = null;
            list_Product = bridge.Get_Product_list();
            if (bridge.QueryStatus)
            {
                if (list_Product != null)
                {
                    foreach (var item in list_Product)
                    {
                        tab_Purchase_cb_Product_Name.Items.Add(String.Format("{0,5} | {1}", item.product_id, item.product_name)); //tambahkan kedalam tab_Order_cb_Product_Name.Items
                    }
                    tab_Purchase_cb_Product_Name.SelectedIndex = 0;
                }
            }
            else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        void input_Filter_Inventory()
        {
            err = ""; //kosongkan pesan error
            var regexAlphaNumericSpace = new Regex("^[a-zA-Z0-9 '.]+$"); //mengatur apa saja yang layak diterma
            var regexNumeric = new Regex("^[0-9]+$");
            //masukkan pesan jika isi tidak sesuai atau kosong
            if (!regexAlphaNumericSpace.IsMatch(tab_Inventory_tb_Product_Name.Text)) err += "Product Name only allow numbers, alphabetics and space\n";
            if (tab_Inventory_cb_Product_Type.Text.Trim() == "") err += "You must select one of the option in Product Type.";
            if (!regexNumeric.IsMatch(tab_Inventory_tb_Minimum_Required.Text)) err += "Required Amount only allow numbers\n";
            if (!regexNumeric.IsMatch(tab_Inventory_tb_Starting_Inventory.Text)) err += "Starting Inventory only allow numbers\n";
        }
        
        void input_Filter_Search_Inventory()
        {
            err = ""; //kosongkan pesan error
            var regexNumeric = new Regex("^[0-9]+$");
            //masukkan pesan jika isi tidak sesuai
            if (tab_Inventory_tb_Search_Product_Id.Text.Trim() != "" && !regexNumeric.IsMatch(tab_Inventory_tb_Search_Product_Id.Text)) err += "Product ID only allow numbers.\n";
            if (tab_Inventory_cb_Search_Product_Type.Text.Trim() == "") err += "You must select one of the option in Product Type.";
            if (tab_Inventory_tb_Search_Required_Amount.Text.Trim() != "" && !regexNumeric.IsMatch(tab_Inventory_tb_Search_Required_Amount.Text)) err += "Required Amount only allow numbers.\n";
            if (tab_Inventory_tb_Search_Starting_Inventory.Text.Trim() != "" && !regexNumeric.IsMatch(tab_Inventory_tb_Search_Starting_Inventory.Text)) err += "Starting Inventory only allow numbers.";
        }
        
        void input_Filter_Order()
        {
            err = ""; //kosongkan pesan error
            var regexNumeric = new Regex("^[0-9]+$"); //mengatur apa saja yang layak diterma
            //masukkan pesan jika isi kosong atau tidak sesuai
            if (tab_Order_dtp_Order_Date.Text == "") err += "Order Date is not allowed to be empty.";
            if (tab_Order_tb_Title.Text.Trim() == "") err += "Title is not allowed to be empty.\n";
            if (tab_Order_cb_Product_Name.Text.Trim() == "") err += "You must select one of the option in Product Name\n";
            if (!regexNumeric.IsMatch(tab_Order_tb_Number_Shipped.Text)) err += "Number Shipped only allow numbers\n";
        }

        void input_Filter_Search_Order()
        {
            err = ""; //kosongkan pesan error
            var regexAlphaNumericSpace = new Regex("^[a-zA-Z0-9 '.]+$");//mengatur apa saja yang layak diterma
            var regexNumeric = new Regex("^[0-9]+$");
           
            //masukkan pesan jika isi tidak seusai
            if (tab_Order_tb_Search_Order_ID.Text.Trim() != "" && !regexNumeric.IsMatch(tab_Order_tb_Search_Order_ID.Text)) err += "Order ID only allow numbers.\n";
            if (tab_Order_cb_Search_Product_Type.Text.Trim() == "") err += "You must select one of the option in Product Type.";
            if (tab_Order_tb_Number_Shipped.Text.Trim() != "" && !regexNumeric.IsMatch(tab_Order_tb_Number_Shipped.Text.Trim())) err += "Number Shipped only allow numbers.\n";
        }
        void input_Filter_Purchase()
        {
            err = "";
            var regexNumeric = new Regex("^[0-9]+$");

            if (tab_Purchase_dtp_Purchase_Date.Text == "") err += "Order Date is not allowed to be empty.";
            if (tab_Purchase_cb_Product_Name.Text.Trim() == "") err += "You must select one of the option in Product Name\n";
            if (tab_Purchase_cb_Supplier.Text.Trim() == "") err += "You must select one of the option in Supplier Name\n";
            if (!regexNumeric.IsMatch(tab_Purchase_tb_Number_Received.Text)) err += "Number Shipped only allow numbers\n";
        }
        void input_Filter_Search_Purchase()
        {
            err = "";
            var regexNumeric = new Regex("^[0-9]+$");
            if (tab_Purchase_tb_Search_Purchase_ID.Text.Trim() != "" && !regexNumeric.IsMatch(tab_Purchase_tb_Search_Purchase_ID.Text)) err += "Purchase ID only allow numbers.\n";
            if (tab_Purchase_tb_Search_Number_Received.Text.Trim() != "" && !regexNumeric.IsMatch(tab_Purchase_tb_Search_Number_Received.Text.Trim())) err += "Number Received only allow numbers.\n";
        }

        void clear_Input_Object_Inventory()
        {
            tab_Inventory_tb_Product_Name.Text = "";
            tab_Inventory_tb_Minimum_Required.Text = "";
            tab_Inventory_tb_Starting_Inventory.Text = "";
            if(tab_Inventory_cb_Product_Type.Items.Count >0) tab_Inventory_cb_Product_Type.SelectedIndex = 0;

        }

        void clear_Input_Object_Order()
        {
            tab_Order_tb_Number_Shipped.Clear();
            tab_Order_dtp_Order_Date.Value = DateTime.Now;

            tab_Order_tb_Title.Clear();
            if (tab_Order_cb_Product_Name.Items.Count >0)
            {
                tab_Order_cb_Product_Name.SelectedIndex = 0;
            }
        }

        void clear_Input_Object_Purchase()
        {
            tab_Purchase_dtp_Purchase_Date.Value = DateTime.Now;
            tab_Purchase_tb_Number_Received.Clear();
            if(tab_Purchase_cb_Product_Name.Items.Count > 0)
            {
                tab_Purchase_cb_Product_Name.SelectedIndex = 0;
            }
            if(tab_Purchase_cb_Supplier.Items.Count > 0)
            {
                tab_Purchase_cb_Supplier.SelectedIndex = 0;
            }
        }

        

        //-----FORM START-----
        public Form_Main(Connector _bridge)
        {
            InitializeComponent(); //inisiasi isi form
            this.bridge = _bridge; //menyetel connector dari form sebelumnya kepada connector pada form ini
            tab_Inventory_dgv_Inventory.AutoGenerateColumns = false; //mengatur agar AutoGenerateCollumn dari dgv agar tidak terjadi

            tab_Order_dgv_Order.AutoGenerateColumns = false;//mengatur agar AutoGenerateCollumn dari dgv agar tidak terjadi
                                                            //mengatur format tanggal pada kolom pertama tab_Order_dgv_Order
            tab_Order_dgv_Order.Columns[1].DefaultCellStyle.Format = "dd/MM/yyyy";

            tab_Purchase_dgv_Purchase.AutoGenerateColumns = false;

        }
        private void Form_Main_Load(object sender, EventArgs e)
        {
            load_DGV_Inventory(); //panggil function load_DGV_Inventory()
            load_cb_Product_Type_Inventory(); // panggil functiion load_cb_Product_Type_Inventory()
 
        }

        private void tabView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabView.SelectedTab == tabView.TabPages["tab_Current_Inventory"]) //jika tab berganti ke halaman Current Inventory
            {
                load_DGV_Inventory(); //panggil function load_DGV_Inventory()
                load_cb_Product_Type_Inventory(); // panggil functiion load_cb_Product_Type_Inventory()
            }

            else if (tabView.SelectedTab == tabView.TabPages["tab_Record_Purchase"])
            {
                load_DGV_Purchase();
                load_cb_Supplier_Name_Purchase();
                load_cb_Product_Name_Purchase();
                tab_Purchase_dgv_Purchase.ClearSelection();
                tab_Purchase_cb_Number_Received_Filter.SelectedIndex = 0;
            }

            else if (tabView.SelectedTab == tabView.TabPages["tab_Record_Order"]) //jika tab berganti kehalaman Record Order
            {
                load_DGV_Order(); //panggil function load_DGV_Order()
                load_cb_Product_Type_Order(); //panggil function load_cb_Product_Type_Order()
                load_cb_Product_Name_Order(); //panggil function load_cb_Product_Name_Order()
                //pilihan awal dari  tab_Order_cb_Search_Number_Shipped_Filter_Type adalah item yang pertama
                tab_Order_cb_Search_Number_Shipped_Filter_Type.SelectedIndex = 0; 
            }

        }
        
       //------------------------------------------------------------------------------
        private void tb_Product_Name_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetter(e.KeyChar) || Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Space || e.KeyChar == (char)Keys.Back || e.KeyChar == '.' || e.KeyChar == '\'' ) e.Handled = false; //jika tombol yang ditekan sesuai, maka input diterima
            else e.Handled = true; //jika tidak, maka input ditolak
        }
        
        private void tb_Starting_Inventory_KeyPress(object sender, KeyPressEventArgs e)
        {
            //jika tombol yang ditekan sesuai, maka input diterima
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Enter) e.Handled = false; 
            else e.Handled = true;//jika tidak, maka input ditolak
        }
       
        private void tab_Inventory_dgv_Inventory_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected_Row_Count = e.RowIndex; //urutan dari baris yang dipilih
            if (selected_Row_Count >= 0) //memastikan ada baris yang dipilih
            {
                var row = this.tab_Inventory_dgv_Inventory.CurrentRow; //baris yang dipilih

                //memasukkan nilai-nilai kolom yang ada pada baris kedalam textbox dan combobox
                tab_Inventory_tb_Product_Name.Text = row.Cells[1].Value.ToString();
                tab_Inventory_cb_Product_Type.Text = row.Cells[2].Value.ToString();
                tab_Inventory_tb_Starting_Inventory.Text = row.Cells[3].Value.ToString();
                tab_Inventory_tb_Minimum_Required.Text = row.Cells[7].Value.ToString();
            }
        }
        
        private void tab_Inventory_dgv_Inventory_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            this.tab_Inventory_dgv_Inventory.ClearSelection(); //membersihkan dgv dari pilihan
        }
        
        private void tab_Inventory_btn_Add_Click(object sender, EventArgs e)
        {
            input_Filter_Inventory(); //panggil function input_Filter_Inventory()
            if (err.Trim() != "") MessageBox.Show(err); // jika ada pesan error setelah input_Filter_Inventory() berjalan
            else//jika tidak 
            {
                err = ""; //kosongkan pesan error
                int int_starting_inventory, int_minimum_required;

                //parsing textbox, dan masukkan hasil parsing kedalam int_starting_inventory dan int_minimum_required
                //masukkan pesan error jika parsing gagal
                if (!(Int32.TryParse(tab_Inventory_tb_Starting_Inventory.Text, out int_starting_inventory))) err += "Error when parsing starting inventory in Add Inventory.\n";
                if (!(Int32.TryParse(tab_Inventory_tb_Minimum_Required.Text, out int_minimum_required))) err += "Error when parsing minimum required in Add Inventory.";
                if(err.Trim() != "" ) MessageBox.Show(err, "Error"); //jika pesan error tidak kosong, tampilkan error
                else//jika tidak
                {
                    Products_Item product = new Products_Item //buat objek Producs_Item baru
                    {
                        //isi property sesuai dengan objek input mereka
                        product_name = tab_Inventory_tb_Product_Name.Text,
                        product_type_name = tab_Inventory_cb_Product_Type.Text,
                        starting_inventory = int_starting_inventory,
                        inventory_received = 0,
                        inventory_shipped = 0,
                        inventory_on_hand = int_starting_inventory,
                        minimum_required = int_minimum_required
                    };
                    if (bridge.Add_Product(product)) //jalankan Add_Product dari connector, jika berhasil jalankan code berikut
                    {
                        MessageBox.Show("Add Success!"); //pesan sukses
                        load_DGV_Inventory(); // tampilkan isi tabel Inventory yang baru

                        //kosongkan objek-objek input
                        clear_Input_Object_Inventory();
                    }
                    //tampilkan pesan error jika Add_Product() gagal
                    else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tab_Inventory_btn_Update_Click(object sender, EventArgs e)
        {
            input_Filter_Inventory(); //panggil function  input_Filter_Inventory()

            //jika ada isi pesan error setelah menjalankan function input_Filter_Inventory()
            if (err.Trim() != "") MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            else
            {
                var row = this.tab_Inventory_dgv_Inventory.CurrentRow; //baris yang dipilih
                if (selected_Row_Count >= 0) //jika baris memang ada
                {
                    err = "";//kosongkan pesan error
                    int int_product_id, int_starting_inventory, int_minimum_required;

                    //parsing nilai dan masukkan kedalam int_product_id, int_starting_inventory dan int_minimum_required
                    //jika tidak berhasil masukkan pesan error
                    if (!(Int32.TryParse(row.Cells[0].Value.ToString(), out int_product_id))) err += "Error when parsing product id in update inventory.\n";
                    if (!(Int32.TryParse(tab_Inventory_tb_Starting_Inventory.Text, out int_starting_inventory))) err += "Error when parsing starting inventory in update inventory.\n";
                    if (!(Int32.TryParse(tab_Inventory_tb_Minimum_Required.Text, out int_minimum_required))) err += "Error when parsing minimum required in update inventory.";
                    if (err.Trim() != "") MessageBox.Show(err, "Error"); //jika ada pesan error, tampilkan pesan
                    else
                    {
                        //buat object Products_Item yang baru yang isinya product lama yang ada pada baris yang dipilih
                        //dengan memanggil function pada Get_Product_By_ID pada connector()
                        Products_Item old_product = bridge.Get_Product_By_ID(int_product_id);
                        if (bridge.QueryStatus)//  Get_Product_By_ID pada connector() berhasil                       
                        {
                            //buat object Products_Item baru yang isinya objek produk yang baru
                            Products_Item new_product = new Products_Item 
                            {
                                //isi dari new_Products_Item yang diisi dengan objek input mereka masing-masing
                                product_id = int_product_id,
                                product_name = tab_Inventory_tb_Product_Name.Text,
                                product_type_name = tab_Inventory_cb_Product_Type.Text,
                                starting_inventory = int_starting_inventory,
                                inventory_received = 0,
                                inventory_shipped = 0,
                                inventory_on_hand = int_starting_inventory,
                                minimum_required = int_minimum_required
                            };

                            ////jalankan function Update_Product dari connector, jika berhasil jalankan 
                            if (bridge.Update_Product(old_product, new_product)) 
                            {
                                MessageBox.Show("Update Success!"); //pesan update berhasil
                                load_DGV_Inventory(); //tampilkan isi tabel Inventory yang baru
                                clear_Input_Object_Inventory();
                            }
                            else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //tampilkan pesan error
                         }
                         else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);//tampilkan pesan error
                    }
                }
            }
        }

        private void tab_Inventory_btn_Search_Click(object sender, EventArgs e)
        {
            input_Filter_Search_Inventory(); //jalankan function  input_Filter_Search_Inventory()


            //jika ada pesan error, maka tampilkan pesan error
            if (err.Trim() != "") MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else //jika tidak
            {   //cari daftar product yang sesuai dengan yang ingin dicari dengan Search_Product dari connector
                list_Product = bridge.Search_Product(tab_Inventory_tb_Search_Product_Id.Text, tab_Inventory_tb_Search_Product_Name.Text, tab_Inventory_cb_Search_Product_Type.Text, tab_Inventory_tb_Search_Starting_Inventory.Text, tab_Inventory_tb_Search_Required_Amount.Text);
                if (bridge.QueryStatus)//jika query berhasil
                {
                    load_DGV_Inventory(list_Product);//tampilkan isi hasil pencarian
                }

                else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);//jika tidak tampilkan pesan error
            }          
        }
            
        private void tab_Inventory_btn_Refresh_Click(object sender, EventArgs e) 
        {
            //Mengambil ulang daftar produk dari tabel produk dalam database
            load_DGV_Inventory();
            load_cb_Product_Type_Inventory();
        }

        private void tab_Inventory_btn_Remove_Click(object sender, EventArgs e)
        {
            int int_product_id;
            if (!(Int32.TryParse(tab_Inventory_dgv_Inventory.CurrentRow.Cells[0].Value.ToString(), out int_product_id))) err += "Failed when parsing product in Delete Inventory.\n";
            if (err != "") MessageBox.Show(err, "Error");
            else
            {
                //tampilkan pesan konfirmasi
                DialogResult dialogResult = MessageBox.Show("Do you want to remove all related data to this product?", "Option", MessageBoxButtons.YesNoCancel);
                if (dialogResult == DialogResult.Yes) //jika pilihan adalah yes
                {
                    //tampilkan pesan konfirmasi sekali lagi
                    DialogResult dialogResult2 = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo);
                    if (dialogResult2 == DialogResult.Yes) //jika ya, maka
                    {
                        if (bridge.Remove_Related_To_Product(int_product_id))//panggil function Remove_Related_To_Product dari connector
                        {
                            MessageBox.Show("Remove Success!"); //pesan hapus berhasil
                            load_DGV_Inventory();
                            clear_Input_Object_Inventory();
                        }
                        else MessageBox.Show(bridge.ErrorMessage, "Error"); //tampilkan pesan error jika gagal
                    }
                }
                if (dialogResult == DialogResult.No)//jika pilihan tidak
                {
                    // tampilkan pesan konfirmasi
                    DialogResult dialogResult2 = MessageBox.Show("Are you sure?", "Confirmation", MessageBoxButtons.YesNo); 
                    if (dialogResult2 == DialogResult.Yes)//jika pilihan ya
                    {
                        if (bridge.Remove_Product(int_product_id)) //panggil function Remove__Product dari connector
                        {
                            MessageBox.Show("Remove Success!"); //pesan hapus berhasil
                            load_DGV_Inventory();//tampilkan isi inventory
                            clear_Input_Object_Inventory();
                        }
                        else MessageBox.Show(bridge.ErrorMessage, "Error");
                    }
                }
            }
            
        }
        //----TAB ORDER START-----
        private void tab_Order_dgv_Order_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected_Row_Count = e.RowIndex; //urutan baris yang dipilih
            if (selected_Row_Count >= 0) //jika pilihan ada
            { 
                var row = this.tab_Order_dgv_Order.CurrentRow; //baris yang dipilih
                err = "";//pesan error kosong
                DateTime date;

                //coba parsing tanggal lalu masukkan kedalam variable date, jika gagal masukkan pesan error
                if (!(DateTime.TryParse(row.Cells[1].Value.ToString(), out date))) err += "Error when parsing date in order dgv.";
                if (err.Trim() != "") MessageBox.Show(err, "Error"); //jika ada pesan error, tampilkan pesan error
                else //jika tidak ada
                {
                    //isi informasi yang dibutuhkan kedalam objek input
                    tab_Order_dtp_Order_Date.Value = date; 
                    tab_Order_cb_Product_Name.Text = String.Format("{0,5} | {1}", row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString());
                    tab_Order_tb_Title.Text = row.Cells[5].Value.ToString();
                    tab_Order_tb_Number_Shipped.Text = row.Cells[6].Value.ToString();
                }

            }
        }

        private void tab_Order_dgv_Order_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tab_Order_dgv_Order.ClearSelection(); //membersihkan pilihan pada dgv
        }

        private void tab_Order_tb_Number_Shipped_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Enter) e.Handled = false; //jika tombol yang ditekan sesuai, maka input diterima
            else e.Handled = true;//jika tidak maka ditolak
        }
        
        private void tab_Order_btn_Add_Click(object sender, EventArgs e)
        {
            input_Filter_Order(); //panggil function input_Filter_Order()
            if (err.Trim() != "") MessageBox.Show(err); //jika ada pesan error, tampilkan pesan error
            else // jika tidak ada 
            {
                err = ""; //kosongkan pesan error
                DateTime date;
                string[] string_product_id;
                int int_number_shipped, int_product_id;
                //mengambil nilai dari combobox product name
                string_product_id = tab_Order_cb_Product_Name.Text.Replace(" ", "").Split('|');

                //parsing dan jika berhasil masukkan kedalam variabel-variabel, jika tidak masukkan pesan error
                if (!(Int32.TryParse(string_product_id[0], out int_product_id))) err += "Error when parsing product id in add order.\n";
                if (!(Int32.TryParse(tab_Order_tb_Number_Shipped.Text, out int_number_shipped))) err += "Error when parsing number shipped in add order.\n";
                if (!(DateTime.TryParse(tab_Order_dtp_Order_Date.Value.ToString(), out date))) err += "Error when parsing date in add order.";
                if (err!= "") MessageBox.Show(err); //jika ada pesan error, tampilkan pesan error
                else // jika tidak ada
                {
                    Order order = new Order //buat objek baru
                    {
                        //nilai berasal dari objek input dan variabel hasil parsing
                        title = tab_Order_tb_Title.Text,
                        product_id = int_product_id,
                        number_shipped = int_number_shipped,
                        order_date = date
                    };
                    if (bridge.Add_Order(order)) //panggil function Add_Order dari connector
                    {
                        MessageBox.Show("Add Success!"); //pesan ad berhasil
                        load_DGV_Order(); //tambilkan tabel order yang baru
                        clear_Input_Object_Order();
                        
                    }
                    else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //tampilkan pesan error
                }
            }
        }
        private void tab_Order_btn_Update_Click(object sender, EventArgs e)
        {
            input_Filter_Order(); //panggil function input_Filter_Order()
            if (err.Trim() != "") MessageBox.Show(err); //jika ada pesan error dari input_Filter_Order(), tampilkan pesan error
            else //jika tidak
            {
                string[] string_product_id;
                int order_ID, int_number_shipped, int_product_id;
                DateTime date;
                err = ""; // kosongkan pesan error

                //ambil nilai dari combobox
                string_product_id = tab_Order_cb_Product_Name.Text.Replace(" ", "").Split('|'); 

                //parsing dan jika berhasil masukkan kedalam variabel, jika gagal masukkan pesan error
                if (!(Int32.TryParse(string_product_id[0], out int_product_id))) err += "Error when parsing product id in add order.\n";
                if (!(Int32.TryParse(tab_Order_dgv_Order.CurrentRow.Cells[0].Value.ToString(), out order_ID))) err += "Error when parsing order id in Order Update.\n";
                if (!(Int32.TryParse(tab_Order_tb_Number_Shipped.Text, out int_number_shipped))) err += "Error when parsing number shipped in add order.\n";
                if (!(DateTime.TryParse(tab_Order_dtp_Order_Date.Value.ToString(), out date))) err += "Error when parsing date in Order Update.\n";
                
                if (err.Trim()!= "") MessageBox.Show(err); //jika ada pesan error, tampilkan pesan error
                else // jika tidak ada
                {
                        
                    Order old_Order = bridge.Get_Order_By_ID(order_ID); //ambil order yang lama
                    Order new_Order = new Order //buat objek order yang baru
                    {
                        //isi dengan data yang ada pada objek-objek input
                        order_id = order_ID,
                        title = tab_Order_tb_Title.Text,
                        product_id = int_product_id,
                        number_shipped = Int32.Parse(tab_Order_tb_Number_Shipped.Text),
                        order_date = date
                    };
                    if (bridge.Update_Order(old_Order, new_Order)) //panggil fungsi Update_Order dari connector
                    {
                        MessageBox.Show("Edit Success!");//pesan edit sukses
                        load_DGV_Order();//tampilkan tabel order yang baru
                        clear_Input_Object_Order();
                    }
                    //jika gagal maka tampilkan error
                    else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                }
                
            }
        }

        private void tab_Order_btn_Search_Click(object sender, EventArgs e)
        {
            input_Filter_Search_Order(); //panggil function  input_Filter_Search_Order()
            //jika ada pesan error dari function  input_Filter_Search_Order(), tampilkan pesan error
            if (err != "") MessageBox.Show(err, "Error"); 
            else
            { 
                //ambil daftar order yang sesuai dengan kriteria dengan memanggil function Search_Order dari connector
                List<Order> order_List = bridge.Search_Order(tab_Order_cb_Search_Number_Shipped_Filter_Type.SelectedIndex, tab_Order_cbox_Search_Date.Checked, tab_Order_dtp_Search_Order_Date_1.Value, tab_Order_tb_Search_Order_ID.Text, tab_Order_tb_Search_Product_Name.Text, tab_Order_cb_Search_Product_Type.Text, tab_Order_tb_Search_Title.Text, tab_Order_tb_Search_Number_Shipped.Text);
                if (bridge.QueryStatus) //jika berhasil
                {
                    load_DGV_Order(order_List); //tampilkan hasil pencarian
                }
                else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //jika gagal tampilkan error
            }
        }

        private void tab_Order_dtp_Order_Date_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true; //agar date time picker tidak dapat diganggu
        }

        private void tab_Order_btn_Remove_Click(object sender, EventArgs e)
        {
            int int_order_id;
            //parsing dan jika berhasil masukkan hasil kedalam variabel, jika tidak masukkan pesan error
            if (!(Int32.TryParse(tab_Order_dgv_Order.CurrentRow.Cells[0].Value.ToString(), out int_order_id))) err += "Failed when parsing order id in Delete Order.\n";

            if (err != "") MessageBox.Show(err, "Error");//jika ada pesan error, tampilkan pesan error
            else //jika tidak ada
            {   //tampilka pesan konfirmasi 
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove this product?", "Option", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)//jika pilihan yes
                { 
                    if (bridge.Remove_Order(int_order_id)) //panggl function Remove_Order dari connector, jika berhasil
                    {
                        MessageBox.Show("Remove Success!"); //pesan remove berhasil
                        load_DGV_Order();//tampilkan tabel order yang baru
                        clear_Input_Object_Order();
                    }
                    else MessageBox.Show(bridge.ErrorMessage, "Error");//jika gagal, tampilkan pesan error
                }
            }
        }
        private void tab_Order_btn_Refresh_Click(object sender, EventArgs e)
        {
            load_DGV_Order();
            load_cb_Product_Type_Order();
            load_cb_Product_Name_Order();
        }


        //------tab Purchase-------
        private void tab_Purchase_dgv_Purchase_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            selected_Row_Count = e.RowIndex;
            if (selected_Row_Count >= 0)
            {
                var row = this.tab_Purchase_dgv_Purchase.CurrentRow;
                err = "";
                DateTime date;
                if (!(DateTime.TryParse(row.Cells[1].Value.ToString(), out date))) err += "Error saat parsing date.";
                if (err.Trim() != "") MessageBox.Show(err, "Error");
                else
                {
                    tab_Purchase_dtp_Purchase_Date.Value = date;
                    tab_Purchase_cb_Product_Name.Text = String.Format("{0,5} | {1}", row.Cells[2].Value.ToString(), row.Cells[3].Value.ToString());
                    tab_Purchase_cb_Supplier.Text = row.Cells[4].Value.ToString();
                    tab_Purchase_tb_Number_Received.Text = row.Cells[5].Value.ToString();
                }

            }
        }

        private void tab_Purchase_dgv_Purchase_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            tab_Purchase_dgv_Purchase.ClearSelection();
        }
        private void tab_Purchase_btn_Add_Purchase_Click(object sender, EventArgs e)
        {
            input_Filter_Purchase();
            if (err.Trim() != "") MessageBox.Show(err);
            else
            {
                err = "";
                DateTime date;
                string[] string_product_id;
                int int_number_received, int_product_id;
                string_product_id = tab_Purchase_cb_Product_Name.Text.Replace(" ", "").Split('|');
                if (!(Int32.TryParse(string_product_id[0], out int_product_id))) err += "Error when parsing product id in add order.\n";
                if (!(Int32.TryParse(tab_Purchase_tb_Number_Received.Text, out int_number_received))) err += "Error saat parsing number received.\n";
                if (!(DateTime.TryParse(tab_Purchase_dtp_Purchase_Date.Value.ToString(), out date))) err += "Error saat parsing date.";
                if (err != "") MessageBox.Show("Error saat parsing Date");
                else
                {
                    Purchase purchase = new Purchase
                    {
                        product_id = int_product_id,
                        supplier_id = bridge.Get_Supplier_By_Name(tab_Purchase_cb_Supplier.Text).supplier_id,
                        number_received = int_number_received,
                        purchase_date = date
                    };
                    if (bridge.Add_Purchase(purchase))
                    {
                        MessageBox.Show("Add Success!");
                        load_DGV_Purchase();
                        tab_Purchase_dgv_Purchase.ClearSelection();
                        clear_Input_Object_Purchase();
                    }
                    else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void tab_Purchase_btn_Update_Purchase_Click(object sender, EventArgs e)
        {
            input_Filter_Purchase();
            if (err.Trim() != "") MessageBox.Show(err);
            else
            {
                err = "";
                DateTime date;
                string[] string_product_id;
                int purchase_ID, int_number_received, int_product_id;
                string_product_id = tab_Purchase_cb_Product_Name.Text.Replace(" ", "").Split('|');
                if (!(Int32.TryParse(string_product_id[0], out int_product_id))) err += "Error when parsing product id in add order.\n";
                if (!(Int32.TryParse(tab_Purchase_dgv_Purchase.CurrentRow.Cells[0].Value.ToString(), out purchase_ID))) err += "Error saat parsing purchase id.\n";
                if (!(Int32.TryParse(tab_Purchase_tb_Number_Received.Text, out int_number_received))) err += "Error saat parsing number received.\n";
                if (!(DateTime.TryParse(tab_Purchase_dtp_Purchase_Date.Value.ToString(), out date))) err += "Error saat parsing Date";


                if (err.Trim() != "") MessageBox.Show(err,"Error");
                else
                {
                    Purchase old_Purchase = bridge.Get_Purchase_By_ID(purchase_ID);
                    Purchase new_Purchase = new Purchase
                    {
                        purchase_id = purchase_ID,
                        product_id = int_product_id,
                        supplier_id = bridge.Get_Supplier_By_Name(tab_Purchase_cb_Supplier.Text).supplier_id,
                        number_received = int_number_received,
                        purchase_date = date
                    };
                    if (bridge.Update_Purchase(old_Purchase, new_Purchase))
                    {
                        MessageBox.Show("Edit Success!");
                        load_DGV_Purchase();
                        tab_Purchase_dgv_Purchase.ClearSelection();
                        clear_Input_Object_Purchase();
                    }
                    else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
        private void tab_Purchase_btn_Search_Click(object sender, EventArgs e)
        {
            input_Filter_Search_Purchase();
            if (err != "") MessageBox.Show(err, "Error");
            else
            {

                List<Purchase> purchase_List = bridge.Search_Purchase(tab_Purchase_cb_Number_Received_Filter.SelectedIndex, tab_Purchase_cbox_Search_Purchase_Date.Checked, tab_Purchase_dtp_Search_Purchase_Date.Value, tab_Purchase_tb_Search_Purchase_ID.Text, tab_Purchase_tb_Search_Product_Name.Text, tab_Purchase_cb_Search_Supplier.Text, tab_Purchase_tb_Search_Number_Received.Text);
                if (bridge.QueryStatus)
                {
                    load_DGV_Purchase(purchase_List);
                    tab_Purchase_dgv_Purchase.ClearSelection();
                }
                else MessageBox.Show(bridge.ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tab_Purchase_btn_Refresh_Click(object sender, EventArgs e)
        {
            load_DGV_Purchase();
            load_cb_Product_Name_Purchase();
            load_cb_Supplier_Name_Purchase();
        }
        //menu tool strip
        private void supplier_Tool_Strip_Menu_Item_Click(object sender, EventArgs e)
        {
            using (var form = new Form_Supplier(bridge))
            {
                form.ShowDialog(); //tampilkan form supplier
                load_cb_Supplier_Name_Purchase();
            }
        }

        private void tab_Purchase_btn_Remove_Click(object sender, EventArgs e)
        {
            int int_purchase_id;
            //parsing dan jika berhasil masukkan hasil kedalam variabel, jika tidak masukkan pesan error
            if (!(Int32.TryParse(tab_Purchase_dgv_Purchase.CurrentRow.Cells[0].Value.ToString(), out int_purchase_id))) err += "Failed when parsing order id in Delete Order.\n";

            if (err != "") MessageBox.Show(err, "Error");//jika ada pesan error, tampilkan pesan error
            else //jika tidak ada
            {   //tampilka pesan konfirmasi 
                DialogResult dialogResult = MessageBox.Show("Are you sure you want to remove this product?", "Option", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)//jika pilihan yes
                {
                    if (bridge.Remove_Purchase(int_purchase_id)) //panggl function Remove_Order dari connector, jika berhasil
                    {
                        MessageBox.Show("Remove Success!"); //pesan remove berhasil
                        load_DGV_Purchase();//tampilkan tabel order yang baru
                        clear_Input_Object_Purchase();
                    }
                    else MessageBox.Show(bridge.ErrorMessage, "Error");//jika gagal, tampilkan pesan error
                }
            }
        }

        private void product_Type_Tool_Strip_Menu_Item_Click(object sender, EventArgs e)
        {
            using (var form = new Form_Product_Type(bridge))
            {
                form.ShowDialog(); //tampilkan form product type
                load_cb_Product_Type_Inventory();
                load_cb_Product_Type_Order();
            }
        }
    }
}
