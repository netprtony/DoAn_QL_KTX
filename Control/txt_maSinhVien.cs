using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control
{
    public class txt_maSinhVien: TextBox
    {
        public ErrorProvider errorProvider;

        public txt_maSinhVien()
        {
            errorProvider = new ErrorProvider();
            this.TextChanged += txt_maSinhVien_TextChanged;
            errorProvider = new ErrorProvider();
        }

        // Kiểm tra chuỗi nhập vào trong sự kiện TextChanged
        void txt_maSinhVien_TextChanged(object sender, EventArgs e)
        {
            string studentID = this.Text;

            // Kiểm tra nếu mã sinh viên có đúng 10 chữ số
            if (!Regex.IsMatch(studentID, @"^\d{10}$"))
            {
                errorProvider.SetError(this, "Mã sinh viên phải có đúng 10 chữ số.");
            }
            else
            {
                errorProvider.Clear();
            }
        }
    }
}
