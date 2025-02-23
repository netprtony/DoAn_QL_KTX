using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control
{
    public class txt_soDienThoai :TextBox
    {
        public ErrorProvider errorProvider;

        public txt_soDienThoai()
        {
            errorProvider = new ErrorProvider();
            this.TextChanged += txt_soDienThoai_TextChanged;
        }

        // Kiểm tra chuỗi nhập vào trong sự kiện TextChanged
        void txt_soDienThoai_TextChanged(object sender, EventArgs e)
        {
            string phoneNumber = this.Text;

            // Kiểm tra nếu số điện thoại có đúng 10 chữ số
            if (!Regex.IsMatch(phoneNumber, @"^\d{10}$"))
            {
                errorProvider.SetError(this, "Số điện thoại phải có 10 chữ số.");
                return;
            }

            // Kiểm tra xem số điện thoại có thuộc các nhà mạng Việt Nam không
            string prefix = phoneNumber.Substring(0, 3); // Lấy ba chữ số đầu tiên
            bool isValidNetwork = false;

            if (prefix == "090" || prefix == "093" || prefix == "089") // Mobifone
            {
                isValidNetwork = true;
            }
            else if (prefix == "091" || prefix == "094" || prefix == "088") // VinaPhone
            {
                isValidNetwork = true;
            }
            else if (prefix == "092" || prefix == "056") // Vietnamobile
            {
                isValidNetwork = true;
            }
            else if (prefix == "099") // GMobile
            {
                isValidNetwork = true;
            }
            else if (prefix == "03" || prefix == "07" || prefix == "08" || prefix == "05") // Viettel
            {
                isValidNetwork = true;
            }

            if (!isValidNetwork)
            {
                errorProvider.SetError(this, "Số điện thoại không thuộc các nhà mạng của Việt Nam.");
            }
            else
            {
                errorProvider.Clear();
            }
        }
    }
}
