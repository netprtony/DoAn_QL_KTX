using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control
{
    public class txt_tenDangNhap:TextBox
    {

        public ErrorProvider errorProvider;

        public txt_tenDangNhap()
        {
            errorProvider = new ErrorProvider();
            this.KeyPress += txt_tenDangNhap_KeyPress;
            this.TextChanged += txt_tenDangNhap_TextChanged;
        }

        // Sự kiện kiểm tra khi người dùng nhập ký tự
        void txt_tenDangNhap_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép các ký tự chữ cái (không bao gồm khoảng trắng, số, ký tự đặc biệt)
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true; // Ngăn ký tự không hợp lệ
            }
        }

        // Kiểm tra lại chuỗi nhập vào trong sự kiện TextChanged
        void txt_tenDangNhap_TextChanged(object sender, EventArgs e)
        {
            // Kiểm tra chuỗi có ký tự không hợp lệ (số, khoảng trắng, hoặc ký tự đặc biệt)
            if (!Regex.IsMatch(this.Text, @"^[A-Za-z]+$"))
            {
                errorProvider.SetError(this, "Tên đăng nhập chỉ được chứa các ký tự chữ cái.");
            }
            else
            {
                errorProvider.Clear();
            }
        }
    }
}
