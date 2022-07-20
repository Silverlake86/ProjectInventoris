namespace ProjectInventoris
{
    partial class Form_Supplier
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.btn_Remove = new System.Windows.Forms.Button();
            this.btn_Update = new System.Windows.Forms.Button();
            this.tb_Supplier_Address = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_Supplier_Name = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_Supplier = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_Search_Supplier_Address = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tb_Search_Supplier_ID = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_Search = new System.Windows.Forms.Button();
            this.tb_Search_Supplier_Name = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Supplier)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.panel1.Controls.Add(this.btn_Refresh);
            this.panel1.Controls.Add(this.btn_Add);
            this.panel1.Controls.Add(this.btn_Remove);
            this.panel1.Controls.Add(this.btn_Update);
            this.panel1.Controls.Add(this.tb_Supplier_Address);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.tb_Supplier_Name);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(13, 307);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1043, 88);
            this.panel1.TabIndex = 0;
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Refresh.Location = new System.Drawing.Point(707, 47);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(86, 29);
            this.btn_Refresh.TabIndex = 5;
            this.btn_Refresh.Text = "&Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Add.Location = new System.Drawing.Point(707, 12);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(86, 29);
            this.btn_Add.TabIndex = 2;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // btn_Remove
            // 
            this.btn_Remove.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Remove.Location = new System.Drawing.Point(892, 12);
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.Size = new System.Drawing.Size(86, 29);
            this.btn_Remove.TabIndex = 4;
            this.btn_Remove.Text = "&Remove";
            this.btn_Remove.UseVisualStyleBackColor = true;
            this.btn_Remove.Click += new System.EventHandler(this.btn_Remove_Click);
            // 
            // btn_Update
            // 
            this.btn_Update.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Update.Location = new System.Drawing.Point(799, 12);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(86, 29);
            this.btn_Update.TabIndex = 3;
            this.btn_Update.Text = "&Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // tb_Supplier_Address
            // 
            this.tb_Supplier_Address.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Supplier_Address.Location = new System.Drawing.Point(175, 37);
            this.tb_Supplier_Address.MaxLength = 100;
            this.tb_Supplier_Address.Multiline = true;
            this.tb_Supplier_Address.Name = "tb_Supplier_Address";
            this.tb_Supplier_Address.Size = new System.Drawing.Size(505, 37);
            this.tb_Supplier_Address.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(137, 20);
            this.label4.TabIndex = 38;
            this.label4.Text = "Supplier Address";
            // 
            // tb_Supplier_Name
            // 
            this.tb_Supplier_Name.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.tb_Supplier_Name.Location = new System.Drawing.Point(175, 9);
            this.tb_Supplier_Name.MaxLength = 30;
            this.tb_Supplier_Name.Name = "tb_Supplier_Name";
            this.tb_Supplier_Name.Size = new System.Drawing.Size(505, 22);
            this.tb_Supplier_Name.TabIndex = 0;
            this.tb_Supplier_Name.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_Supplier_Name_KeyPress);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(119, 20);
            this.label1.TabIndex = 60;
            this.label1.Text = "Supplier Name";
            // 
            // dgv_Supplier
            // 
            this.dgv_Supplier.AllowUserToAddRows = false;
            this.dgv_Supplier.AllowUserToDeleteRows = false;
            this.dgv_Supplier.AllowUserToResizeColumns = false;
            this.dgv_Supplier.AllowUserToResizeRows = false;
            this.dgv_Supplier.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Supplier.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Supplier.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Supplier.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column4});
            this.dgv_Supplier.Location = new System.Drawing.Point(13, 25);
            this.dgv_Supplier.MultiSelect = false;
            this.dgv_Supplier.Name = "dgv_Supplier";
            this.dgv_Supplier.ReadOnly = true;
            this.dgv_Supplier.RowHeadersWidth = 51;
            this.dgv_Supplier.RowTemplate.Height = 24;
            this.dgv_Supplier.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Supplier.Size = new System.Drawing.Size(685, 264);
            this.dgv_Supplier.TabIndex = 0;
            this.dgv_Supplier.TabStop = false;
            this.dgv_Supplier.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Supplier_CellClick);
            this.dgv_Supplier.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Supplier_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Supplier ID";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Supplier Name";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column4
            // 
            this.Column4.HeaderText = "Supplier Address";
            this.Column4.MinimumWidth = 6;
            this.Column4.Name = "Column4";
            this.Column4.ReadOnly = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tb_Search_Supplier_Address);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.tb_Search_Supplier_ID);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.btn_Search);
            this.groupBox1.Controls.Add(this.tb_Search_Supplier_Name);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(720, 25);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(336, 237);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // tb_Search_Supplier_Address
            // 
            this.tb_Search_Supplier_Address.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tb_Search_Supplier_Address.Location = new System.Drawing.Point(23, 146);
            this.tb_Search_Supplier_Address.MaxLength = 100;
            this.tb_Search_Supplier_Address.Multiline = true;
            this.tb_Search_Supplier_Address.Name = "tb_Search_Supplier_Address";
            this.tb_Search_Supplier_Address.Size = new System.Drawing.Size(271, 37);
            this.tb_Search_Supplier_Address.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(19, 123);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(137, 20);
            this.label3.TabIndex = 36;
            this.label3.Text = "Supplier Address";
            // 
            // tb_Search_Supplier_ID
            // 
            this.tb_Search_Supplier_ID.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tb_Search_Supplier_ID.Location = new System.Drawing.Point(23, 49);
            this.tb_Search_Supplier_ID.MaxLength = 9;
            this.tb_Search_Supplier_ID.Name = "tb_Search_Supplier_ID";
            this.tb_Search_Supplier_ID.Size = new System.Drawing.Size(271, 22);
            this.tb_Search_Supplier_ID.TabIndex = 0;
            this.tb_Search_Supplier_ID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_Search_Supplier_ID_KeyPress);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(19, 26);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(92, 20);
            this.label13.TabIndex = 34;
            this.label13.Text = "Supplier ID";
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(23, 189);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(103, 30);
            this.btn_Search.TabIndex = 3;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // tb_Search_Supplier_Name
            // 
            this.tb_Search_Supplier_Name.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tb_Search_Supplier_Name.Location = new System.Drawing.Point(23, 96);
            this.tb_Search_Supplier_Name.MaxLength = 30;
            this.tb_Search_Supplier_Name.Name = "tb_Search_Supplier_Name";
            this.tb_Search_Supplier_Name.Size = new System.Drawing.Size(271, 22);
            this.tb_Search_Supplier_Name.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(19, 73);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(119, 20);
            this.label9.TabIndex = 31;
            this.label9.Text = "Supplier Name";
            // 
            // Form_Supplier
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1073, 418);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgv_Supplier);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form_Supplier";
            this.Text = "Panel Supplier [3]";
            this.Load += new System.EventHandler(this.Form_Supplier_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Supplier)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_Supplier_Name;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgv_Supplier;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tb_Search_Supplier_ID;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.TextBox tb_Search_Supplier_Name;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox tb_Supplier_Address;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_Search_Supplier_Address;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Button btn_Remove;
        private System.Windows.Forms.Button btn_Update;
    }
}