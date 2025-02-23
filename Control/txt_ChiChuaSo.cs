using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Control
{
    public class txt_ChiChuaSo:TextBox
    {

        public txt_ChiChuaSo() { }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // Kiểm tra xem phím được nhấn có phải là phím điều khiển (như phím backspace) không
            if (!char.IsControl(e.KeyChar))
            {
                // Kiểm tra xem phím được nhấn có phải là chữ số không
                if (!char.IsDigit(e.KeyChar))
                {
                    // Nếu không phải là chữ số, ngăn chặn việc nhấn phím
                    e.Handled = true;
                }
            }

            // Nếu cần, bạn có thể thêm logic bổ sung cho các tình huống cụ thể,
            // chẳng hạn như cho phép dấu thập phân hoặc dấu âm nếu cần.
        }
    }
}
