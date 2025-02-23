using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Control
{
    public class txt_email:TextBox
    {
        public ErrorProvider errorProvider;
        public txt_email()
        { 
            errorProvider = new ErrorProvider();
            this.Leave += Txt_email_Leave;
            
        }

        private void Txt_email_Leave(object sender, EventArgs e)
        {
            if (!IsValidEmail(this.Text))
            {
                errorProvider.SetError(this, "Địa chỉ email không hợp lệ!");
            }
            else
            {
                errorProvider.SetError(this, ""); // Xóa thông báo lỗi nếu hợp lệ
            }
        }

        // Phương thức kiểm tra định dạng email
        private bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            // Sử dụng biểu thức chính quy để kiểm tra định dạng email
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, emailPattern);
        }
    }
}
