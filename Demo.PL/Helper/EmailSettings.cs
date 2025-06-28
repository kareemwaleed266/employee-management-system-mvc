using System.Net;
using System.Net.Mail;

namespace Demo.PL.Helper
{
    public static class EmailSettings
    {
        public static void SendEmail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com",587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("kwr260604@gmail.com", "ggvvloheveowxjff");
            client.Send("kwr260604@gmail.com",email.To,email.Title,email.Body);
        }
    }
}
// ggvv lohe veow xjff
