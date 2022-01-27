using System.IO;
using System.Net;
using System.Net.Mail;

namespace Agency.Utilities
{
    public static class Helper
    {
        public static void RemoveFile(string root, string folder, string image)
        {
            string path = Path.Combine(root, folder, image);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
    public static class Email
    {
        public static void EmailSend(string fromMail, string password, string toMail, string body, string subject)
        {
            using (var client = new SmtpClient("smtp.googlemail.com", 587))
            {
                client.Credentials = new NetworkCredential(fromMail, password);
                client.EnableSsl = true;
                var message = new MailMessage(fromMail, toMail);
                message.Body = body;
                message.IsBodyHtml = true;
                message.Subject = subject;
                client.Send(message);
            }
        }
    }
    public enum UserRoles{
        Admin,
        Member
        }
}
