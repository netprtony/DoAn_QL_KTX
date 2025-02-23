namespace GUI
{
    partial class ThongKeThu
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.chart_Spline_TT_Thu = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_CustomRedButton1 = new Control.Control.btn_CustomRedButton();
            this.btn_ThongKeThu = new Control.Control.btn_CustomRedButton();
            this.txtNam = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.data_GV_ThongKeThu = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart_Spline_TT_Thu)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_GV_ThongKeThu)).BeginInit();
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
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1283, 99);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(476, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(261, 29);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thống Kê Doanh Thu";
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 99);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1283, 634);
            this.panel2.TabIndex = 4;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chart_Spline_TT_Thu);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.ForeColor = System.Drawing.Color.RoyalBlue;
            this.groupBox2.Location = new System.Drawing.Point(552, 42);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(660, 529);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Biểu Đồ Thống Kê";
            // 
            // chart_Spline_TT_Thu
            // 
            chartArea1.Name = "ChartArea1";
            this.chart_Spline_TT_Thu.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart_Spline_TT_Thu.Legends.Add(legend1);
            this.chart_Spline_TT_Thu.Location = new System.Drawing.Point(22, 46);
            this.chart_Spline_TT_Thu.Name = "chart_Spline_TT_Thu";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart_Spline_TT_Thu.Series.Add(series1);
            this.chart_Spline_TT_Thu.Size = new System.Drawing.Size(611, 461);
            this.chart_Spline_TT_Thu.TabIndex = 0;
            this.chart_Spline_TT_Thu.Text = "chart1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_CustomRedButton1);
            this.groupBox1.Controls.Add(this.btn_ThongKeThu);
            this.groupBox1.Controls.Add(this.txtNam);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.data_GV_ThongKeThu);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.groupBox1.Location = new System.Drawing.Point(32, 42);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(493, 529);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông Kê Chi";
            // 
            // btn_CustomRedButton1
            // 
            this.btn_CustomRedButton1.BackColor = System.Drawing.Color.DarkBlue;
            this.btn_CustomRedButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_CustomRedButton1.FlatAppearance.BorderSize = 0;
            this.btn_CustomRedButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_CustomRedButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btn_CustomRedButton1.ForeColor = System.Drawing.Color.White;
            this.btn_CustomRedButton1.Location = new System.Drawing.Point(229, 94);
            this.btn_CustomRedButton1.Name = "btn_CustomRedButton1";
            this.btn_CustomRedButton1.Size = new System.Drawing.Size(126, 40);
            this.btn_CustomRedButton1.TabIndex = 4;
            this.btn_CustomRedButton1.Text = "Báo Cáo";
            this.btn_CustomRedButton1.UseVisualStyleBackColor = false;
            // 
            // btn_ThongKeThu
            // 
            this.btn_ThongKeThu.BackColor = System.Drawing.Color.DarkBlue;
            this.btn_ThongKeThu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_ThongKeThu.FlatAppearance.BorderSize = 0;
            this.btn_ThongKeThu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_ThongKeThu.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Bold);
            this.btn_ThongKeThu.ForeColor = System.Drawing.Color.White;
            this.btn_ThongKeThu.Location = new System.Drawing.Point(78, 94);
            this.btn_ThongKeThu.Name = "btn_ThongKeThu";
            this.btn_ThongKeThu.Size = new System.Drawing.Size(126, 40);
            this.btn_ThongKeThu.TabIndex = 3;
            this.btn_ThongKeThu.Text = "Thống Kê";
            this.btn_ThongKeThu.UseVisualStyleBackColor = false;
            // 
            // txtNam
            // 
            this.txtNam.Location = new System.Drawing.Point(94, 43);
            this.txtNam.Name = "txtNam";
            this.txtNam.Size = new System.Drawing.Size(277, 26);
            this.txtNam.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(45, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Năm";
            // 
            // data_GV_ThongKeThu
            // 
            this.data_GV_ThongKeThu.BackgroundColor = System.Drawing.SystemColors.ButtonFace;
            this.data_GV_ThongKeThu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.data_GV_ThongKeThu.Location = new System.Drawing.Point(35, 156);
            this.data_GV_ThongKeThu.Name = "data_GV_ThongKeThu";
            this.data_GV_ThongKeThu.RowHeadersWidth = 62;
            this.data_GV_ThongKeThu.RowTemplate.Height = 28;
            this.data_GV_ThongKeThu.Size = new System.Drawing.Size(410, 343);
            this.data_GV_ThongKeThu.TabIndex = 0;
            // 
            // ThongKeThu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1283, 733);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ThongKeThu";
            this.Text = "g";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart_Spline_TT_Thu)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.data_GV_ThongKeThu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private Control.Control.btn_CustomRedButton btn_CustomRedButton1;
        private Control.Control.btn_CustomRedButton btn_ThongKeThu;
        private System.Windows.Forms.TextBox txtNam;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView data_GV_ThongKeThu;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart_Spline_TT_Thu;
    }
}