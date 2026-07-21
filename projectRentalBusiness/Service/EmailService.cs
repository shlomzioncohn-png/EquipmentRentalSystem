using Core.Services;
using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class EmailService:IEmailService
    {
        public async Task SendEmailAsync(string to, string subject, string text, string name)
        {
            var message = new MimeMessage();

            // הגדרת השולח
            message.From.Add(new MailboxAddress("מאת: גומלי חסדים!!", "shlomzioncohn@gmail.com"));

            // הגדרת הנמען מהאובייקט שקיבלנו
            message.To.Add(new MailboxAddress(name, to));

            // נושא המייל
            message.Subject = subject;

            // גוף המייל (ניתן להשתמש ב-HTML)
            message.Body = new TextPart("html")
            {
                Text = $"<h1>שלום {name}!</h1><p>{text}</p>"
            };

            using (var client = new SmtpClient())
            {
                try
                {
                    // התחברות לשרת של גוגל (פורט 587 הוא הסטנדרט ל-TLS)
                    await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                    // התחברות עם המייל והמפתח (App Password) שקיבלנו בשלב 2
                    await client.AuthenticateAsync("shlomzioncohn@gmail.com", "oarjhzkefajuacqc");

                    // השליחה עצמה
                    await client.SendAsync(message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"שגיאה בשליחת המייל: {ex.Message}");
                }
                finally
                {
                    // ניתוק מסודר
                    await client.DisconnectAsync(true);
                }
            }
        }

    }
}
