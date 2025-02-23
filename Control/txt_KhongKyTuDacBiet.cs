using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control
{
    public class txt_KhongKyTuDacBiet : TextBox
    {
        public ErrorProvider errorProvider;

        public txt_KhongKyTuDacBiet()
        {
            errorProvider = new ErrorProvider();
            this.KeyPress += txt_KhongKyTuDacBiet_KeyPress;
            this.TextChanged += txt_KhongKyTuDacBiet_TextChanged;
        }

        // Sự kiện kiểm tra khi người dùng nhập ký tự
        void txt_KhongKyTuDacBiet_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép chữ cái, chữ số, dấu cách và Backspace
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true; // Ngăn ký tự không hợp lệ
            }
        }

        // Kiểm tra lại chuỗi nhập vào trong sự kiện TextChanged
        void txt_KhongKyTuDacBiet_TextChanged(object sender, EventArgs e)
        {
            // Kiểm tra nếu có ký tự đặc biệt thì hiển thị thông báo lỗi
            if (Regex.IsMatch(this.Text, @"[^A-Za-z0-9 ]"))
            {
                errorProvider.SetError(this, "Chỉ cho phép chữ cái, chữ số và dấu cách.");
            }
            else
            {
                errorProvider.Clear();
            }
        }
    }
}
