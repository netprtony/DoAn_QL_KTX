using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control
{
    public class txt_SoNguyenDuong : TextBox
    {
        public ErrorProvider errorProvider;

        public txt_SoNguyenDuong()
        {
            errorProvider = new ErrorProvider();
            this.KeyPress += txt_SoNguyenDuong_KeyPress;
            this.TextChanged += txt_SoNguyenDuong_TextChanged;
        }

        // Sự kiện kiểm tra khi người dùng nhập ký tự
        void txt_SoNguyenDuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép các ký tự số và phím Backspace
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ngăn ký tự không hợp lệ
            }
        }

        // Kiểm tra lại chuỗi nhập vào trong sự kiện TextChanged
        void txt_SoNguyenDuong_TextChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu chuỗi không phải là số nguyên dương
            if (!Regex.IsMatch(this.Text, @"^[1-9][0-9]*$"))
            {
                errorProvider.SetError(this, "Chỉ được nhập số nguyên dương.");
            }
            else
            {
                errorProvider.Clear();
            }
        }
    }
}
