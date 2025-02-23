using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control
{
    public class txt_Text:TextBox
    {
        public ErrorProvider errorProvider;

        public txt_Text() 
        {
            errorProvider = new ErrorProvider();
            this.KeyPress += Txt_Text_KeyPress;
            this.Leave += Txt_Text_Leave;
        }

        void Txt_Text_Leave(object sender, EventArgs e)
        {
            // Xóa thông báo lỗi khi TextBox mất tiêu điểm
            if (errorProvider != null)
            {
                errorProvider.SetError(this, "");
            }
        }

        void Txt_Text_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Kiểm tra nếu ký tự không phải chữ cái hoặc khoảng trắng
            if (!char.IsLetter(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Kiểm tra nếu errorProvider đã được khởi tạo
                if (errorProvider != null)
                {
                    errorProvider.SetError(this, "Chỉ được nhập chữ cái và khoảng trắng!");
                }
                e.Handled = true; // Ngăn không cho ký tự không hợp lệ xuất hiện trong TextBox
            }
            else
            {
                if (errorProvider != null)
                {
                    errorProvider.SetError(this, "");
                }
            }
        }
    }
}
