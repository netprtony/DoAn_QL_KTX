using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GUI.Model
{
    public class EmailService
    {
        private readonly string _fromEmail = "websitemuabannhadat@gmail.com"; // Địa chỉ email gửi
        private readonly string _fromPassword = "apxitumsvjsmkxyl"; // Mật khẩu email gửi

        public void SendOtp(string emailAddress, string otp)
        {
            var fromAddress = new MailAddress(_fromEmail, "Ký Túc Xá Trường Đại Học Công Thương");
            var toAddress = new MailAddress(emailAddress);
            const string subject = "Mã Xác Thực Đổi Mất Khẩu";
            string body = $"Your OTP code is: {otp}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, _fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

        // Hàm gửi hóa đơn qua email
        public void SendInvoiceEmail(string toEmail, string subject, string body, string attachmentPath)
        {
            var fromAddress = new MailAddress(_fromEmail, "Ký Túc Xá Trường Đại Học Công Thương");
            var toAddress = new MailAddress(toEmail);

            // Cấu hình SMTP
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, _fromPassword)
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                // Đính kèm hóa đơn PDF
                if (File.Exists(attachmentPath))
                {
                    var attachment = new Attachment(attachmentPath);
                    message.Attachments.Add(attachment);
                }
                else
                {
                    // Thông báo nếu tệp đính kèm không tồn tại
                    Console.WriteLine($"Tệp đính kèm không tồn tại: {attachmentPath}");
                }

                // Gửi email
                try
                {
                    smtp.Send(message);
                    Console.WriteLine("Hóa đơn đã được gửi thành công!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi gửi email: {ex.Message}");
                }
            }
        }





    }
}
