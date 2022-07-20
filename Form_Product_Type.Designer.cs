namespace ProjectInventoris
{
    partial class Form_Product_Type
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tb_Search_Product_Type_Id = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.btn_Search = new System.Windows.Forms.Button();
            this.tb_Search_Product_Type_Name = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.dgv_Product_Type = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tb_Product_Type = new System.Windows.Forms.TextBox();
            this.btn_Refresh = new System.Windows.Forms.Button();
            this.btn_Add = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Remove = new System.Windows.Forms.Button();
            this.btn_Update = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Product_Type)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tb_Search_Product_Type_Id);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.btn_Search);
            this.groupBox1.Controls.Add(this.tb_Search_Product_Type_Name);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Location = new System.Drawing.Point(504, 29);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(390, 178);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Search";
            // 
            // tb_Search_Product_Type_Id
            // 
            this.tb_Search_Product_Type_Id.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tb_Search_Product_Type_Id.Location = new System.Drawing.Point(23, 43);
            this.tb_Search_Product_Type_Id.MaxLength = 9;
            this.tb_Search_Product_Type_Id.Name = "tb_Search_Product_Type_Id";
            this.tb_Search_Product_Type_Id.Size = new System.Drawing.Size(347, 22);
            this.tb_Search_Product_Type_Id.TabIndex = 0;
            this.tb_Search_Product_Type_Id.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_Search_Product_Type_Id_KeyPress_1);
            // 
            // label13
            // 
            this.label13.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(19, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(130, 20);
            this.label13.TabIndex = 34;
            this.label13.Text = "Product Type ID";
            // 
            // btn_Search
            // 
            this.btn_Search.Location = new System.Drawing.Point(24, 128);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(103, 30);
            this.btn_Search.TabIndex = 2;
            this.btn_Search.Text = "Search";
            this.btn_Search.UseVisualStyleBackColor = true;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // tb_Search_Product_Type_Name
            // 
            this.tb_Search_Product_Type_Name.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tb_Search_Product_Type_Name.Location = new System.Drawing.Point(23, 90);
            this.tb_Search_Product_Type_Name.MaxLength = 30;
            this.tb_Search_Product_Type_Name.Name = "tb_Search_Product_Type_Name";
            this.tb_Search_Product_Type_Name.Size = new System.Drawing.Size(347, 22);
            this.tb_Search_Product_Type_Name.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(19, 67);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 20);
            this.label9.TabIndex = 31;
            this.label9.Text = "Product Type";
            // 
            // dgv_Product_Type
            // 
            this.dgv_Product_Type.AllowUserToAddRows = false;
            this.dgv_Product_Type.AllowUserToDeleteRows = false;
            this.dgv_Product_Type.AllowUserToResizeColumns = false;
            this.dgv_Product_Type.AllowUserToResizeRows = false;
            this.dgv_Product_Type.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgv_Product_Type.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgv_Product_Type.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_Product_Type.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.dgv_Product_Type.Location = new System.Drawing.Point(23, 29);
            this.dgv_Product_Type.MultiSelect = false;
            this.dgv_Product_Type.Name = "dgv_Product_Type";
            this.dgv_Product_Type.ReadOnly = true;
            this.dgv_Product_Type.RowHeadersWidth = 51;
            this.dgv_Product_Type.RowTemplate.Height = 24;
            this.dgv_Product_Type.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv_Product_Type.Size = new System.Drawing.Size(433, 287);
            this.dgv_Product_Type.TabIndex = 45;
            this.dgv_Product_Type.TabStop = false;
            this.dgv_Product_Type.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Product_Type_CellClick);
            this.dgv_Product_Type.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_Product_Type_CellDoubleClick);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Product Type Id";
            this.Column1.MinimumWidth = 6;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "Product Type";
            this.Column2.MinimumWidth = 6;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.tb_Product_Type);
            this.panel1.Controls.Add(this.btn_Refresh);
            this.panel1.Controls.Add(this.btn_Add);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btn_Remove);
            this.panel1.Controls.Add(this.btn_Update);
            this.panel1.Location = new System.Drawing.Point(504, 228);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(390, 88);
            this.panel1.TabIndex = 0;
            // 
            // tb_Product_Type
            // 
            this.tb_Product_Type.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_Product_Type.Location = new System.Drawing.Point(152, 9);
            this.tb_Product_Type.MaxLength = 30;
            this.tb_Product_Type.Name = "tb_Product_Type";
            this.tb_Product_Type.Size = new System.Drawing.Size(233, 22);
            this.tb_Product_Type.TabIndex = 0;
            this.tb_Product_Type.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tb_Search_Product_Type_Name_KeyPress);
            // 
            // btn_Refresh
            // 
            this.btn_Refresh.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Refresh.Location = new System.Drawing.Point(296, 45);
            this.btn_Refresh.Name = "btn_Refresh";
            this.btn_Refresh.Size = new System.Drawing.Size(86, 29);
            this.btn_Refresh.TabIndex = 4;
            this.btn_Refresh.Text = "&Refresh";
            this.btn_Refresh.UseVisualStyleBackColor = true;
            this.btn_Refresh.Click += new System.EventHandler(this.btn_Refresh_Click);
            // 
            // btn_Add
            // 
            this.btn_Add.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Add.Location = new System.Drawing.Point(17, 45);
            this.btn_Add.Name = "btn_Add";
            this.btn_Add.Size = new System.Drawing.Size(86, 29);
            this.btn_Add.TabIndex = 1;
            this.btn_Add.Text = "&Add";
            this.btn_Add.UseVisualStyleBackColor = true;
            this.btn_Add.Click += new System.EventHandler(this.btn_Add_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(108, 20);
            this.label1.TabIndex = 60;
            this.label1.Text = "Product Type";
            // 
            // btn_Remove
            // 
            this.btn_Remove.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Remove.Location = new System.Drawing.Point(204, 45);
            this.btn_Remove.Name = "btn_Remove";
            this.btn_Remove.Size = new System.Drawing.Size(86, 29);
            this.btn_Remove.TabIndex = 3;
            this.btn_Remove.Text = "&Remove";
            this.btn_Remove.UseVisualStyleBackColor = true;
            this.btn_Remove.Click += new System.EventHandler(this.btn_Remove_Click);
            // 
            // btn_Update
            // 
            this.btn_Update.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btn_Update.Location = new System.Drawing.Point(112, 45);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(86, 29);
            this.btn_Update.TabIndex = 2;
            this.btn_Update.Text = "&Update";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // Form_Product_Type
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 363);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.dgv_Product_Type);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form_Product_Type";
            this.Text = "Panel Product Type [2]";
            this.Load += new System.EventHandler(this.Form_Product_Type_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Product_Type)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dgv_Product_Type;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox tb_Product_Type;
        private System.Windows.Forms.Button btn_Refresh;
        private System.Windows.Forms.Button btn_Remove;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.Button btn_Add;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_Search_Product_Type_Id;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.TextBox tb_Search_Product_Type_Name;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
    }
}