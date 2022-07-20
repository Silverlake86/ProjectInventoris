using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ProjectInventoris
{
    public class Connector
    {

        private SqlConnection _conn = null; //koneksi sql
        public Connector(string con, string db) //menginisiasi class Connector
        {
            QueryStatus = true;
            try
            {
                _conn = new SqlConnection($"Data Source = {con}; Initial Catalog = {db}; Integrated Security = True;"); //detil koneksi
                _conn.Open(); //membuka koneksike database
            }
            catch (Exception ex) //jika ada masalah
            {
                QueryStatus = false; //status query salah
                ErrorMessage = ex.Message; //pesan error
            }

        }

        string err = "";

        public string ErrorMessage { get; set; } // pesan eror

        public bool QueryStatus { get; set; } // status query (berhasil atau tidak)

        private bool result { get; set; } //hasil dalam bool

        private void CheckIsNull(SqlCommand cmd, string parameter, string item) //mengecek apakah kosong atau tidak
        {
            if (String.IsNullOrEmpty(item)) // jika item kosong
            {
                cmd.Parameters.AddWithValue(parameter, DBNull.Value); // maka mengisi query dengan nilai kosong
            }
            else //selain itu
            {
                cmd.Parameters.AddWithValue(parameter, item); // maka mengisi query dengan nilai item
            }
        }
        //----Tabel Produk----
        public int Get_Product_Type_Id(string product_type_name) //mendapatkan id product_type_name
        {
            int result = 0; //inisiasi variabel hasil
            QueryStatus = true; // status query benar
            err = "";
            try
            {
                string sql = $"SELECT [product_type_id] from [InventoryManager].[dbo].[product_type] where product_type_name = '{product_type_name}' and status = 'YES' ";
                using (var cmd = new SqlCommand()) //memulai perintah sql
                {
                    cmd.Connection = _conn; //koneksi adalah koneksi pada class
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah 
                    cmd.CommandText = sql; //query yang akan dijalankan adalah sql //teks perintah adalah isi variabelsql
                    cmd.Parameters.Clear(); // membersihkan sisa sisa parameter sebelumnya
                    using (var reader = cmd.ExecuteReader()) //eksekusi pembaca hasil query
                    {
                        if (reader.HasRows) // jika ada hasilnya
                        {
                            while (reader.Read()) //selama pembaca membaca
                            {
                                //hasil yang didapatkan dari query di ubah menjadi integer
                                if (!(Int32.TryParse(reader["product_type_id"].ToString(), out result))) err += "Error when parsing product type id.";

                                // jika ada error, tampilkan error
                                if (err.Trim() != "")
                                {
                                    MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    QueryStatus = false;
                                }
                                else QueryStatus = true; // status query benar

                            }

                        }
                    }
                }
            }
            catch (Exception ex)// jika ada error
            {
                QueryStatus = false; //status query salah
                ErrorMessage = ex.Message; // pesan error adalah pesan dari error yang didapatkan
            }
            return result; //kembalikan hasil
        }

        public bool Check_Product_Name_Avaibility(string product_name) //mengecek apakah product_name dapat digunakan
        {
            result = false; // hasil adalah salah
            try
            {
                using (var cmd = new SqlCommand()) //memulai perintah sql
                {
                    cmd.Connection = _conn; // koneksi query adalah koneksi yang ada pada class
                    cmd.CommandText = @"SELECT product_name FROM InventoryManager.[dbo].[products] WHERE product_name = @product_name and [status] = 'YES' "; // teks dari perintah yang akan di jalankan
                    cmd.Parameters.Clear(); // membersihkan parameter query sebelumnya
                    cmd.Parameters.AddWithValue("@product_name", product_name); // menambahkan value pada @product_value sesuai dengan nilai variabel product_name
                    using (var reader = cmd.ExecuteReader()) //eksekusi pembaca hasil query
                    {
                        if (reader.HasRows) //jika pernah dipakai
                        {
                            result = false; //hasil adalah nilai salah 
                        }
                        else // jika tidak pernah dipakai
                        {
                            result = true; // hasil adalah benar
                        }
                    }
                }
            }
            catch (Exception ex) //jika error  
            {
                QueryStatus = false; //status query salah
                ErrorMessage = ex.Message; // pesan error adalah pesan dari error yang didapatkan
                result = false;
            }
            return result;
        }


        public Products_Item Get_Product_By_ID(int product_id) //cari product berdasarkan id dari tabel product
        {
            Products_Item result = null; //hasil dalam Producs_Item
            QueryStatus = true; // status query benar
            err = ""; //kosongkan pesan error (untuk parsing)
            try
            {
                string sql = @"SELECT p.[product_id], p.[product_name], pt.[product_type_name], p.[starting_inventory],
                            p.[inventory_received], p.[inventory_shipped], p.[inventory_on_hand], p.[minimum_required]  
                            FROM [InventoryManager].[dbo].[products] p, [InventoryManager].[dbo].[product_type] pt 
                            where p.[product_type_id] = pt.[product_type_id] and p.[product_id] = @product_id and p.status = 'YES' ";
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = _conn;
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah
                    cmd.CommandText = sql; //query yang akan dijalankan adalah sql //query yang akan dijalankan adalah sql
                    cmd.Parameters.Clear(); //bersihkan query sebelumnya
                    CheckIsNull(cmd, "@product_id", product_id.ToString()); //masukkan nilai kedalam query
                    using (var reader = cmd.ExecuteReader()) //jalankan pembaca
                    {
                        if (reader.HasRows) //jika ada hasil
                        {

                            while (reader.Read()) //selama pembaca berjalan
                            {
                                int int_product_id, int_starting_inventory, int_inventory_received, int_inventory_shipped, int_inventory_on_hand, int_minimum_required;
                                //parsing dan masukkan hasil parsing kedalam variabel-variabel, jika gagal, masukkan pesan error
                                if (!(Int32.TryParse(reader["product_id"].ToString(), out int_product_id))) err += "Error when parsing product id.\n";
                                if (!(Int32.TryParse(reader["starting_inventory"].ToString(), out int_starting_inventory))) err += "Error when parsing starting inventory.\n";
                                if (!(Int32.TryParse(reader["inventory_received"].ToString(), out int_inventory_received))) err += "Error when parsing inventory received.\n";
                                if (!(Int32.TryParse(reader["inventory_shipped"].ToString(), out int_inventory_shipped))) err += "Error when parsing inventory shipped.\n";
                                if (!(Int32.TryParse(reader["inventory_on_hand"].ToString(), out int_inventory_on_hand))) err += "Error when parsing inventory on hand.\n";
                                if (!(Int32.TryParse(reader["minimum_required"].ToString(), out int_minimum_required))) err += "Error when parsing minimum required.";
                                //jika ada pesan error setelah parsing, tampilkan pesan error
                                if (err.Trim() != "") MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                result = new Products_Item
                                {   // isi result dengan hasil query
                                    product_id = int_product_id,
                                    product_name = reader["product_name"].ToString(),
                                    product_type_name = reader["product_type_name"].ToString(),
                                    starting_inventory = int_starting_inventory,
                                    inventory_received = int_inventory_received,
                                    inventory_shipped = int_inventory_shipped,
                                    inventory_on_hand = int_inventory_on_hand,
                                    minimum_required = int_minimum_required
                                };
                            }

                        }
                    }
                }

            }
            catch (Exception ex) //jika ada error
            {
                QueryStatus = false; //status query salah
                ErrorMessage = ex.Message; //pesan error
            }

            return result;
        }
        public List<Products_Item> Get_Product_list() //ambil daftar product dari tabel product
        {
            List<Products_Item> result = null; //daftar product
            QueryStatus = true; //status query benar
            err = ""; //pesan error kosong
            try
            {
                //perintah sql yang akan digunakan
                string sql = @"SELECT p.[product_id], p.[product_name], pt.[product_type_name], p.[starting_inventory],
                            p.[inventory_received], p.[inventory_shipped], p.[inventory_on_hand], p.[minimum_required]  
                            FROM [InventoryManager].[dbo].[products] p, [InventoryManager].[dbo].[product_type] pt 
                            where p.[product_type_id] = pt.[product_type_id] and p.[status] = 'YES' ";
                using (var cmd = new SqlCommand()) //mulai perintah sql
                {
                    cmd.Connection = _conn; //koneksi yang akan digunakan
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah
                    cmd.CommandText = sql; //query yang akan dijalankan adalah sql
                    cmd.Parameters.Clear(); //hapus parameter yang digunakan sebelumnya
                    using (var reader = cmd.ExecuteReader()) //mulai pembaca hasil query
                    {
                        if (reader.HasRows) //jika ada hasil
                        {
                            result = new List<Products_Item>();
                            while (reader.Read()) //selama pembaca berjalan
                            {
                                int int_product_id, int_starting_inventory, int_inventory_received, int_inventory_shipped, int_inventory_on_hand, int_minimum_required;
                                
                                //parsing dan jika berhasil masukkan hasil parsing kedalam variabel, jika tidak masukkan pesan error
                                if (!(Int32.TryParse(reader["product_id"].ToString(), out int_product_id))) err += "Error when parsing product id.\n";
                                if (!(Int32.TryParse(reader["starting_inventory"].ToString(), out int_starting_inventory))) err += "Error when parsing starting inventory.\n";
                                if (!(Int32.TryParse(reader["inventory_received"].ToString(), out int_inventory_received))) err += "Error when parsing inventory received.\n";
                                if (!(Int32.TryParse(reader["inventory_shipped"].ToString(), out int_inventory_shipped))) err += "Error when parsing inventory shipped.\n";
                                if (!(Int32.TryParse(reader["inventory_on_hand"].ToString(), out int_inventory_on_hand))) err += "Error when parsing inventory on hand.\n";
                                if (!(Int32.TryParse(reader["minimum_required"].ToString(), out int_minimum_required))) err += "Error when parsing minimum required.";
                                
                                //jika ada pesan error setelah proses parsing, tampilkan pesan error
                                if (err.Trim() != "") MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else//jika tidak ada
                                {
                                    //masukkan hasil query sebagai products_item kedalam result
                                    result.Add(new Products_Item
                                    {
                                        product_id = int_product_id,
                                        product_name = reader["product_name"].ToString(),
                                        product_type_name = reader["product_type_name"].ToString(),
                                        starting_inventory = int_starting_inventory,
                                        inventory_received = int_inventory_received,
                                        inventory_shipped = int_inventory_shipped,
                                        inventory_on_hand = int_inventory_on_hand,
                                        minimum_required = int_minimum_required
                                    });
                                }
                            }
                        }
                    }
                }

            }
            catch (Exception ex) //jika ada error
            {
                QueryStatus = false; //status query salah
                ErrorMessage = ex.Message; //pesan eror
            }

            return result;
        }
        
        public bool Add_Product(Products_Item product) //tambah product baru kedalam tabel product
        {
            QueryStatus = true; //status query benar
            result = false; //hasil dalam boolean
            if (!(Check_Product_Name_Avaibility(product.product_name)))//panggil function Check_Product_Name_Avaibility
            {
                QueryStatus = false;
                result = false;
                ErrorMessage = "Name is already being used.";
            }
            else
            {
                try
                {
                    using (var cmd = new SqlCommand()) //jalankan perintah sql
                    {
                        cmd.Connection = _conn; //konenksi yang akan digunakan
                        //query yang akan digunakan
                        cmd.CommandText = @"insert into [InventoryManager].[dbo].[products] ([product_name], [product_type_id],
			        [starting_inventory],[inventory_received],[inventory_shipped],[inventory_on_hand],[minimum_required], [status]) 
			        values (@product_name, @product_type_id, @starting_inventory, @inventory_received, 
                    @inventory_shipped, @inventory_on_hand, @minimum_required, @status);";
                        cmd.Parameters.Clear(); //bersihkan parameter query sebelumnya
                        CheckIsNull(cmd, "@product_name", product.product_name); //masukkan nilai kedalam query
                        int product_type_id = Get_Product_Type_Id(product.product_type_name); //cari product_type_id berdasakan nama product type
                        if (QueryStatus) //jika berhasil
                        {
                            cmd.Parameters.AddWithValue("@product_type_id", product_type_id.ToString()); //masukkan nilai product_type_id
                        }
                        else MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); //jika gagal tampilkan error
                        
                        //masukkan nilai kedalam query
                        CheckIsNull(cmd, "@starting_inventory", product.starting_inventory.ToString());
                        CheckIsNull(cmd, "@inventory_received", product.inventory_received.ToString());
                        CheckIsNull(cmd, "@inventory_shipped", product.inventory_shipped.ToString());
                        CheckIsNull(cmd, "@inventory_on_hand", product.inventory_on_hand.ToString());
                        CheckIsNull(cmd, "@minimum_required", product.minimum_required.ToString());
                        CheckIsNull(cmd, "@status", "YES");
                        result = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)//jika ada error
                {
                    QueryStatus = false; //status query salah
                    result = false; //result salah
                    ErrorMessage = ex.Message; //pesan error
                }
            }

            return result;
        }

        public bool Update_Product(Products_Item old_product, Products_Item new_product)
        {
            QueryStatus = true;
            result = false;
            //cek apakah nama product dapat digunakan 
            
            //jika dapat digunakan
            if ((Check_Product_Name_Avaibility(new_product.product_name)) || (new_product.product_name == old_product.product_name))
            {
                try
                {
                    using (var cmd = new SqlCommand()) //mulai perintah sql
                    {
                        cmd.Connection = _conn; //koneksi yang digunakan
                        //query yang akan dijalankan
                        cmd.CommandText = @"UPDATE [InventoryManager].[dbo].[products] SET product_name = @product_name, 
                                                                  product_type_id = @product_type_id,
                                                                  starting_inventory = @new_starting_inventory,
                                                                  inventory_on_hand = inventory_on_hand + @new_starting_inventory,
                                                                  minimum_required = @minimum_required
                                         WHERE  product_id = @product_id ;

                                        UPDATE [InventoryManager].[dbo].[products] SET
                                                                  inventory_on_hand = inventory_on_hand - @old_starting_inventory
                                         WHERE  product_id = @product_id";
                        cmd.Parameters.Clear(); //bersihkan parameter query sebelumnya
                        //masukkan value kedalam query
                        cmd.Parameters.AddWithValue("@product_id", new_product.product_id);
                        cmd.Parameters.AddWithValue("@product_name", new_product.product_name);
                        int product_type_id = Get_Product_Type_Id(new_product.product_type_name);
                        if (QueryStatus)
                        {
                            cmd.Parameters.AddWithValue("@product_type_id", product_type_id.ToString());
                        }
                        else MessageBox.Show(ErrorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        cmd.Parameters.AddWithValue("@new_starting_inventory", new_product.starting_inventory.ToString());
                        cmd.Parameters.AddWithValue("@old_starting_inventory", old_product.starting_inventory.ToString());
                        cmd.Parameters.AddWithValue("@minimum_required", new_product.minimum_required.ToString());
                        result = cmd.ExecuteNonQuery() > 0;
                        
                    }
                }
                catch (Exception ex)//jika ada error
                {
                    QueryStatus = true; //status query benar
                    ErrorMessage = ex.Message; //pesan error
                    result = false; //hasil salah
                }
            }

            //jika tidak dapat digunakan
            else
            {
                QueryStatus = false;
                result = false;
                ErrorMessage = "Name is already being used."; //pesan error
            }
            return result;
        }


        //mencari produk sesuai kriteria
        public List<Products_Item> Search_Product(string product_id = "", string product_name = "", string product_type_name = "", string starting_inventory = "", string minimum_required = "")
        {
            err = ""; //pesan error kosong
            QueryStatus = true; //status query benar
            List<Products_Item> result = null; //daftar product item
            //query yang akan digunakan
            string sql = @"SELECT p.[product_id], p.[product_name], pt.[product_type_name], p.[starting_inventory], 
                            p.[inventory_received], p.[inventory_shipped], p.[inventory_on_hand], p.[minimum_required] 
                            FROM [InventoryManager].[dbo].[products] p, [InventoryManager].[dbo].[product_type] pt  
                            where p.[product_type_id] = pt.[product_type_id]  "; 

            //mengecek ada saja yang ingin dicari
            if (product_id.Trim() != "")
            {
                sql += "and p.[product_id] = @product_id ";
            }
            if (product_name.Trim() != "")
            {
                sql += "and p.[product_name] like '%' + @product_name + '%' ";
            }
            if (product_type_name.Trim() != "" && product_type_name.Trim() != "NONE")
            {
                sql += "and pt.[product_type_name] = @product_type_name  ";
            }
            if (starting_inventory.Trim() != "")
            {
                sql += "and p.[starting_inventory] = @starting_inventory ";
            }
            if (minimum_required.Trim() != "")
            {
                sql += "and p.[minimum_required] = @minimum_required";
            }

            try
            {
                using (var cmd = new SqlCommand()) //mulai perintah sql
                {
                    cmd.Connection = _conn;//koneksiyang akan digunakan
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah
                    cmd.CommandText = sql; //query yang akan dijalankan adalah sql
                    cmd.Parameters.Clear(); //bersihkan parameter query sebelumnya

                    //masukkan nilai kedalam query
                    CheckIsNull(cmd, "@product_id", product_id);
                    CheckIsNull(cmd, "@product_name", product_name);
                    CheckIsNull(cmd, "@product_type_name", product_type_name);
                    CheckIsNull(cmd, "@starting_inventory", starting_inventory);
                    CheckIsNull(cmd, "@minimum_required", minimum_required);
                    using (var reader = cmd.ExecuteReader())//jalankan pembaca
                    {
                        result = new List<Products_Item>();
                        while (reader.Read())//selama membaca
                        {
                                
                            int int_product_id, int_starting_inventory, int_inventory_received, int_inventory_shipped, int_inventory_on_hand, int_minimum_required;
                            //parsing hasil baca kedalam variabel-variabel jika berhasil, jika tidak tampilkan error
                            if (!(Int32.TryParse(reader["product_id"].ToString(), out int_product_id))) err += "Error when parsing product id.\n";
                            if (!(Int32.TryParse(reader["starting_inventory"].ToString(), out int_starting_inventory))) err += "Error when parsing starting inventory.\n";
                            if (!(Int32.TryParse(reader["inventory_received"].ToString(), out int_inventory_received))) err += "Error when parsing inventory received.\n";
                            if (!(Int32.TryParse(reader["inventory_shipped"].ToString(), out int_inventory_shipped))) err += "Error when parsing inventory shipped.\n";
                            if (!(Int32.TryParse(reader["inventory_on_hand"].ToString(), out int_inventory_on_hand))) err += "Error when parsing inventory on hand.\n";
                            if (!(Int32.TryParse(reader["minimum_required"].ToString(), out int_minimum_required))) err += "Error when parsing minimum required.";
                                
                            //jika ada error, tampilkan pesan error
                            if (err.Trim() != "") MessageBox.Show(err, "Error");
                            else//jika tidak ada
                            {
                                //tambahkan objek baru kedalam result
                                result.Add(new Products_Item
                                {
                                    product_id = int_product_id,
                                    product_name = reader["product_name"].ToString(),
                                    product_type_name = reader["product_type_name"].ToString(),
                                    starting_inventory = int_starting_inventory,
                                    inventory_received = int_inventory_received,
                                    inventory_shipped = int_inventory_shipped,
                                    inventory_on_hand = int_inventory_on_hand,
                                    minimum_required = int_minimum_required
                                });
                            }
                        }   
                    }
                }
            }
            catch (Exception ex) //jika ada error
            {
                QueryStatus = false; //status quert
                ErrorMessage = ex.Message; //pesan error
            }

            return result;
        }
        
        public bool Remove_Product(int product_id) //menyingkirkan product dari tampilan
        {
            try
            {
                using (var cmd = new SqlCommand()) //mulai perintah sql
                {
                    cmd.Connection = _conn; //koeksi yang digunakan
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah
                    //query yang digunakan
                    cmd.CommandText = @"UPDATE InventoryManager.[dbo].[products] SET [status] = 'NO' WHERE product_id = @product_id and status = 'YES' ; ";
                    cmd.Parameters.Clear(); //hapus parameter query sebelumnya
                    cmd.Parameters.AddWithValue("@product_id", product_id.ToString()); //masukkan nilai kedalam query
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex) //jika ada error
            {
                result = false;
                ErrorMessage = ex.Message;
            }
            return result;
        }
        //sama seperti Remove_Product namun menyingkirkan semua hal yang berhubungan dengan product tersebut jika hirarkinya lebih rendah.
        //Hirarki: Product_Type = Supplier > Product > Order = Purchase
        public bool Remove_Related_To_Product(int product_id)
        { 
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = _conn;
                    cmd.CommandText = @" UPDATE InventoryManager.[dbo].[products] SET status = 'NO' where product_id = @product_id and status = 'YES';
                                        UPDATE InventoryManager.[dbo].[order] SET status = 'NO'  where product_id = @product_id ";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@product_id", product_id.ToString());
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                result = false;
                ErrorMessage = ex.Message;
            }
            return result;
        }


   


        //----Tabel Order----
        public List<Order> Get_Order_List() //mengambil daftar order
        {
            List<Order> result = null;
            QueryStatus = true;
            err = ""; //pesan error
            try
            {
                //perintah sql yang akan digunakan
                string sql = @"SELECT o.[order_id], o.[product_id], p.[product_name], pt.[product_type_name], o.[title],  
                            o.[number_shipped], o.[order_date] FROM [InventoryManager].[dbo].[products] p, 
                            [InventoryManager].[dbo].[order] o, [InventoryManager].[dbo].[product_type] pt
                            where o.[product_id] = p.[product_id] AND p.[product_type_id] = pt.[product_type_id] and o.[status] = 'YES' " ;
                using (var cmd = new SqlCommand()) //mulai perintah sql
                {
                    cmd.Connection = _conn; //koneksi yang digunakan
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah
                    cmd.CommandText = sql; //query yang akan dijalankan adalah sql
                    cmd.Parameters.Clear(); //memberiskan parameter yang digunakan pada sql sebelumnya
                    using (var reader = cmd.ExecuteReader()) //mulai pembaca hasil sql
                    {
                        if (reader.HasRows)//jika ada hasil
                        {
                            result = new List<Order>();
                            while (reader.Read()) //selama memabca
                            {

                                DateTime date;
                                int int_order_id, int_product_id, int_number_shipped;

                                //parsing hasil membaca, jika berhasil maka masukkan kedalam variabel, jika tidak masukkan pesan eror
                                if (!(Int32.TryParse(reader["order_id"].ToString(), out int_order_id))) err += "Error when parsing order id.\n";
                                if (!(Int32.TryParse(reader["product_id"].ToString(), out int_product_id))) err += "Error when parsing product id.\n";
                                if (!(Int32.TryParse(reader["number_shipped"].ToString(), out int_number_shipped))) err += "Error when parsing number shipped.\n";
                                if (!(DateTime.TryParse(reader["order_date"].ToString(), out date))) err += "Error whne parsing date.";
                                
                                //jika ada pesan error, tampilkan pesan error
                                if (err != "") MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else//jika tidak ada
                                {
                                    //masukkan order baru kedalam result
                                    result.Add(new Order
                                    {
                                        order_id = int_order_id,
                                        product_id = int_product_id,
                                        product_name = reader["product_name"].ToString(),
                                        product_type_name = reader["product_type_name"].ToString(),
                                        title = reader["title"].ToString(),
                                        order_date = date,
                                        number_shipped = int_number_shipped
                                    });
                                }

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueryStatus = false;
                ErrorMessage = ex.Message;
            }

            return result;
        }

        public Order Get_Order_By_ID(int order_id) //mengambil order berdasarkan dengan id
        {
            Order result = null;
            QueryStatus = true;
            err = "";
            try
            {
                //query yang ingin dipakai
                string sql = $"Select o.[order_id], o.[product_id], o.[title], o.[number_shipped], o.[order_date] FROM  [InventoryManager].[dbo].[order] o where o.[order_id] = '{order_id}'";
                using (var cmd = new SqlCommand()) //memulai perintah sql
                {
                    cmd.Connection = _conn; //koneksi yang diguakan
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah
                    cmd.CommandText = sql; //query yang akan dijalankan adalah sql
                    cmd.Parameters.Clear(); //membersihkan parameter query sebelumnya
                    using (var reader = cmd.ExecuteReader()) //mulai pembaca hasil query
                    {
                        if (reader.HasRows)//jika ada hasil
                        {
                            while (reader.Read()) //selama membaca
                            {
                                DateTime date;
                                int int_order_id, int_product_id, int_number_shipped;

                                //parsing hasil bacaan. jika berhasil masukkan kedalam variabel, jika gagal masukkan pesan error
                                if (!(Int32.TryParse(reader["order_id"].ToString(), out int_order_id))) err += "Error when parsing order id.\n";
                                if (!(Int32.TryParse(reader["product_id"].ToString(), out int_product_id))) err += "Error when parsing product id.\n";
                                if (!(Int32.TryParse(reader["number_shipped"].ToString(), out int_number_shipped))) err += "Error when parsing number shipped.\n";
                                if (!(DateTime.TryParse(reader["order_date"].ToString(), out date))) err += "Error when parsing date.";
                               //jika ada pesan error tampilka pesan error
                                if (err != "") MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                result = new Order //hasil dalam bentuk order
                                {
                                    order_id = int_order_id,
                                    product_id = int_product_id,
                                    title = reader["title"].ToString(),
                                    order_date = date,
                                    number_shipped = int_number_shipped
                                };
                                QueryStatus = true;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueryStatus = false;
                ErrorMessage = ex.Message;
            }

            return result;
        }

        public bool Add_Order(Order order) //menambahkan order kedalam tabel order
        {
            QueryStatus = true;
            result = false;
            try
            {
                using (var cmd = new SqlCommand()) //mulai perintah sql
                {
                    cmd.Connection = _conn;//koneksi yang digunakan
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah
                    //sql yang akan digunakan
                    cmd.CommandText = @"insert into [InventoryManager].[dbo].[order] ([title], [product_id],
			        [number_shipped],[order_date],[status]) 
			        values (@title, @product_id, @number_shipped, @order_date,@status); 
                    update [InventoryManager].[dbo].[products] SET inventory_shipped = inventory_shipped + @number_shipped, 
                    inventory_on_hand = inventory_on_hand - @number_shipped where product_id = @product_id;
                    ";
                    cmd.Parameters.Clear();//bersihkan parameter sebelumnya
                    //masukkan nilai kedalam sql
                    CheckIsNull(cmd, "@title", order.title);
                    CheckIsNull(cmd, "@product_id", order.product_id.ToString());
                    CheckIsNull(cmd, "@number_shipped", order.number_shipped.ToString());
                    CheckIsNull(cmd, "@order_date", order.order_date.ToString("MM/dd/yyyy"));
                    CheckIsNull(cmd, "@status", "YES");
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                result = false;
                QueryStatus = false;
                ErrorMessage = ex.Message;
            }
            return result;
        }

        public bool Update_Order(Order old_Order, Order new_Order) //edit order berdasarkan dengan kriteria(id) yang ada
        {
            QueryStatus = true;
            result = false;
            try
            {
                using (var cmd = new SqlCommand()) //mulai perintah sql
                {
                    cmd.Connection = _conn;//koneksi yang digunakan
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah
                    //query yang akan digunakan
                    cmd.CommandText = @"UPDATE [InventoryManager].[dbo].[order] SET 
                                                                  [order_date] = @order_date_new, 
                                                                  [product_id] = @product_id_new,
                                                                  [number_shipped] = @number_shipped_new,
                                                                  [title] = @title_new 
                                                                  WHERE  order_id = @order_id and status = 'YES' ;
                                      UPDATE [InventoryManager].[dbo].[products] SET 
                                                                  inventory_shipped = inventory_shipped - @number_shipped_old,
                                                                  inventory_on_hand = inventory_on_hand + @number_shipped_old   
                                                                  WHERE  product_id = @product_id_old ;
                                      UPDATE [InventoryManager].[dbo].[products] SET 
                                                                  inventory_shipped = inventory_shipped + @number_shipped_new,
                                                                  inventory_on_hand = inventory_on_hand - @number_shipped_new   
                                                                  WHERE  product_id = @product_id_new ;";
                    cmd.Parameters.Clear();//bersihkan parameter query sebelumnya

                    //masukkan nilai kedalam query
                    cmd.Parameters.AddWithValue("@product_id_old", old_Order.product_id.ToString());
                    cmd.Parameters.AddWithValue("@number_shipped_old", old_Order.number_shipped.ToString());

                    cmd.Parameters.AddWithValue("@product_id_new", new_Order.product_id.ToString());
                    cmd.Parameters.AddWithValue("@number_shipped_new", new_Order.number_shipped.ToString());

                    cmd.Parameters.AddWithValue("@order_id", new_Order.order_id.ToString());
                    cmd.Parameters.AddWithValue("@order_date_new", new_Order.order_date.ToString("MM/dd/yyyy"));
                    cmd.Parameters.AddWithValue("@title_new", new_Order.title);

                    cmd.ExecuteNonQuery();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                QueryStatus = false;
                result = false;
                ErrorMessage = ex.Message;
            }

            return result;
        }
        public bool Remove_Order(int order_id) //hapus order sesuai dengan id
        {
            try
            {
                using (var cmd = new SqlCommand()) //mulai perintah sql
                {
                    cmd.Connection = _conn;//koneksi yang digunakan
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah
                    //query yang akan digunakan
                    cmd.CommandText = @" UPDATE InventoryManager.[dbo].[order] SET status = 'NO' 
                                        where order_id = @order_id and status = 'YES' ";

                    cmd.Parameters.Clear();//bersihkan parameter query sebelumnya
                    cmd.Parameters.AddWithValue("@order_id", order_id.ToString()); //masukkan nilai kedalam query
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                result = false;
                ErrorMessage = ex.Message;
            }
            return result;
        }
        //mencari order berdasarkan kriteria
        public List<Order> Search_Order(int param, bool bool_date, DateTime search_Date, string order_id = "", string product_name = "", string product_type_name = "", string title = "", string number_shipped = "")
        {
            QueryStatus = true;
            List<Order> result = null;
            err = "";
            //query yang akan digunakan
            string sql = @"select o.[order_id], o.[product_id], p.[product_name], pt.[product_type_name], o.[title],  
                            o.[number_shipped], o.[order_date] from [inventorymanager].[dbo].[products] p, 
                            [inventorymanager].[dbo].[order] o, [inventorymanager].[dbo].[product_type] pt
                            where o.[product_id] = p.[product_id] and p.[product_type_id] = pt.[product_type_id] and o.status = 'YES' ";

            //memastikan apa saja yang ingin dicari
            if (order_id.Trim() != "")
            {
                sql += "and o.[order_id] = @order_id ";
            }
            if (product_name.Trim() != "")
            {
                sql += "and p.[product_name] like '%' + @product_name + '%' ";
            }
            if (product_type_name.Trim() != "" && product_type_name != "NONE")
            {
                sql += "and pt.[product_type_name] = @product_type_name ";
            }
            if (title.Trim() != "")
            {
                sql += "and o.[title] like '%' + @title + '%' ";
            }
            if (number_shipped.Trim() != "")
            {
                if (param == 0)
                {
                    sql += "and o.[number_shipped] < @number_shipped";
                }
                else if (param == 1)
                {
                    sql += "and o.[number_shipped] = @number_shipped";
                }
                else if (param == 2)
                {
                    sql += "and o.[number_shipped] > @number_shipped";
                }
            }

            if (bool_date)
            {
                sql += "and o.[order_date] = @order_date";
            }


            try
            {
                using (var cmd = new SqlCommand()) //mulai perintah sql
                {
                    cmd.Connection = _conn; //koneksi yang akan digunakan
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah
                    cmd.CommandText = sql; //query yang akan dijalankan adalah sql
                    cmd.Parameters.Clear(); //bersihkan parameter query sebelumnya

                    //masukkan nilai kedalam query
                    CheckIsNull(cmd, "@order_id", order_id);
                    CheckIsNull(cmd, "@product_name", product_name);
                    CheckIsNull(cmd, "@product_type_name", product_type_name);
                    CheckIsNull(cmd, "@title", title);
                    CheckIsNull(cmd, "@number_shipped", number_shipped);
                    CheckIsNull(cmd, "@order_date", search_Date.ToString("MM/dd/yyyy"));
                    using (var reader = cmd.ExecuteReader()) //mulai pembaca hasil query
                    {
                        result = new List<Order>();
                        while (reader.Read())//selama membaca
                        {
                            DateTime date;
                            int int_order_id, int_product_id, int_number_shipped;

                            //parsing hasil bacaan, jika berhasil masukkan kedalam variabel, jika tidak masukkan pesan error
                            if (!(Int32.TryParse(reader["order_id"].ToString(), out int_order_id))) err += "Error when parsing order id.\n";
                            if (!(Int32.TryParse(reader["product_id"].ToString(), out int_product_id))) err += "Error when parsing product id.\n";
                            if (!(Int32.TryParse(reader["number_shipped"].ToString(), out int_number_shipped))) err += "Error when parsing number shipped.\n";
                            if (!(DateTime.TryParse(reader["order_date"].ToString(), out date))) err += "Error when parsing date.";
                            //jika ada error, tampilkan pesan error
                            if (err.Trim() != "") MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else //jika tidak ada
                            {//masukkan order kedalam result
                                result.Add(new Order
                                {
                                    order_id = int_order_id,
                                    product_id = int_product_id,
                                    product_name = reader["product_name"].ToString(),
                                    product_type_name = reader["product_type_name"].ToString(),
                                    title = reader["title"].ToString(),
                                    order_date = date,
                                    number_shipped = int_number_shipped
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueryStatus = false;
                ErrorMessage = ex.Message;
            }

            return result;
        }
        
        //---------table Product_type-------
        public List<Product_Item_Type> Get_Product_Type_List() //mendapatkan daftar tipe produk dari database
        {
            List<Product_Item_Type> result = null; //daftar hasil sql adalah kosong
            QueryStatus = true; //status query adalah benar
            err = "";
            try
            {
                string sql = @"SELECT [product_type_id], [product_type_name] from [InventoryManager].[dbo].[product_type] where status = 'YES' "; //teks perintah sql yang akan dipakai
                using (var cmd = new SqlCommand()) // inisiasi perintah sql query
                {
                    cmd.Connection = _conn;  // koneksi query adalah koneksi yang ada pada class
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah 
                    cmd.CommandText = sql; //query yang akan dijalankan adalah sql //perintah query yang akan digunakan adalah isi dari variabel sql
                    cmd.Parameters.Clear();// membersihkan parameter query sebelumnya
                    using (var reader = cmd.ExecuteReader()) //eksekusi pembaca hasil query
                    {
                        if (reader.HasRows) //jika ada hasil 
                        {
                            result = new List<Product_Item_Type>(); // inisialsi result sebagai list dari class Product_Item_Type
                            while (reader.Read()) //selama membaca 
                            {
                                int int_product_type_id;
                                //parsing
                                if (!(Int32.TryParse(reader["product_type_id"].ToString(), out int_product_type_id))) err += "Error when parsing product type id.";
                                //jika ada error, tampilkan pesan error
                                if (err.Trim() != "") MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else//jika tidak ada
                                {//masukkan product_item_type kedalam result
                                    result.Add(new Product_Item_Type
                                    {
                                        product_type_id = int_product_type_id,
                                        product_type_name = reader["product_type_name"].ToString(),
                                    }); //masukkan hasil nilai query class Product_Item_Type yang baru kedalam daftar
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueryStatus = false; //status query salah
                ErrorMessage = ex.Message; // pesan error adalah pesan dari error yang didapatkan
            }

            return result;

        }

        public bool Check_Product_Type_Name_Avaibility(string product_type_name) //mengecek apakah product_type_name dapat digunakan
        {
            bool result;
            try
            {
                using (var cmd = new SqlCommand()) //memulai perintah sql
                {
                    cmd.Connection = _conn; // koneksi query adalah koneksi yang ada pada class
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah 
                    cmd.CommandText = @"SELECT product_type_name FROM InventoryManager.[dbo].[product_type] WHERE product_type_name = @product_type_name AND status = 'YES' " ; // teks dari perintah yang akan di jalankan
                    cmd.Parameters.Clear(); // membersihkan parameter query sebelumnya
                    cmd.Parameters.AddWithValue("@product_type_name", product_type_name); // menambahkan value pada @product_value sesuai dengan nilai variabel product_name
                    using (var reader = cmd.ExecuteReader()) //eksekusi pembaca hasil query
                    {
                        if (reader.HasRows) //jika pernah dipakai
                        {
                           result = false; //hasil adalah nilai salah 
                        }
                        else // jika tidak pernah dipakai
                        {
                            result = true; // hasil adalah benar
                        }
                    }
                }
            }
            catch (Exception ex) //jika error  
            {
                QueryStatus = false; //status query salah
                ErrorMessage = ex.Message; // pesan error adalah pesan dari error yang didapatkan
                result = false;
            }
            return result;
        }
        public bool Add_Product_Type(Product_Item_Type product_type) //menambah product_type ke dalam tabel product_type
        {
            QueryStatus = true;
            result = false;
            //cek apakah nama dapat digunakan
            //jika tidak dapat digunakan
            if (!(Check_Product_Type_Name_Avaibility(product_type.product_type_name)))
            {
                QueryStatus = false;
                result = false;
                ErrorMessage = "Name is already being used.";

            }
            else //jika dapat digunakan
            {
                try
                {
                    using (var cmd = new SqlCommand()) //mulai perintah sql
                    {
                        cmd.Connection = _conn;//koneksi yang akan digunakan
                        cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah 
                        //query yang akan digunakan
                        cmd.CommandText = @"insert into [InventoryManager].[dbo].[product_type] ([product_type_name], [status]) 
                                        values(@product_type_name, @status)";
                        cmd.Parameters.Clear();//bersihkan parameter query sebelumnya
                        //masukkan nilai kedalam query
                        CheckIsNull(cmd, "@product_type_name", product_type.product_type_name);
                        CheckIsNull(cmd, "@status", "YES");
                        result = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    QueryStatus = false;
                    result = false;
                    ErrorMessage = ex.Message;
                }
            }

            return result;
        }

        public bool Update_Product_Type(Product_Item_Type product_type, string old_name) //edit product_type berdasarkan kriteria
        {
            QueryStatus = true;
            result = false;
            //mengecek apakah nama product_type dapat digunakan
            //jika dapat digunakan
            if (Check_Product_Type_Name_Avaibility(product_type.product_type_name) || (product_type.product_type_name == old_name))
            {
                try
                {
                    using (var cmd = new SqlCommand()) //mulai perintah sql
                    {
                        cmd.Connection = _conn; //koneksi yang akan digunakan
                        cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah 
                        //query yang akan digunakan
                        cmd.CommandText = @"update [InventoryManager].[dbo].[product_type] SET product_type_name = @product_type_name where product_type_id = @product_type_id;";
                        cmd.Parameters.Clear(); //hapus parameter query sebelumnya

                        //masukkan value kedalam query
                        cmd.Parameters.AddWithValue("@product_type_id", product_type.product_type_id.ToString());
                        cmd.Parameters.AddWithValue("@product_type_name", product_type.product_type_name);
                        result = cmd.ExecuteNonQuery() > 0;
                    }
                }
                catch (Exception ex)
                {
                    QueryStatus = false;
                    result = false;
                    ErrorMessage = ex.Message;
                }
            }
            //jika tidak
            else
            {
                QueryStatus = false;
                result = false;
                ErrorMessage = "Name is already being used.";
            }
            return result;
        }
        //cari product_type berdasarkan kriteria
        public List<Product_Item_Type> Search_Product_Type(string product_type_id = "" , string product_type_name =""){
            QueryStatus = true;
            List<Product_Item_Type> result = null;
            err = "";
            //query yang akan digunakan
            string sql = @"select [product_type_id], [product_type_name] from [inventorymanager].[dbo].[product_type] where status = 'YES'";
            //cek kriteria apa saja yang akan digunakan
            if (product_type_id.Trim() != "")
            {
                sql += "and [product_type_id] = @product_type_id ";
            }
            if (product_type_name.Trim() != "")
            {
                sql += "and [product_type_name] like '%' + @product_type_name + '%' ";
            }


            try
            {
                using (var cmd = new SqlCommand()) //jalankan perintah sql
                {
                    cmd.Connection = _conn; //koneksi yang akan digunakan
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah
                    cmd.CommandText = sql; //query yang akan dijalankan adalah sql
                    cmd.Parameters.Clear();//hapus parameter query sebelumnya

                    //masukkan nilai kedalam query
                    CheckIsNull(cmd, "@product_type_id", product_type_id);
                    CheckIsNull(cmd, "@product_type_name", product_type_name);
                    using (var reader = cmd.ExecuteReader()) //jalankan pembaca hasil query
                    {
                        result = new List<Product_Item_Type>();
                        while (reader.Read()) //selama pembaca berjalan
                        {
                            int int_order_id;
                            //parsing..
                            if (!(Int32.TryParse(reader["product_type_id"].ToString(), out int_order_id))) err += "Error when parsing product type id.\n";
                                
                            //jika ada pesan error, tampilkan pesan error
                            if (err.Trim() != "") MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else //jika tidak ada
                            {//masukkan product_item_type kedalam result
                                result.Add(new Product_Item_Type
                                {
                                    product_type_id = int_order_id,
                                    product_type_name = reader["product_type_name"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueryStatus = false;
                ErrorMessage = ex.Message;
            }

            return result;
        }
        //menyinkirkan product_type dari tampilan datagridview           
        public bool Remove_Product_Type(int product_type_id)
        {
            try
            {
                using (var cmd = new SqlCommand()) //mulai perintah sql
                {
                    cmd.Connection = _conn;//koneksi yang akan digunakan
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah
                    //query yang akan digunakan
                    cmd.CommandText = @"UPDATE InventoryManager.[dbo].[product_type] SET status = 'NO' WHERE product_type_id = @product_type_id and status = 'YES'; ";
                    cmd.Parameters.Clear();//bersihkan parameter query sebelumnya
                    //masukkan nilai kedalam query
                    cmd.Parameters.AddWithValue("@product_type_id", product_type_id.ToString());
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                result = false;
                ErrorMessage = ex.Message;
            }
            return result;
        }
        //sama seperti Remove_Product_Type namun menghapus semua yang hal yang berhubungan dengan Product_Type yang dihapus jika hirarkinya lebih rendah
        //Hirarki: Product_Type = Supplier > Product > Order = Purchase
        public bool Remove_Related_To_Product_Type(int product_type_id)
        {
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = _conn;
                    cmd.CommandText = @"UPDATE InventoryManager.[dbo].[product_type] SET status = 'NO' WHERE product_type_id = @product_type_id;
                                        UPDATE InventoryManager.[dbo].[products] SET status = 'NO' where product_type_id = @product_type_id;
                                        UPDATE o SET status = 'NO' from InventoryManager.[dbo].[order] o, InventoryManager.[dbo].[products] p
                                        where p.product_type_id = @product_type_id and o.product_id = p.product_id ";
                                    
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@product_type_id", product_type_id.ToString());
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                result = false;
                ErrorMessage = ex.Message;
            }
            return result;
        }
    
        //------Tabel Supplier-------
        public List<Supplier> Get_Supplier_List() //mengambil daftar supplier dari tabel supplieir
        {
            List<Supplier> result = null;
            err = "";
            try
            {
                //query yang akan digunakan
                string sql = @"SELECT [supplier_id], [supplier_name], [supplier_address] from [InventoryManager].[dbo].[supplier] where status = 'YES' "; //teks perintah sql yang akan dipakai
                using (var cmd = new SqlCommand()) // inisiasi perintah sql query
                {
                    cmd.Connection = _conn;  // koneksi query adalah koneksi yang ada pada class
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah 
                    cmd.CommandText = sql; //query yang akan dijalankan adalah sql //perintah query yang akan digunakan adalah isi dari variabel sql
                    cmd.Parameters.Clear();// membersihkan parameter query sebelumnya
                    using (var reader = cmd.ExecuteReader()) //eksekusi pembaca hasil query
                    {
                        if (reader.HasRows) //jika ada hasil 
                        {
                            result = new List<Supplier>(); // inisialsi result sebagai list dari class Product_Item_Type
                            while (reader.Read()) //selama membaca 
                            {
                                int int_supplier_id;
                                //parsing....
                                if (!(Int32.TryParse(reader["supplier_id"].ToString(), out int_supplier_id))) err += "Error when parsing supplier id.";
                                //jika ada error, tampilkan error
                                if (err.Trim() != "") MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else //jika tidak ada
                                {
                                    result.Add(new Supplier //masukkan supplier kedalam result
                                    {
                                        supplier_id = int_supplier_id,
                                        supplier_name = reader["supplier_name"].ToString(),
                                        supplier_address = reader["supplier_address"].ToString()
                                    }); 
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueryStatus = false; //status query salah
                ErrorMessage = ex.Message; // pesan error adalah pesan dari error yang didapatkan
            }

            return result;
        }

        public bool Add_Supplier(Supplier supplier) //menambahkan supplier baru kedalam tabel supplier
        {
            result = false; 
            try
            {
                using (var cmd = new SqlCommand()) //mulai perintah sql
                {
                    cmd.Connection = _conn;//koneksi yang digunakan
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah 
                    //query yang digunakan
                    cmd.CommandText = @"insert into [InventoryManager].[dbo].[supplier] ([supplier_name],
			        [supplier_address],[status]) 
			        values ( @supplier_name, @supplier_address, @status);";
                    cmd.Parameters.Clear(); //menghapus parameter query sebelumnya


                    //memasukkan nilai kedalam query
                    CheckIsNull(cmd, "@supplier_name", supplier.supplier_name);
                    CheckIsNull(cmd, "@supplier_address", supplier.supplier_address);
                    CheckIsNull(cmd, "@status", "YES");
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                result = false;
                QueryStatus = false;
                ErrorMessage = ex.Message;
            }
            return result;
        }
    
        public bool Update_Supplier(Supplier supplier) // mengedit supplier dari tabel supplier berdasarkan kriteria id supplier
        {
            QueryStatus = true;
            result = false;
            try
            {
                using (var cmd = new SqlCommand()) //mulai perintah sql
                {
                    cmd.Connection = _conn;
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah 
                    //query yang digunakan
                    cmd.CommandText = @"update [InventoryManager].[dbo].[supplier] SET supplier_name = @supplier_name,
                                        supplier_address = @supplier_address  where supplier_id = @supplier_id and STATUS = 'YES';";
                                    
                    cmd.Parameters.Clear();//bersihkan parameter query sebelumnya

                    //masukkan nilai kedalam query
                    cmd.Parameters.AddWithValue("@supplier_id", supplier.supplier_id);
                    cmd.Parameters.AddWithValue("@supplier_name", supplier.supplier_name);
                    cmd.Parameters.AddWithValue("@supplier_address", supplier.supplier_address);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                QueryStatus = false;
                result = false;
                ErrorMessage = ex.Message;
            }
            

            return result;
        }

        public bool Remove_Supplier(int supplier_id) //menyingkirkan supplier dari tampilan
        {
            QueryStatus = true;
            result = false;
            try
            {
                using (var cmd = new SqlCommand()) //mulai perintah sql
                {
                    cmd.Connection = _conn; //koneksi yang digunakan
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah 
                    //query yang digunakan
                    cmd.CommandText = @"update [InventoryManager].[dbo].[supplier] SET status = 'NO' where supplier_id = @supplier_id and status = 'YES' ;";    
                    cmd.Parameters.Clear();//memberishkan parameter query sebelumnya
                    //tambahkan nilai kedalam query
                    cmd.Parameters.AddWithValue("@supplier_id",supplier_id);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                QueryStatus = false;
                result = false;
                ErrorMessage = ex.Message;
            }
            

            return result;
        }


        //sama seperti Remove_Supplier namun menghapus semua hal yang berhubungan dengan supplier yang dihapus jika hirarkinya lebih rendah
        //Hirarki: Product_Type = Supplier > Product > Order = Purchase
        public bool Remove_Related_To_Supplier(int supplier_id)
        {
            QueryStatus = true;
            result = false;
            try
            {
                using (var cmd = new SqlCommand()) //mulai perintah sql
                {
                    cmd.Connection = _conn; //koneksi yang digunakan
                    cmd.CommandText = @"update [InventoryManager].[dbo].[supplier] SET status = 'NO' where supplier_id = @supplier_id and status = 'YES';
                                        update p SET p.[inventory_received] = p.[inventory_received] - pr.[number_received], 
                                           p.[inventory_on_hand] = p.[inventory_on_hand] - pr.[number_received] from 
                                            [InventoryManager].[dbo].[products] p,
                                            [InventoryManager].[dbo].[purchase] pr where pr.[supplier_id] = @supplier_id;                                         
                                        update [InventoryManager].[dbo].[purchase] SET status = 'NO' where 
                                            supplier_id = @supplier_id and status = 'YES';";
                    cmd.Parameters.Clear(); //bersihkan parameter query sebelunya
                    cmd.Parameters.AddWithValue("@supplier_id", supplier_id); //masukkan nilai kedalam query
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                QueryStatus = false;
                result = false;
                ErrorMessage = ex.Message;
            }


            return result;
        }

        public List<Supplier> Search_Supplier(string supplier_id = "", string supplier_name = "", string supplier_address = "")
        {
            QueryStatus = true;
            List<Supplier> result = null;
            err = "";
            //query yang akan digunakan
            string sql = @"select [supplier_id], [supplier_name], [supplier_address] from [inventorymanager].[dbo].[supplier] where status = 'YES'";
            //cek kriteria apa saja yang akan digunakan
            if (supplier_id.Trim() != "")
            {
                sql += "and [supplier_id] = @supplier_id ";
            }
            if (supplier_name.Trim() != "")
            {
                sql += "and [supplier_name] like '%' + @supplier_name + '%' ";
            }
            if (supplier_address.Trim() != "")
            {
                sql += "and [supplier_address] like '%' + @supplier_address + '%' ";
            }


            try
            {
                using (var cmd = new SqlCommand()) //jalankan perintah sql
                {
                    cmd.Connection = _conn; //koneksi yang akan digunakan
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah
                    cmd.CommandText = sql; //query yang akan dijalankan adalah sql
                    cmd.Parameters.Clear();//hapus parameter query sebelumnya

                    //masukkan nilai kedalam query
                    CheckIsNull(cmd, "@supplier_id", supplier_id);
                    CheckIsNull(cmd, "@supplier_name", supplier_name);
                    CheckIsNull(cmd, "@supplier_address", supplier_address);
                    using (var reader = cmd.ExecuteReader()) //jalankan pembaca hasil query
                    {
                        result = new List<Supplier>();
                        while (reader.Read()) //selama pembaca berjalan
                        {
                            int int_supplier_id;
                            //parsing..
                            if (!(Int32.TryParse(reader["supplier_id"].ToString(), out int_supplier_id))) err += "Error when parsing supplier id.\n";

                            //jika ada pesan error, tampilkan pesan error
                            if (err.Trim() != "") MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else //jika tidak ada
                            {//masukkan product_item_type kedalam result
                                result.Add(new Supplier
                                {
                                    supplier_id = int_supplier_id,
                                    supplier_name = reader["supplier_name"].ToString(),
                                    supplier_address = reader["supplier_address"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueryStatus = false;
                ErrorMessage = ex.Message;
            }

            return result;
        }

        public Supplier Get_Supplier_By_Name(string supplier_name)
        {
            Supplier result = null;
            QueryStatus = true;
            err = "";
            try
            {
                string sql = @"SELECT s.[supplier_id], s.[supplier_name], s.[supplier_address]  
                            FROM [InventoryManager].[dbo].[supplier]s WHERE supplier_name = @supplier_name";
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = _conn;  // koneksi query adalah koneksi yang ada pada class
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah
                    cmd.CommandText = sql; //perintah query yang akan digunakan adalah isi dari variabel sql
                    cmd.Parameters.Clear();// membersihkan parameter query sebelumnya
                    CheckIsNull(cmd, "@supplier_name", supplier_name);
                    using (var reader = cmd.ExecuteReader()) //eksekusi pembaca hasil query
                    {
                        if (reader.HasRows) //jika ada hasil
                        {
                            while (reader.Read()) //selama membaca
                            {
                                int int_supplier_id;
                                if (!(Int32.TryParse(reader["supplier_id"].ToString(), out int_supplier_id))) err += "Error when parsing supplier id.\n";
                                if (err.Trim() != "") MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                result = new Supplier
                                {
                                    supplier_id = int_supplier_id,
                                    supplier_name = reader["supplier_name"].ToString(),
                                    supplier_address = reader["supplier_address"].ToString()
                                };//masukkan hasil nilai query  kedalam class Product_Item_Type yang baru
                            }
                        }
                    }
                }
            }
            catch (Exception ex) //jika ada error
            {
                QueryStatus = false; //status query salah
                ErrorMessage = ex.Message; // pesan error adalah pesan dari error yang didapatkan
            }

            return result; //kembalikan hasil
        }
        //------table Purchase
        public List<Purchase> Get_Purchase_List()
        {
            List<Purchase> result = null;
            QueryStatus = true;
            err = "";
            try
            {
                string sql = @"SELECT pc.[purchase_id], pc.[supplier_id], s.[supplier_name], pc.[product_id], p.[product_name], 
                             pt.[product_type_name], pc.[number_received], pc.[purchase_date] FROM [InventoryManager].[dbo].[products] p, 
                             [InventoryManager].[dbo].[purchase] pc, [InventoryManager].[dbo].[product_type] pt,
                             [InventoryManager].[dbo].[supplier] s
                             where pc.[product_id] = p.[product_id] and p.[product_type_id] = pt.[product_type_id] and
                             pc.[supplier_id] = s.[supplier_id] and pc.status ='YES' ";
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = _conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            result = new List<Purchase>();
                            while (reader.Read())
                            {

                                DateTime date;
                                int int_purchase_id, int_product_id, int_supplier_id, int_number_received;
                                if (!(Int32.TryParse(reader["purchase_id"].ToString(), out int_purchase_id))) err += "Error when parsing purchase id.\n";
                                if (!(Int32.TryParse(reader["product_id"].ToString(), out int_product_id))) err += "Error when parsing product id.\n";
                                if (!(Int32.TryParse(reader["supplier_id"].ToString(), out int_supplier_id))) err += "Error when parsing supplier id.\n";
                                if (!(Int32.TryParse(reader["number_received"].ToString(), out int_number_received))) err += "Error when parsing number received.\n";
                                if (!(DateTime.TryParse(reader["purchase_date"].ToString(), out date))) err += "Error when parsing purchase date.";
                                if (err != "") MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                else
                                {
                                    result.Add(new Purchase
                                    {
                                        purchase_id = int_purchase_id,
                                        product_id = int_product_id,
                                        supplier_id = int_supplier_id,
                                        product_name = reader["product_name"].ToString(),
                                        supplier_name = reader["supplier_name"].ToString(),
                                        purchase_date = date,
                                        number_received = int_number_received
                                    });
                                }

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                QueryStatus = false;
            }

            return result;
        }
        public bool Add_Purchase(Purchase purchase)
        {
            bool result = false;
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = _conn;
                    cmd.CommandText = @"insert into [InventoryManager].[dbo].[purchase] ([product_id],
			        [supplier_id], [number_received], [purchase_date], [status]) 
			        values (@product_id, @supplier_id, @number_received, @purchase_date, @status);
                    update [InventoryManager].[dbo].[products] SET inventory_received = inventory_received + @number_received, 
                    inventory_on_hand = inventory_on_hand + @number_received where product_id = @product_id;";
                    cmd.Parameters.Clear();
                    CheckIsNull(cmd, "@product_id", purchase.product_id.ToString());
                    CheckIsNull(cmd, "@supplier_id", purchase.supplier_id.ToString());
                    CheckIsNull(cmd, "@number_received", purchase.number_received.ToString());
                    CheckIsNull(cmd, "@purchase_date", purchase.purchase_date.ToString("MM/dd/yyyy"));
                    CheckIsNull(cmd, "@status", "YES");
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            return result;
        }

        public Purchase Get_Purchase_By_ID(int purchase_id)
        {
            Purchase result = null;
            QueryStatus = true;
            err = "";
            try
            {
                string sql = $"Select pc.[purchase_id], pc.[product_id], pc.[supplier_id], pc.[number_received], pc.[purchase_date] FROM  [InventoryManager].[dbo].[purchase] pc where pc.[purchase_id] = '{purchase_id}'";
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = _conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                DateTime date;
                                int int_purchase_id, int_product_id, int_supplier_id, int_number_received;
                                if (!(Int32.TryParse(reader["purchase_id"].ToString(), out int_purchase_id))) err += "Error when parsing purchase id.\n";
                                if (!(Int32.TryParse(reader["product_id"].ToString(), out int_product_id))) err += "Error when parsing product id.\n";
                                if (!(Int32.TryParse(reader["supplier_id"].ToString(), out int_supplier_id))) err += "Error when parsing supplier id.\n";
                                if (!(Int32.TryParse(reader["number_received"].ToString(), out int_number_received))) err += "Error when parsing number received.\n";
                                if (!(DateTime.TryParse(reader["purchase_date"].ToString(), out date))) err += "Error when parsing purchase date.";
                                if (err != "") MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                result = new Purchase
                                {
                                    purchase_id = int_purchase_id,
                                    product_id = int_product_id,
                                    supplier_id = int_supplier_id,
                                    purchase_date = date,
                                    number_received = int_number_received
                                };
                                QueryStatus = true;
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueryStatus = false;
                ErrorMessage = ex.Message;
            }

            return result;
        }

        public bool Update_Purchase(Purchase old_Purchase, Purchase new_Purchase) //mengubah purchase yang ada
        {
            bool result = false;
            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = _conn;
                    cmd.CommandText = @"UPDATE [InventoryManager].[dbo].[purchase] SET 
                                                                  [purchase_date] = @purchase_date_new, 
                                                                  [product_id] = @product_id_new,
                                                                  [supplier_id] = @supplier_id_new,
                                                                  [number_received] = @number_received_new
                                                                  WHERE  purchase_id = @purchase_id;
                                      UPDATE [InventoryManager].[dbo].[products] SET 
                                                                  inventory_received = inventory_received - @number_received_old,
                                                                  inventory_on_hand = inventory_on_hand - @number_received_old   
                                                                  WHERE  product_id = @product_id_old ;
                                      UPDATE [InventoryManager].[dbo].[products] SET 
                                                                  inventory_received = inventory_received + @number_received_new,
                                                                  inventory_on_hand = inventory_on_hand + @number_received_new   
                                                                  WHERE  product_id = @product_id_new ;";

                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@product_id_old", old_Purchase.product_id.ToString());
                    cmd.Parameters.AddWithValue("@number_received_old", old_Purchase.number_received.ToString());

                    cmd.Parameters.AddWithValue("@product_id_new", new_Purchase.product_id.ToString());
                    cmd.Parameters.AddWithValue("@number_received_new", new_Purchase.number_received.ToString());

                    cmd.Parameters.AddWithValue("@purchase_id", new_Purchase.purchase_id.ToString());
                    cmd.Parameters.AddWithValue("@supplier_id_new", new_Purchase.supplier_id.ToString());
                    cmd.Parameters.AddWithValue("@purchase_date_new", new_Purchase.purchase_date.ToString("MM/dd/yyyy"));

                    cmd.ExecuteNonQuery();
                    result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                result = false;
            }

            return result;
        }


        //mengambil daftar purchase yang memenuhi kriteria dari tabel purchase
        public List<Purchase> Search_Purchase(int param, bool bool_date, DateTime search_Date, string purchase_id = "", string product_name = "", string supplier_name = "", string number_received = "")
        {
            QueryStatus = true;
            List<Purchase> result = null;
            err = "";
            string sql = @"select pc.[purchase_id], pc.[product_id], p.[product_name], pc.[supplier_id], s.[supplier_name],  
                            pc.[number_received], pc.[purchase_date] from [inventorymanager].[dbo].[products] p, 
                            [inventorymanager].[dbo].[purchase] pc, [inventorymanager].[dbo].[supplier] s, [inventorymanager].[dbo].[product_type] pt 
                             where pc.[product_id] = p.[product_id] and p.[product_type_id] = pt.[product_type_id] and
                             pc.[supplier_id] = s.[supplier_id]";
            if (purchase_id.Trim() != "")
            {
                sql += "and pc.[purchase_id] = @purchase_id ";
            }
            if (product_name.Trim() != "")
            {
                sql += "and p.[product_name] like '%' + @product_name + '%' ";
            }
            if (supplier_name.Trim() != "" && supplier_name != "NONE")
            {
                sql += "and s.[supplier_name] = @supplier_name ";
            }
            if (number_received.Trim() != "")
            {
                if (param == 0)
                {
                    sql += "and pc.[number_received] < @number_received ";
                }
                else if (param == 1)
                {
                    sql += "and pc.[number_received] = @number_received ";
                }
                else if (param == 2)
                {
                    sql += "and pc.[number_received] > @number_received ";
                }
            }

            if (bool_date)
            {
                sql += "and pc.[purchase_date] = @purchase_date";
            }


            try
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = _conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = sql;
                    cmd.Parameters.Clear();
                    CheckIsNull(cmd, "@purchase_id", purchase_id);
                    CheckIsNull(cmd, "@product_name", product_name);
                    CheckIsNull(cmd, "@supplier_name", supplier_name);
                    CheckIsNull(cmd, "@number_received", number_received);
                    CheckIsNull(cmd, "@purchase_date", search_Date.ToString("MM/dd/yyyy"));
                    using (var reader = cmd.ExecuteReader())
                    {
                        result = new List<Purchase>();
                        while (reader.Read())
                        {
                            DateTime date;
                            int int_purchase_id, int_product_id, int_number_received;
                            if (!(Int32.TryParse(reader["purchase_id"].ToString(), out int_purchase_id))) err += "Error when parsing purchase id.\n";
                            if (!(Int32.TryParse(reader["product_id"].ToString(), out int_product_id))) err += "Error when parsing product id.\n";
                            if (!(Int32.TryParse(reader["number_received"].ToString(), out int_number_received))) err += "Error when parsing number received.\n";
                            if (!(DateTime.TryParse(reader["purchase_date"].ToString(), out date))) err += "Error when parsing date.";
                            if (err.Trim() != "") MessageBox.Show(err, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            else
                            {
                                result.Add(new Purchase
                                {
                                    purchase_id = int_purchase_id,
                                    product_id = int_product_id,
                                    product_name = reader["product_name"].ToString(),
                                    supplier_name = reader["supplier_name"].ToString(),
                                    purchase_date = date,
                                    number_received = int_number_received
                                });
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                QueryStatus = false;
                ErrorMessage = ex.Message;
            }

            return result;
        }
    
        public bool Remove_Purchase(int purchase_id)//menyingkirkan purchase tertentu dengan kriteria purchase id dari tampilan dgv
        {
            QueryStatus = true;
            result = false;
            try
            {
                using (var cmd = new SqlCommand()) //mulai perintah sql
                {
                    cmd.Connection = _conn; //koneksi yang digunakan
                    cmd.CommandType = System.Data.CommandType.Text; // format tipe perintah 
                    //query yang digunakan
                    cmd.CommandText = @"update [InventoryManager].[dbo].[purchase] SET status = 'NO' where purchase_id = @purchase_id and status = 'YES' ;";
                    cmd.Parameters.Clear();//memberishkan parameter query sebelumnya
                    //tambahkan nilai kedalam query
                    cmd.Parameters.AddWithValue("@purchase_id", purchase_id);
                    result = cmd.ExecuteNonQuery() > 0;
                }
            }
            catch (Exception ex)
            {
                QueryStatus = false;
                result = false;
                ErrorMessage = ex.Message;
            }



            return result;
        }
    }
}

