using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Control
{
    public class txt_SDT: TextBox
    {
        public ErrorProvider errorProvider;

        public txt_SDT()
        {
            errorProvider = new ErrorProvider(); // Khởi tạo ErrorProvider
            this.KeyPress += Txt_SDT_KeyPress;
            this.Leave += Txt_SDT_Leave;
        }

        void Txt_SDT_Leave(object sender, EventArgs e)
        {
            // Kiểm tra chiều dài khi mất tiêu điểm
            if (this.Text.Length < 10)
            {
                errorProvider.SetError(this, "Số điện thoại phải có ít nhất 10 ký tự."); // Thiết lập thông báo lỗi
            }
            else if (this.Text.Length>10)
            {
                errorProvider.SetError(this, "Đã nhập đủ 10 kí tự"); // Thiết lập thông báo lỗi
            }
            else
            {
                errorProvider.SetError(this, ""); // Xóa thông báo lỗi
            }
        }

        void Txt_SDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                return;
            }

            // Kiểm tra xem ký tự được nhập có phải là chữ số hay không
            if (!char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập ký tự không hợp lệ
            }

            // Nếu bạn muốn giới hạn số lượng ký tự, bạn có thể kiểm tra chiều dài
            if (this.Text.Length >= 10 && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Ngăn không cho nhập khi đã đủ 10 ký tự
            }
        }
    }
}
