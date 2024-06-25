using System.Net;
using System.Net.Mail;

namespace MyFirstJobProject.Services
{
    public class EmailServiceNet
    {
        public void SendMail(string senderEmail, string senderEmailPassword, string reportEmail, byte[] Pdf)
        {
                var message = new MailMessage();
                message.From = new MailAddress(senderEmail);
                message.To.Add(new MailAddress(reportEmail));
                message.Subject = "Proof Of Use";
                message.Body = "Please find the attached PDF document.";

                var attachment = new Attachment(new MemoryStream(Pdf), "ProofOfUse.Pdf", "application/pdf");
                message.Attachments.Add(attachment);

                var smtpClient = new SmtpClient("smtp.gmail.com", 587);
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new NetworkCredential(message.From.Address, senderEmailPassword);

                smtpClient.Send(message);
        }
    }
}
