namespace GUI
{
    partial class ThongKeDoanhChi
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_XuatBaoCaoDoanhChi = new Control.Control.btn_CustomRedButton();
            this.btn_ThongKeChi = new Control.Control.btn_CustomRedButton();
            this.txtNam = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.data_GV_ThongKeChi = new System.Windows.Forms.DataGridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.char1_ThongKeChi = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txt_tongtien = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_phi_nhapnguyenlieu = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_phi_nhapttb = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_phi_ycsc = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_GV_ThongKeChi)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.char1_ThongKeChi)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AllowDrop = true;
            this.panel1.BackColor = System.Drawing.Color.DodgerBlue;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.ForeColor = System.Drawing.Color.Transparent;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(921, 64);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(317, 19);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(174, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thống Kế Doanh Chi";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_XuatBaoCaoDoanhChi);
            this.groupBox1.Controls.Add(this.btn_ThongKeChi);
            this.groupBox1.Controls.Add(this.txtNam);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.data_GV_ThongKeChi);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.groupBox1.Location = new System.Drawing.Point(8, 74);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox1.Size = new System.Drawing.Size(539, 223);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông Kê Chi";
            // 
            // btn_XuatBaoCaoDoanhChi
            // 
            this.btn_XuatBaoCaoDoanhChi.BackColor = System.Drawing.Color.DarkBlue;
            this.btn_XuatBaoCaoDoanhChi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_XuatBaoCaoDoanhChi.FlatAppearance.BorderSize = 0;
            this.btn_XuatBaoCaoDoanhChi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_XuatBaoCaoDoanhChi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btn_XuatBaoCaoDoanhChi.ForeColor = System.Drawing.Color.White;
            this.btn_XuatBaoCaoDoanhChi.Location = new System.Drawing.Point(269, 28);
            this.btn_XuatBaoCaoDoanhChi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_XuatBaoCaoDoanhChi.Name = "btn_XuatBaoCaoDoanhChi";
            this.btn_XuatBaoCaoDoanhChi.Size = new System.Drawing.Size(84, 26);
            this.btn_XuatBaoCaoDoanhChi.TabIndex = 4;
            this.btn_XuatBaoCaoDoanhChi.Text = "Báo Cáo";
            this.btn_XuatBaoCaoDoanhChi.UseVisualStyleBackColor = false;
            // 
            // btn_ThongKeChi
            // 
            this.btn_ThongKeChi.BackColor = System.Drawing.Color.DarkBlue;
            this.btn_ThongKeChi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ThongKeChi.FlatAppearance.BorderSize = 0;
            this.btn_ThongKeChi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ThongKeChi.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btn_ThongKeChi.ForeColor = System.Drawing.Color.White;
            this.btn_ThongKeChi.Location = new System.Drawing.Point(173, 28);
            this.btn_ThongKeChi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btn_ThongKeChi.Name = "btn_ThongKeChi";
            this.btn_ThongKeChi.Size = new System.Drawing.Size(84, 26);
            this.btn_ThongKeChi.TabIndex = 3;
            this.btn_ThongKeChi.Text = "Thống Kê";
            this.btn_ThongKeChi.UseVisualStyleBackColor = false;
            // 
            // txtNam
            // 
            this.txtNam.Location = new System.Drawing.Point(63, 28);
            this.txtNam.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtNam.Name = "txtNam";
            this.txtNam.Size = new System.Drawing.Size(84, 20);
            this.txtNam.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 30);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Năm";
            // 
            // data_GV_ThongKeChi
            // 
            this.data_GV_ThongKeChi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_GV_ThongKeChi.Location = new System.Drawing.Point(13, 58);
            this.data_GV_ThongKeChi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.data_GV_ThongKeChi.Name = "data_GV_ThongKeChi";
            this.data_GV_ThongKeChi.RowHeadersWidth = 62;
            this.data_GV_ThongKeChi.RowTemplate.Height = 28;
            this.data_GV_ThongKeChi.Size = new System.Drawing.Size(509, 147);
            this.data_GV_ThongKeChi.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.char1_ThongKeChi);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.groupBox2.Location = new System.Drawing.Point(21, 311);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox2.Size = new System.Drawing.Size(834, 253);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Biểu Đồ Thống Kê";
            // 
            // char1_ThongKeChi
            // 
            chartArea1.Name = "ChartArea1";
            this.char1_ThongKeChi.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.char1_ThongKeChi.Legends.Add(legend1);
            this.char1_ThongKeChi.Location = new System.Drawing.Point(13, 22);
            this.char1_ThongKeChi.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.char1_ThongKeChi.Name = "char1_ThongKeChi";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.char1_ThongKeChi.Series.Add(series1);
            this.char1_ThongKeChi.Size = new System.Drawing.Size(803, 216);
            this.char1_ThongKeChi.TabIndex = 0;
            this.char1_ThongKeChi.Text = "chart1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txt_tongtien);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.txt_phi_nhapnguyenlieu);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.txt_phi_nhapttb);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.txt_phi_ycsc);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.RoyalBlue;
            this.groupBox3.Location = new System.Drawing.Point(551, 90);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.groupBox3.Size = new System.Drawing.Size(339, 217);
            this.groupBox3.TabIndex = 5;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thông Tin ";
            // 
            // txt_tongtien
            // 
            this.txt_tongtien.Location = new System.Drawing.Point(172, 183);
            this.txt_tongtien.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_tongtien.Name = "txt_tongtien";
            this.txt_tongtien.Size = new System.Drawing.Size(143, 20);
            this.txt_tongtien.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 186);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 13);
            this.label6.TabIndex = 6;
            this.label6.Text = "Tổng Tiền";
            // 
            // txt_phi_nhapnguyenlieu
            // 
            this.txt_phi_nhapnguyenlieu.Location = new System.Drawing.Point(172, 141);
            this.txt_phi_nhapnguyenlieu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_phi_nhapnguyenlieu.Name = "txt_phi_nhapnguyenlieu";
            this.txt_phi_nhapnguyenlieu.Size = new System.Drawing.Size(143, 20);
            this.txt_phi_nhapnguyenlieu.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 144);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(136, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Phí Nhập Nguyên Liệu";
            // 
            // txt_phi_nhapttb
            // 
            this.txt_phi_nhapttb.Location = new System.Drawing.Point(172, 94);
            this.txt_phi_nhapttb.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_phi_nhapttb.Name = "txt_phi_nhapttb";
            this.txt_phi_nhapttb.Size = new System.Drawing.Size(143, 20);
            this.txt_phi_nhapttb.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 97);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(146, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Phí Nhập Trang Thiết Bị";
            // 
            // txt_phi_ycsc
            // 
            this.txt_phi_ycsc.Location = new System.Drawing.Point(172, 51);
            this.txt_phi_ycsc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txt_phi_ycsc.Name = "txt_phi_ycsc";
            this.txt_phi_ycsc.Size = new System.Drawing.Size(143, 20);
            this.txt_phi_ycsc.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(4, 54);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(138, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Phí Yêu Cầu Sửa Chữa";
            // 
            // ThongKeDoanhChi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(921, 648);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "ThongKeDoanhChi";
            this.Text = "ThongKeDoanhChi";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_GV_ThongKeChi)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.char1_ThongKeChi)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView data_GV_ThongKeChi;
        private Control.Control.btn_CustomRedButton btn_ThongKeChi;
        private System.Windows.Forms.TextBox txtNam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart char1_ThongKeChi;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txt_tongtien;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_phi_nhapnguyenlieu;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_phi_nhapttb;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_phi_ycsc;
        private System.Windows.Forms.Label label3;
        private Control.Control.btn_CustomRedButton btn_XuatBaoCaoDoanhChi;
    }
}