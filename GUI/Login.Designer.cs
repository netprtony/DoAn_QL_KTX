namespace GUI
{
    partial class Login
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
            this.panel_lammo = new System.Windows.Forms.Panel();
            this.tableLayoutPanel_Login = new System.Windows.Forms.TableLayoutPanel();
            this.panel_Login = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel_nghien = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.check_luu = new System.Windows.Forms.CheckBox();
            this.btn_dangnhap = new System.Windows.Forms.Button();
            this.txt_MatKhau1 = new Control.txt_MatKhau();
            this.txt_tenDangNhap1 = new Control.txt_tenDangNhap();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel_Login.SuspendLayout();
            this.panel_Login.SuspendLayout();
            this.panel_nghien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_lammo
            // 
            this.panel_lammo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel_lammo.ForeColor = System.Drawing.SystemColors.Control;
            this.panel_lammo.Location = new System.Drawing.Point(214, 606);
            this.panel_lammo.Name = "panel_lammo";
            this.panel_lammo.Size = new System.Drawing.Size(1006, 67);
            this.panel_lammo.TabIndex = 2;
            // 
            // tableLayoutPanel_Login
            // 
            this.tableLayoutPanel_Login.BackColor = System.Drawing.Color.LightSkyBlue;
            this.tableLayoutPanel_Login.BackgroundImage = global::GUI.Properties.Resources.NenKTX;
            this.tableLayoutPanel_Login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tableLayoutPanel_Login.ColumnCount = 3;
            this.tableLayoutPanel_Login.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.5825F));
            this.tableLayoutPanel_Login.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 38.04607F));
            this.tableLayoutPanel_Login.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 22.39873F));
            this.tableLayoutPanel_Login.Controls.Add(this.panel_Login, 1, 1);
            this.tableLayoutPanel_Login.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel_Login.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel_Login.Name = "tableLayoutPanel_Login";
            this.tableLayoutPanel_Login.RowCount = 3;
            this.tableLayoutPanel_Login.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9.789157F));
            this.tableLayoutPanel_Login.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 90.21085F));
            this.tableLayoutPanel_Login.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel_Login.Size = new System.Drawing.Size(1259, 673);
            this.tableLayoutPanel_Login.TabIndex = 3;
            // 
            // panel_Login
            // 
            this.panel_Login.BackColor = System.Drawing.Color.LightCyan;
            this.panel_Login.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel_Login.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel_Login.Controls.Add(this.panel2);
            this.panel_Login.Controls.Add(this.label1);
            this.panel_Login.Controls.Add(this.panel_nghien);
            this.panel_Login.Controls.Add(this.pictureBox3);
            this.panel_Login.Controls.Add(this.pictureBox1);
            this.panel_Login.Controls.Add(this.check_luu);
            this.panel_Login.Controls.Add(this.btn_dangnhap);
            this.panel_Login.Controls.Add(this.txt_MatKhau1);
            this.panel_Login.Controls.Add(this.txt_tenDangNhap1);
            this.panel_Login.Controls.Add(this.label3);
            this.panel_Login.Controls.Add(this.label2);
            this.panel_Login.Location = new System.Drawing.Point(501, 68);
            this.panel_Login.Name = "panel_Login";
            this.panel_Login.Size = new System.Drawing.Size(472, 593);
            this.panel_Login.TabIndex = 1;
            this.panel_Login.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LightBlue;
            this.panel2.Location = new System.Drawing.Point(0, 556);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(472, 86);
            this.panel2.TabIndex = 11;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI Variable Display", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(125, 158);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(246, 58);
            this.label1.TabIndex = 0;
            this.label1.Text = "Đăng nhập";
            // 
            // panel_nghien
            // 
            this.panel_nghien.Controls.Add(this.panel1);
            this.panel_nghien.Controls.Add(this.pictureBox2);
            this.panel_nghien.Location = new System.Drawing.Point(-48, 3);
            this.panel_nghien.Name = "panel_nghien";
            this.panel_nghien.Size = new System.Drawing.Size(520, 152);
            this.panel_nghien.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(359, 84);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(128, 49);
            this.panel1.TabIndex = 10;
            // 
            // pictureBox2
            // 
            this.pictureBox2.BackColor = System.Drawing.Color.LightBlue;
            this.pictureBox2.Image = global::GUI.Properties.Resources.airplane__1_;
            this.pictureBox2.Location = new System.Drawing.Point(65, 9);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(121, 89);
            this.pictureBox2.TabIndex = 9;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = global::GUI.Properties.Resources.User1__2_;
            this.pictureBox3.Location = new System.Drawing.Point(17, 270);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(59, 47);
            this.pictureBox3.TabIndex = 8;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.ErrorImage = null;
            this.pictureBox1.Image = global::GUI.Properties.Resources.pass1;
            this.pictureBox1.Location = new System.Drawing.Point(17, 334);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(59, 47);
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // check_luu
            // 
            this.check_luu.AutoSize = true;
            this.check_luu.ForeColor = System.Drawing.Color.RoyalBlue;
            this.check_luu.Location = new System.Drawing.Point(268, 412);
            this.check_luu.Name = "check_luu";
            this.check_luu.Size = new System.Drawing.Size(171, 24);
            this.check_luu.TabIndex = 6;
            this.check_luu.Text = "Ghi nhớ đăng nhập";
            this.check_luu.UseVisualStyleBackColor = true;
            // 
            // btn_dangnhap
            // 
            this.btn_dangnhap.BackColor = System.Drawing.Color.RoyalBlue;
            this.btn_dangnhap.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_dangnhap.ForeColor = System.Drawing.Color.Transparent;
            this.btn_dangnhap.Location = new System.Drawing.Point(135, 469);
            this.btn_dangnhap.Name = "btn_dangnhap";
            this.btn_dangnhap.Size = new System.Drawing.Size(197, 49);
            this.btn_dangnhap.TabIndex = 5;
            this.btn_dangnhap.Text = "Đăng nhập";
            this.btn_dangnhap.UseVisualStyleBackColor = false;
            // 
            // txt_MatKhau1
            // 
            this.txt_MatKhau1.Location = new System.Drawing.Point(218, 347);
            this.txt_MatKhau1.Name = "txt_MatKhau1";
            this.txt_MatKhau1.Size = new System.Drawing.Size(239, 26);
            this.txt_MatKhau1.TabIndex = 4;
            // 
            // txt_tenDangNhap1
            // 
            this.txt_tenDangNhap1.Location = new System.Drawing.Point(218, 281);
            this.txt_tenDangNhap1.Name = "txt_tenDangNhap1";
            this.txt_tenDangNhap1.Size = new System.Drawing.Size(207, 26);
            this.txt_tenDangNhap1.TabIndex = 3;
            this.txt_tenDangNhap1.TextChanged += new System.EventHandler(this.txt_tenDangNhap1_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label3.Location = new System.Drawing.Point(73, 353);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Mật khẩu";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label2.Location = new System.Drawing.Point(73, 287);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên đăng nhập";
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1259, 673);
            this.Controls.Add(this.tableLayoutPanel_Login);
            this.Controls.Add(this.panel_lammo);
            this.DoubleBuffered = true;
            this.Name = "Login";
            this.Text = "Login";
            this.tableLayoutPanel_Login.ResumeLayout(false);
            this.panel_Login.ResumeLayout(false);
            this.panel_Login.PerformLayout();
            this.panel_nghien.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel_Login;
        private System.Windows.Forms.Button btn_dangnhap;
        private Control.txt_MatKhau txt_MatKhau1;
        private Control.txt_tenDangNhap txt_tenDangNhap1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox check_luu;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel_lammo;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel_Login;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panel_nghien;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
    }
}