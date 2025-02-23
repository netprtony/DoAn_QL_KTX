namespace GUI
{
    partial class QuanLyThongBao
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
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_DangThongBao = new Control.Control.btn_CustomRedButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_LoadFile = new Control.Control.btn_CustomRedButton();
            this.txt_TenFile = new System.Windows.Forms.TextBox();
            this.webBrowser_FileThongBao = new System.Windows.Forms.WebBrowser();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dateTime_NgayHetHan = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.dateTime_NgayTao = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_TenNoiDungTB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_TenTieuDe = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1101, 113);
            this.panel1.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(408, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(322, 37);
            this.label1.TabIndex = 0;
            this.label1.Text = "Quản Lý Thông Báo";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.tabControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 113);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1101, 566);
            this.panel2.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1101, 566);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_DangThongBao);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1093, 533);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Đăng Thông Báo";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_DangThongBao
            // 
            this.btn_DangThongBao.BackColor = System.Drawing.Color.DarkBlue;
            this.btn_DangThongBao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_DangThongBao.FlatAppearance.BorderSize = 0;
            this.btn_DangThongBao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_DangThongBao.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btn_DangThongBao.ForeColor = System.Drawing.Color.White;
            this.btn_DangThongBao.Location = new System.Drawing.Point(472, 456);
            this.btn_DangThongBao.Name = "btn_DangThongBao";
            this.btn_DangThongBao.Size = new System.Drawing.Size(176, 35);
            this.btn_DangThongBao.TabIndex = 9;
            this.btn_DangThongBao.Text = "Đăng Thông Báo";
            this.btn_DangThongBao.UseVisualStyleBackColor = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_LoadFile);
            this.groupBox2.Controls.Add(this.txt_TenFile);
            this.groupBox2.Controls.Add(this.webBrowser_FileThongBao);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.groupBox2.Location = new System.Drawing.Point(532, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(506, 418);
            this.groupBox2.TabIndex = 8;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chi Tiết Thông Tin";
            // 
            // btn_LoadFile
            // 
            this.btn_LoadFile.BackColor = System.Drawing.Color.DarkBlue;
            this.btn_LoadFile.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_LoadFile.FlatAppearance.BorderSize = 0;
            this.btn_LoadFile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_LoadFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btn_LoadFile.ForeColor = System.Drawing.Color.White;
            this.btn_LoadFile.Location = new System.Drawing.Point(23, 176);
            this.btn_LoadFile.Name = "btn_LoadFile";
            this.btn_LoadFile.Size = new System.Drawing.Size(97, 32);
            this.btn_LoadFile.TabIndex = 10;
            this.btn_LoadFile.Text = "Tải File";
            this.btn_LoadFile.UseVisualStyleBackColor = false;
            // 
            // txt_TenFile
            // 
            this.txt_TenFile.Location = new System.Drawing.Point(23, 118);
            this.txt_TenFile.Name = "txt_TenFile";
            this.txt_TenFile.Size = new System.Drawing.Size(112, 26);
            this.txt_TenFile.TabIndex = 9;
            // 
            // webBrowser_FileThongBao
            // 
            this.webBrowser_FileThongBao.Location = new System.Drawing.Point(155, 35);
            this.webBrowser_FileThongBao.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser_FileThongBao.Name = "webBrowser_FileThongBao";
            this.webBrowser_FileThongBao.Size = new System.Drawing.Size(334, 358);
            this.webBrowser_FileThongBao.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(18, 78);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(117, 20);
            this.label9.TabIndex = 0;
            this.label9.Text = "File Nội Dung";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dateTime_NgayHetHan);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.dateTime_NgayTao);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_TenNoiDungTB);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_TenTieuDe);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.groupBox1.Location = new System.Drawing.Point(8, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(506, 418);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông Tin Thông Báo";
            // 
            // dateTime_NgayHetHan
            // 
            this.dateTime_NgayHetHan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTime_NgayHetHan.Location = new System.Drawing.Point(164, 367);
            this.dateTime_NgayHetHan.Name = "dateTime_NgayHetHan";
            this.dateTime_NgayHetHan.Size = new System.Drawing.Size(297, 26);
            this.dateTime_NgayHetHan.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(18, 372);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(121, 20);
            this.label5.TabIndex = 6;
            this.label5.Text = "Ngày Hết Hạn";
            // 
            // dateTime_NgayTao
            // 
            this.dateTime_NgayTao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTime_NgayTao.Location = new System.Drawing.Point(164, 309);
            this.dateTime_NgayTao.Name = "dateTime_NgayTao";
            this.dateTime_NgayTao.Size = new System.Drawing.Size(297, 26);
            this.dateTime_NgayTao.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 314);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(84, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Ngày Tạo";
            // 
            // txt_TenNoiDungTB
            // 
            this.txt_TenNoiDungTB.Location = new System.Drawing.Point(164, 158);
            this.txt_TenNoiDungTB.Multiline = true;
            this.txt_TenNoiDungTB.Name = "txt_TenNoiDungTB";
            this.txt_TenNoiDungTB.Size = new System.Drawing.Size(297, 104);
            this.txt_TenNoiDungTB.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 201);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 20);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nội Dung";
            // 
            // txt_TenTieuDe
            // 
            this.txt_TenTieuDe.Location = new System.Drawing.Point(164, 55);
            this.txt_TenTieuDe.Multiline = true;
            this.txt_TenTieuDe.Name = "txt_TenTieuDe";
            this.txt_TenTieuDe.Size = new System.Drawing.Size(297, 67);
            this.txt_TenTieuDe.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(19, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Tiêu Đề";
            // 
            // QuanLyThongBao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1101, 679);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "QuanLyThongBao";
            this.Text = "QuanLyThongBao";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker dateTime_NgayHetHan;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker dateTime_NgayTao;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_TenNoiDungTB;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_TenTieuDe;
        private System.Windows.Forms.Label label2;
        private Control.Control.btn_CustomRedButton btn_LoadFile;
        private System.Windows.Forms.TextBox txt_TenFile;
        private System.Windows.Forms.WebBrowser webBrowser_FileThongBao;
        private Control.Control.btn_CustomRedButton btn_DangThongBao;
    }
}