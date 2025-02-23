using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control
{
    public class dtp_ngaySinh :DateTimePicker
    {
        public ErrorProvider errorProvider;

        public dtp_ngaySinh()
        {
            errorProvider = new ErrorProvider();
            this.ValueChanged += dtp_ngaySinh_ValueChanged;

            // Thiết lập ngày tối đa (ngày hiện tại trừ đi 18 năm) để đảm bảo ít nhất 18 tuổi
            this.MaxDate = DateTime.Today.AddYears(-18);
            this.Format = DateTimePickerFormat.Short;
        }

        // Sự kiện kiểm tra khi người dùng chọn ngày
        void dtp_ngaySinh_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = this.Value;
            int age = DateTime.Today.Year - selectedDate.Year;

            // Điều chỉnh nếu ngày sinh chưa đến trong năm hiện tại
            if (selectedDate > DateTime.Today.AddYears(-age))
            {
                age--;
            }

            // Kiểm tra tuổi và thiết lập thông báo lỗi nếu nhỏ hơn 18
            if (age < 18)
            {
                errorProvider.SetError(this, "Người dùng phải đủ 18 tuổi.");
            }
            else
            {
                errorProvider.Clear();
            }
        }

    }
}
