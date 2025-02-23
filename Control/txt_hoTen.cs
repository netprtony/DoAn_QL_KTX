using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control
{
    public class txt_hoTen : TextBox
    {
        public ErrorProvider errorProvider;

        public txt_hoTen()
        {
            errorProvider = new ErrorProvider();
            this.KeyPress += txt_hoTen_KeyPress;
            this.TextChanged += txt_hoTen_TextChanged;
        }

        // Sự kiện kiểm tra khi người dùng nhập ký tự
        void txt_hoTen_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Chỉ cho phép các ký tự chữ cái và dấu cách (không bao gồm số hoặc ký tự đặc biệt)
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back && e.KeyChar != ' ')
            {
                e.Handled = true; // Ngăn ký tự không hợp lệ
            }
        }

        // Kiểm tra lại chuỗi nhập vào trong sự kiện TextChanged
        void txt_hoTen_TextChanged(object sender, EventArgs e)
        {
            // Kiểm tra chuỗi có ít nhất 2 ký tự và chỉ chứa chữ cái cùng dấu cách
            if (this.Text.Length < 2 || !Regex.IsMatch(this.Text, @"^[A-Za-z\s]+$"))
            {
                errorProvider.SetError(this, "Họ tên phải có ít nhất 2 ký tự và chỉ chứa chữ cái.");
            }
            else
            {
                errorProvider.Clear();
            }
        }
    }
}
